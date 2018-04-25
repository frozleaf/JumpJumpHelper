using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace JumpJumpHelper.UIs
{
    /// <summary>
    /// 进度窗体
    /// </summary>
    public partial class FrmProgress : Form
    {
        /// <summary>
        /// 进度窗体持续时间
        /// </summary>
        private TimeSpan _during;

        /// <summary>
        /// 获取或设置提示信息
        /// </summary>
        public string Tip
        {
            get { return this.labelTip.Text; }
            set { this.labelTip.Text = value; }
        }

        /// <summary>
        /// 是否显示时间
        /// </summary>
        public bool IsShowTime
        {
            get { return this.labelDuring.Visible; }
            set
            {
                this.label2.Visible = value;
                this.labelDuring.Visible = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private FrmProgress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 进度窗体持续时间计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // ticks和毫秒的单位差为10000
            _during += new TimeSpan(10000 * this.timer1.Interval);
            this.labelDuring.Text = _during.ToString();
        }

        private static bool _isShowing = false;
        public static void InvokeShowProgressDialog(string tip, bool isShowTime)
        {
            Thread t = new Thread(()=>ShowProgressDialog(tip,isShowTime));
            t.IsBackground = true;
            t.Start();
        }

        public static void InvokeHideProgressDialog()
        {
            _isShowing = false;
        }

        public static void ShowProgressDialog(string tip, bool isShowTime)
        {
            _isShowing = true;
            var frm = new FrmProgress();
            frm.Tip = tip;
            frm.IsShowTime = isShowTime;
            frm.Show();
            while (_isShowing)
            {
                Thread.Sleep(200);
                Application.DoEvents();
            }
            frm.Close();
            frm.Dispose();
        }
    }
}
