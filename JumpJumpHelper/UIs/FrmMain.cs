using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using JumpJumpHelper.Services;
using TDLib.UIs.Forms;
using TDLib.Utils;

namespace JumpJumpHelper.UIs
{
    public partial class FrmMain : FrmBase
    {
        private Point _start;
        private Point _end;
        private JumpJumpService _service;
        private bool _isRunning = false;
        private bool _isDebug = false;

        public FrmMain()
        {
            InitializeComponent();
            LoadIconFromRes("app.ico");
            _service = new JumpJumpService();
            _service.TimeCoefficientChanged += new EventHandler(_service_TimeCoefficientChanged);
            _isDebug = AppSettingsUtil.Instance.GetValue<bool>("isDebug", false);

            if (_isDebug)
            {
                this.toolStripButtonMark.Visible = true;
                this.toolStripMenuItemLoad.Visible = true;
            }
        }

        void _service_TimeCoefficientChanged(object sender, EventArgs e)
        {
            this.toolStripStatusLabelTimeCoefficient.Text = "当前时间系数:" + this._service.TimeCoefficient;
        }

        private void toolStripButtonCapture_Click(object sender, EventArgs e)
        {
            _service.KillOtherAdbProcesses();
            var img = _service.CaptureMobileScreen();
            if (img == null)
            {
                MsgBox.Error("截屏失败!");
                return;
            }
            this.pictureBox1.Image = img;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("设为起点", null, (o, args) => {
                _start = e.Location;
                this.toolStripStatusLabelStartPoint.Text = "起点：" + _start;
            });
            cms.Items.Add("设为终点", null, (o, args) => {
                _end = e.Location;
                this.toolStripStatusLabelEndPoint.Text = "终点：" + _end;
            });
            cms.Show(sender as PictureBox, e.Location);
        }

        private void toolStripButtonMannual_Click(object sender, EventArgs e)
        {
            _service.KillOtherAdbProcesses();
            _service.Jump(_start,_end);
            Thread.Sleep(1000);
            toolStripButtonCapture_Click(null, null);
        }

        private void toolStripButtonAuto_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                _isRunning = false;
                this.toolStripButtonAuto.Enabled = false;
                this.toolStripButtonAuto.Text = "正在停止";
                return;
            }

            this.toolStripButtonAuto.Text = "停止自动跳跃";
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();

            FrmProgress.ShowProgressDialog("正在连接手机,请稍后...", true);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripButtonAuto.Text = "自动跳跃";
            this.toolStripButtonAuto.Enabled = true;
            _isRunning = false;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is Image)
            {
                this.pictureBox1.Image = e.UserState as Image; 
            }
            else if (e.UserState is Size)
            {
                Size size = (Size) e.UserState;
                this.toolStripStatusLabelScreenSize.Text = "屏幕尺寸：" + size.Width + "x" + size.Height; 
            }
            else if (e.UserState is string)
            {
                this.toolStripStatusLabelStatus.Text = e.UserState.ToString();
            }
            else if (e.UserState is Point[])
            {
                var points = e.UserState as Point[];
                this.toolStripStatusLabelStartPoint.Text = "起点：" + points[0].X + "," + points[0].Y;
                this.toolStripStatusLabelEndPoint.Text = "终点：" + points[1].X + "," + points[1].Y;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            _isRunning = true;
            worker.ReportProgress(1, "清理其他adb进程");
            _service.KillOtherAdbProcesses();
            worker.ReportProgress(1, "连接手机");
            Size screenSize = _service.GetMobileScreenSize();
            FrmProgress.InvokeHideProgressDialog();
            if (screenSize == Size.Empty)
            {
                MsgBox.Warning("连接手机失败，已终止！");
                return;
            }
            worker.ReportProgress(1, screenSize);
            long counter = 0;
            while (_isRunning)
            {
                Thread.Sleep(1200);

                counter++;
                string header = "第" + counter + "次跳跃(连跳次数：" + _service.GetDoubleJumpCounter() + ")，";

                // 截图
                worker.ReportProgress(1, header + "开始截图");
                var img = _service.CaptureMobileScreen();
                if (img == null)
                {
                    MsgBox.Error("截屏失败!");
                    return;
                }

                // 寻找关键点
                Point chesspicess;
                Point chessboard;
                try
                {
                    worker.ReportProgress(1, header + "开始寻找关键点");
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    _service.FindKeyPoints(img, out chesspicess, out chessboard);
                    _service.DrawPoint(img, chesspicess, Color.Red);
                    _service.DrawPoint(img, chessboard, Color.Red);
                    sw.Stop();
                    Console.WriteLine(sw.Elapsed);
                    worker.ReportProgress(1, new Point[] { chesspicess, chessboard });
                }
                catch (Exception ex)
                {
                    worker.ReportProgress(1, header + "寻找关键点失败," + ex.Message);
                    continue;
                }

                // 更新图像
                worker.ReportProgress(1, img);

                // 跳跃
                worker.ReportProgress(1, header + "开始跳跃");
                _service.Jump(chesspicess, chessboard);
            }
        }

        private void toolStripMenuItemManual_Click(object sender, EventArgs e)
        {
            if (this._isRunning)
            {
                MsgBox.Warning("自动模式正在运行，无法切换为手动模式！");
                return;
            }
            this.toolStripMenuItemManual.Checked = true;
            this.toolStripMenuItemAuto.Checked = false;

            this.toolStripButtonCapture.Visible = true;
            this.toolStripButtonManual.Visible = true;
            this.toolStripButtonAuto.Visible = false;
        }

        private void toolStripMenuItemAuto_Click(object sender, EventArgs e)
        {
            this.toolStripMenuItemManual.Checked = false;
            this.toolStripMenuItemAuto.Checked = true;
            this.toolStripButtonCapture.Visible = false;
            this.toolStripButtonManual.Visible = false;
            this.toolStripButtonAuto.Visible = true;
        }

        private void toolStripMenuItemQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItemLoad_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "*.png|*.png";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var file = dialog.FileName;
                try
                {
                    this.pictureBox1.Image = Image.FromFile(file);
                }
                catch (Exception ex)
                {
                    MsgBox.Error("加载图像失败", ex);
                }
            }
        }

        private void toolStripButtonMark_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image == null)
            {
                MsgBox.Warning("请先加载图像！");
                return;
            }
            Bitmap bmp = this.pictureBox1.Image.Clone() as Bitmap;
            // 计算关键点
            Point chesspicess;
            Point chessboard;
            _service.FindKeyPoints(bmp, out chesspicess, out chessboard);
            _service.DrawPoint(bmp, chesspicess, Color.Red);
            _service.DrawPoint(bmp, chessboard, Color.Red);
            this.pictureBox1.Image = bmp;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MsgBox.Question("确定要退出跳一跳助手？", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            e.Cancel = true;
        }
    }
}
