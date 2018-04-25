using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JumpJumpHelper.Services;
using JumpJumpHelper.Services.ImageProcessors;
using JumpJumpHelper.UIs;

namespace JumpJumpHelper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        static void TestImageProcessor()
        {
            string imgFile = "autojump.png";
            var p1 = new DefaultImageProcessor(Image.FromFile(imgFile).Clone() as Bitmap);
            var p2 = new MemoryImageProcessor(Image.FromFile(imgFile).Clone() as Bitmap);

            for (int y = 0; y < p1.Height; y++)
            {
                for (int x = 0; x < p1.Width; x++)
                {
                    Color c1 = p1.GetPixel(x, y);
                    Color c2 = p2.GetPixel(x, y);
                    if (c1 != c2)
                    {
                        throw new Exception("存在颜色不同！");
                    }
                }
            }
        }
    }
}
