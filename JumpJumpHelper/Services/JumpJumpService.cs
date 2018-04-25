using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using JumpJumpHelper.Services.ImageProcessors;
using JumpJumpHelper.Services.ImageProcessors.Contracts;
using TDLib.Utils;

namespace JumpJumpHelper.Services
{
    class JumpJumpService
    {
        // 绘制标记圆的半径
        private int _drawCircleRadius = 10;
        // 时间系数
        private float _timeCoefficient = 1.382f;
        // 棋子底盘高度
        private int _chesspieceBottomHeight = 28;
        // 棋子宽度
        private float _chesspieceWidth = 65f;
        // 棋子主颜色
        private Color _chesspieceMainColor = Color.FromArgb(56, 50, 89);
        // 连跳点颜色
        private Color _doubleJumpPointColor = Color.FromArgb(245, 245, 245);
        // 棋盘最大宽度
        private float _chessboardMaxWidth = 380;
        // 跳跃计数器
        private int _jumpCounter = 0;
        // 每跳跃多少次，重启一下adb进程
        private int _restartAdbProcessPerJumpCount = 50;
        // 连跳点搜索半径
        private int _doubleJumpPointSearchRadius = 200;
        // 连跳计数
        private int _doubleJumpCounter = 0;
        // 上次棋子位置
        private Point _lastChesspiecePosition = Point.Empty;
        // 上次按压时间
        private int _lastPushDuringInMs = -1;
        // 时间系数变化事件
        public event EventHandler TimeCoefficientChanged;

        /// <summary>
        /// 构造函数
        /// </summary>
        public JumpJumpService()
        {
            if (!System.IO.Directory.Exists("captures"))
                System.IO.Directory.CreateDirectory("captures");
        }

        /// <summary>
        /// 时间系数
        /// </summary>
        public float TimeCoefficient
        {
            get { return _timeCoefficient; }
        }

        /// <summary>
        /// 获取连跳计数
        /// </summary>
        /// <returns></returns>
        public int GetDoubleJumpCounter()
        {
            return _doubleJumpCounter;
        }

        /// <summary>
        /// 重置连跳计数
        /// </summary>
        public void ResetDoubleJumpCounter()
        {
            _doubleJumpCounter = 0;
        }

        /// <summary>
        /// 结束其他adb进程
        /// </summary>
        public void KillOtherAdbProcesses()
        {
            string[] pros = new string[] 
            { 
                "360MobileMgr"
            };
            foreach (var p in pros)
            {
                ProcessUtil.KillProcess(p); 
            }
        }

        /// <summary>
        /// 获取手机屏幕尺寸
        /// </summary>
        /// <returns></returns>
        public Size GetMobileScreenSize()
        {
            using (var img = CaptureMobileScreen())
            {
                if (img == null)
                    return Size.Empty;

                return img.Size;
            }

            // 另一种方法：adb shell wm size
        }

        /// <summary>
        /// 捕获手机屏幕
        /// </summary>
        /// <returns></returns>
        public Bitmap CaptureMobileScreen()
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".png";
            return this.CaptureMobileScreen(fileName);
        }

        /// <summary>
        /// 捕获手机屏幕
        /// </summary>
        /// <returns></returns>
        private Bitmap CaptureMobileScreen(string fileName)
        {
            string localFilePath = ".\\captures\\" + fileName;

            // 截屏并拉取文件
            RunAdb("shell screencap -p /sdcard/autojump.png");
            RunAdb("pull /sdcard/autojump.png .\\captures\\" + fileName);
            if (!System.IO.File.Exists(localFilePath))
                return null;

            // 重新new一个Bitmap对象，防止文件被占用
            using (Image img = Image.FromFile(localFilePath))
            {
                return new Bitmap(img);  
            }
        }

