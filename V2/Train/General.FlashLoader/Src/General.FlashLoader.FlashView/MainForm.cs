using System;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Util;
using flash;
using General.FlashLoader.FlashView.Model;

namespace General.FlashLoader.FlashView
{
    public partial class MainForm : Form
    {
        private FlashWindow m_FlashWindow;

        private FlashStartArg m_FlashStartArg;

        private FlashSender m_FlashSender;

        public MainForm(FlashStartArg startArg = null)
        {
            Initalize(startArg);
        }

        private void Initalize(FlashStartArg startArg)
        {
            InitializeComponent();

            if (startArg != null && !DesignMode)
            {
                m_FlashStartArg = startArg;
                LoadFlash(startArg);
                Load += OnLoad;
            }
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            TopMost = m_FlashStartArg.TopMost;
            Location = new Point(m_FlashStartArg.Bound.X, m_FlashStartArg.Bound.Y);
            Size = new Size(m_FlashStartArg.Bound.Width, m_FlashStartArg.Bound.Height);
            FormBorderStyle = m_FlashStartArg.FormBorderStyle;
            Text = m_FlashStartArg.AppName;
        }

        protected override void OnClosed(EventArgs e)
        {
            Controls.Clear();

            base.OnClosed(e);
        }

        private void LoadFlash(FlashStartArg startArg)
        {

            FlashLog.IsLogOn = true;
            try
            {
                var file = startArg.SwfConfigFile;
                var logger = LogMgr.GetLogger("FlashLog");
                m_FlashWindow = new FlashWindow(file, logger)
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Padding = new Padding(0),
                };
                m_FlashSender = new FlashSender(startArg.ClilentSenderName);
                m_FlashSender.OnReceive += FlashSenderOnOnReceive;
                //m_FlashWindow.OnReceiveFromFlash += FlashWindowOnOnReceiveFromFlash;//flash有输出时 传给数据服务程序


                //flash窗口通过该属性 找到该窗口  将值传回

                //mFlashSender = new FlashSender("Flash_HXD2");
                //mFlashSender.OnReceive += Form1OnReceive;//接受到数据  给flash设值

            }
            catch (Exception e)
            {
            }

            Controls.Add(m_FlashWindow);
        }

        private void FlashSenderOnOnReceive(string cmd, string value)
        {
            m_FlashWindow.SetValue(cmd, value);
        }
    }
}
