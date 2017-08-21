using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using flash;
using Yunda.FlashTester.Data;
using Yunda.FlashTester.Model;
using Yunda.FlashTester.SendQueue;
using Yunda.FlashTester.ViewModel;

namespace Yunda.FlashTester.Views
{
    public partial class Form1 : Form
    {

        //继承 DataFrom（）
        //当前应用只作为 flash数据返送端  不加载flash文件

        public Form1()
        {
            Initalize();
        }

        private FlashWindow m_FlashWindow;

        private FlashSendThread m_FlashSendThread;


        protected override void OnClosed(EventArgs e)
        {
            Controls.Clear();

            base.OnClosed(e);
        }

        public void SetValue(FlashCommandType cmdType, string value)
        {
            m_FlashSendThread.Push(new FlashData(cmdType, value));
            //SetValue(cmdType.ToString(), value);
        }

        public void SetVisible(bool visible)
        {
            if (m_FlashWindow.InvokeRequired)
            {
                m_FlashWindow.Invoke(new Action(() => m_FlashWindow.Visible = visible));
            }
            else
            {
                m_FlashWindow.Visible = visible;
            }
        }

        private void FlashWindowOnOnReceiveFromFlash(string cmd, string value)
        {
        }

        private void Initalize()
        {
            InitializeComponent();

            LoadFlash();

        }

        FlashSender mFlashSender;

        Process flashProcess;

        private void LoadFlash()
        {

            //            FlashLog.IsLogOn = true;
            try
            {
                if (true)
                {
                    LoadFlashThread();
                }
                else
                {
                    //消息传递辅助
                    LoadFlashProcess();
                }

                var dp = new DataProducerThread(m_FlashSendThread);

                var vm = new DataMonitorViewModel();
                var monitor = new DataMonitorView() { DataContext = vm };
                dp.DataProducted += data =>
                {
                    vm.ActureSpeed = data.InFloats[97];
                    vm.SettingSpeed = data.InFloats[98];
                };

                var fomr2 = new Form2(monitor);
                fomr2.Show();

                m_FlashSendThread.RunWorkerAsync();

            }
            catch (Exception e)
            {
            }

            //Controls.Add(m_FlashWindow);
        }

        private void LoadFlashThread()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "General.FlashLoader.HXD2MMI\\Flash\\FlashLoader.xml");

            m_FlashWindow = new FlashWindow(file, null)
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(0),
            };
            m_FlashWindow.OnReceiveFromFlash += FlashWindowOnOnReceiveFromFlash;

            Controls.Add(m_FlashWindow);

            m_FlashSendThread = new FlashSendThread(m_FlashWindow);
        }

        private void LoadFlashProcess()
        {
            mFlashSender = new FlashSender("Flash_Server");
            mFlashSender.OnReceive += Form1OnReceive; //处理接受到的数据  相当于flash传来的数据


            //m_FlashSendThread = new FlashSendThread(m_FlashWindow);
            m_FlashSendThread = new FlashSendThread(mFlashSender); //通过flashSender向flash设值


            //打开 单独显示flash的程序
            //引用flashWindow解决方案  就会自动在本项目输出目录中生成 flashWindow.exe
            var exeFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "flashWindow.exe");
            flashProcess = new Process();
            flashProcess.StartInfo.CreateNoWindow = true;
            flashProcess.StartInfo.FileName = exeFile;
            flashProcess.Start();

        }

        private void Form1OnReceive(string fun, string value)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mFlashSender != null)
            {
                mFlashSender.Send("Flash_HXD2", "closeWindow", "");
            }
        }
    }
}