        /// <summary>
        /// 查找关键点坐标（棋子坐标、棋盘坐标）
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="chesspiecePoint">棋子坐标</param>
        /// <param name="chessboardPoint">棋盘坐标</param>
        public void FindKeyPoints(Bitmap bmp, out Point chesspiecePoint, out Point chessboardPoint)
        {
            chesspiecePoint = Point.Empty;
            chessboardPoint = Point.Empty;

            IImageProcessor img = new MemoryImageProcessor(bmp);

            // 图像宽度
            int width = img.Width;
            // 图像高度
            int height = img.Height;
            // 棋子Y轴起始点
            int chesspieceYStart = height/3;
            // 棋子Y轴结束点
            int chesspieceYEnd = height*2/3;
            // 棋盘X轴起始点
            int chessboardXStart = 0;
            // 棋盘X轴结束点
            int chessboardXEnd = width;
            // 棋盘Y轴起始点
            int chessboardYStart = height / 3;
            // 棋盘Y轴结束点
            int chessboardYEnd = height * 2 / 3;

            // 查找棋子坐标
            /*
             * 1、为了过滤游戏中左上角和右上角相关按钮，将搜索Y轴区域锁定在高度的[1/3,2/3]内；
             * 2、为了减少搜索范围，过滤掉上方的纯色区域
             *    -纯色区域的判断规则，取每行的第一个点颜色c1,如果该行的其他点颜色都与其c1相同，则该行为纯色区域
             */

            // 逐行遍历，过滤纯色区域
            for (int y = chesspieceYStart; y <= chesspieceYEnd; y++)
            {
                bool flag = false;
                Color firstColor = img.GetPixel(y, 0);
                for (int x = 0; x < width; x++)
                {
                    Color c = img.GetPixel(x,y);
                    if (!IsSameColor(firstColor, c))
                    {
                        flag = true;
                        chesspieceYStart = y;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }

            // 查找棋子坐标
            /*
             * 查找所有与棋子颜色相似的颜色的坐标，则棋子x坐标为平均值，y坐标为所有坐标y坐标的最小值
             */
            int chesspieceXSum = 0;
            int chesspieceXCount = 0;
            int chesspieceX = 0;
            int chesspieceY = 0;
            for (int y = chesspieceYStart; y <= chesspieceYEnd; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = img.GetPixel(x,y);
                    if (IsSimilarColor(_chesspieceMainColor, c, 10))
                    {
                        chesspieceXSum += x;
                        chesspieceXCount++;
                        if (y > chesspieceY)
                        {
                            chesspieceY = y;
                        }
                    }
                }
            }
            chesspieceX = (int)(chesspieceXSum*1.0/chesspieceXCount);
            chesspiecePoint = new Point(chesspieceX, chesspieceY-_chesspieceBottomHeight);

            // 查找棋盘坐标
            /*
             * 特征：
             * 1、棋盘位于棋子的上方；
             * 2、棋子和棋盘总是分布在屏幕左右不同侧；
             * 3、通过查找棋子的上顶点和左顶点，来定位棋盘中心点
             */

            chessboardPoint = Point.Empty;

            // 缩小棋盘搜索区域
            if (chesspieceX < width / 2)
            {
                // 棋子在左侧时，棋盘在右侧
                chessboardXStart = chesspieceX;
                chessboardXEnd = width;
            }
            else
            {
                // 棋子在右侧时，棋盘在左侧
                chessboardXStart = 0;
                chessboardXEnd = chesspieceX;
            }
            chessboardYStart = height/3;
            chessboardYEnd = chesspieceY;

            // 寻找上顶点
            Point chessboardTopPoint = Point.Empty;
            Color chessboardTopColor = Color.Empty;
            for (int y = chessboardYStart; y < chessboardYEnd; y++)
            {
                bool flag = false;
                Color c0 = img.GetPixel(0, y);
                for (int x = chessboardXStart; x < chessboardXEnd; x++)
                {
                    Color c = img.GetPixel(x, y);
                    if (!IsSimilarColor(c0, c,5))
                    {
                        // 特殊情况：棋子的顶部超过了下一个棋盘的顶部
                        // 解决：x轴方向上，过滤在棋子范围内的坐标
                        if (x >= chesspieceX - _chesspieceWidth/2 && x <= chesspieceX + _chesspieceWidth/2)
                        {
                            continue;
                        }
                        flag = true;
                        chessboardTopPoint = new Point(x, y);
                        chessboardTopColor = c;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }

            // 检测是否存在连跳点(r245 g245 b245)
            // 1、如果存在连跳点，则连跳点即为棋盘中心点
            // 2、如果不存在连跳点，则根据上顶点+左、右顶点，来确定棋盘中心点
            for (int y = 0; y < _doubleJumpPointSearchRadius; y++)
            {
                Color c = img.GetPixel(chessboardTopPoint.X, chessboardTopPoint.Y + y);
                if (IsSameColor(c,_doubleJumpPointColor))
                {
                    /*存在连跳点的情况*/
                    _doubleJumpCounter++;
                    chessboardPoint = new Point(chessboardTopPoint.X, chessboardTopPoint.Y + y);
                    Console.WriteLine("存在连跳点");
                    break;
                }
            }
            if (chessboardPoint == Point.Empty)
            {
                /*不存在连跳点的情况*/

                _doubleJumpCounter = 0;
                // 寻找左顶点和右顶点
                Point chessboardLeftPoint = new Point(width, 0);
                Point chessboardRightPoint = new Point(0, 0);
                float chessboardMaxWidthHalf = _chessboardMaxWidth / 2;
                for (int y = chessboardYStart; y < chessboardYEnd; y++)
                {
                    for (int x = chessboardXStart; x < chessboardXEnd; x++)
                    {
                        Color c = img.GetPixel(x, y);
                        // 由于上顶点和左顶点的颜色不一定一样，故采用相似颜色判断
                        if (IsSimilarColor(c, chessboardTopColor, 10))
                        {
                            // 特殊情况：当前棋盘和下一个棋盘颜色相似，寻找左顶点时，定位到当前棋盘
                            // 解决：左顶点必须在最大棋盘宽度内
                            if (x < chessboardTopPoint.X - chessboardMaxWidthHalf ||
                                x > chessboardTopPoint.X + chessboardMaxWidthHalf)
                            {
                                continue;
                            }
                            if (x < chessboardLeftPoint.X)
                            {
                                // 更新左顶点
                                chessboardLeftPoint.X = x;
                                chessboardLeftPoint.Y = y;
                            }
                            if (x > chessboardRightPoint.X)
                            {
                                // 更新右顶点
                                chessboardRightPoint.X = x;
                                chessboardRightPoint.Y = y;
                            }
                        }
                    }
                }
                // 左右顶点选取规则：取Y轴方向，与上顶点差值最小的
                if (Math.Abs(chessboardLeftPoint.Y - chessboardTopPoint.Y) >
                    Math.Abs(chessboardRightPoint.Y - chessboardTopPoint.Y))
                {
                    chessboardPoint = new Point(chessboardTopPoint.X, chessboardRightPoint.Y);
                }
                else
                {
                    chessboardPoint = new Point(chessboardTopPoint.X, chessboardLeftPoint.Y);
                }
            }

            img.Dispose();
        }

        /// <summary>
        /// 在原图上绘制点
        /// </summary>
        /// <param name="img">原图</param>
        /// <param name="point">绘制点</param>
        /// <param name="pointColor"></param>
        public void DrawPoint(Bitmap img, Point point, Color pointColor)
        {
            using (var g = Graphics.FromImage(img))
            {
                SolidBrush brush = new SolidBrush(pointColor);
                int radius = _drawCircleRadius;
                g.FillEllipse(brush, point.X, point.Y, radius, radius);
                brush.Dispose();
            }
        }

        /// <summary>
        /// 是否为相似颜色
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="tolerance">误差</param>
        /// <returns></returns>
        private bool IsSimilarColor(Color c1, Color c2, int tolerance)
        {
            return Math.Abs(c1.R - c2.R) <= tolerance &&
                   Math.Abs(c1.G - c2.G) <= tolerance &&
                   Math.Abs(c1.B - c2.B) <= tolerance;
        }

        /// <summary>
        /// 是否为相同颜色
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        private bool IsSameColor(Color c1, Color c2)
        {
            return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }

        /// <summary>
        /// 根据起点终点，进行跳跃
        /// </summary>
        /// <param name="start">起点坐标</param>
        /// <param name="end">终点坐标</param>
        public void Jump(Point start, Point end)
        {
            double distance = GetDistance(start, end);
            double time = distance*_timeCoefficient;

            this.JumpByTime((int)time);

            // 时间系数动态调整
            //if (_lastPushDuringInMs == -1)
            //{
            //    // 第一次时，记录上次棋子位置和上次按压时间
            //    _lastChesspiecePosition = start;
            //    _lastPushDuringInMs = (int)time;
            //    if (TimeCoefficientChanged != null)
            //    {
            //        TimeCoefficientChanged(this, EventArgs.Empty);
            //    }
            //}
            //else
            //{
            //    // 第二次以及以后，动态计算时间系数
            //    // 时间系数=time/distance;
            //    double dis = GetDistance(_lastChesspiecePosition, start);
            //    _timeCoefficient = (float)(_lastPushDuringInMs / dis);
            //    if (TimeCoefficientChanged != null)
            //    {
            //        TimeCoefficientChanged(this, EventArgs.Empty);
            //    }
            //    _lastChesspiecePosition = start;
            //    _lastPushDuringInMs = (int)time;
            //}

            // 计数跳跃次数
            _jumpCounter++;

            // 每隔一定跳跃次数后，重启adb进程
            if (_jumpCounter > _restartAdbProcessPerJumpCount)
            {
                ProcessUtil.KillProcess("adb");
            }
        }

        /// <summary>
        /// 计算两点间距离
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private double GetDistance(Point start, Point end)
        {
            double distance = Math.Sqrt((start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y));

            if (distance < 0)
                distance = -distance;

            return distance;
        }

        /// <summary>
        /// 根据手指按压时间，进行跳跃
        /// </summary>
        /// <param name="milliseconds">手指按压时间，单位毫秒</param>
        public void JumpByTime(int milliseconds)
        {
            // 2018.01.08 增加按压起点和滑动点随机偏移
            Random r = new Random();
            int startX = r.Next(40, 80);
            int StartY = r.Next(200, 300);
            int endX = startX + r.Next(1, 3);
            int endY = StartY + r.Next(2, 5);

            // 注意：该命令会阻塞adb.exe进程milliseconds毫秒
            RunAdb(string.Format("shell input swipe {0} {1} {2} {3} {4}", startX, StartY, endX, endY, milliseconds));
        }

        /// <summary>
        /// 运行adb
        /// </summary>
        /// <param name="cmd">参数</param>
        public string RunAdb(string cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = Application.StartupPath + "\\adb\\adb.exe";
            p.StartInfo.Arguments = cmd;
            p.StartInfo.UseShellExecute = false; // 使用redirect时必须设置为false
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true; // 对于console程序有效
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit(); // 等待进程结束

            return output;
        }
    }
}
