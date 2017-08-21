using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using flash;


namespace flashWindow
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            Initalize();
        }

        private FlashWindow m_FlashWindow;



        protected override void OnClosed(EventArgs e)
        {
            Controls.Clear();

            base.OnClosed(e);
        }




        private void Initalize()
        {
            InitializeComponent();

            LoadFlash();

        }

        FlashSender mFlashSender;


        private void LoadFlash()
        {
            FlashLog.IsLogOn = true;
            try
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "General.FlashLoader.HXD2MMI\\Flash\\FlashLoader.xml");

                m_FlashWindow = new FlashWindow(file, null)
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Padding = new Padding(0),

                };
                m_FlashWindow.OnReceiveFromFlash += FlashWindowOnOnReceiveFromFlash;//flash有输出时 传给数据服务程序


                //flash窗口通过该属性 找到该窗口  将值传回

                mFlashSender = new FlashSender("Flash_HXD2");
                mFlashSender.OnReceive += Form1OnReceive;//接受到数据  给flash设值

            }
            catch (Exception e)
            {
            }

            Controls.Add(m_FlashWindow);
        }


        private void FlashWindowOnOnReceiveFromFlash(string cmd, string value)
        {
            mFlashSender.Send("Flash_Server", cmd, value);
        }

        private void Form1OnReceive(string cmd, string value)
        {
            if (cmd == "closeWindow")
            {
                this.Close();
            }
            else
            {
                Debug.Write(String.Format("recv --- Set value {0} .{1}\r\n", DateTime.Now, DateTime.Now.Millisecond));
                Console.Write("set value " + DateTime.Now);
                m_FlashWindow.SetValue(cmd, value);
            }
        }


    }
}
