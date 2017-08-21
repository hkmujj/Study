using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using flash;
using General.FlashLoader.Subsystem.Interface;
using General.FlashLoader.Subsystem.Model;
using General.FlashLoader.Subsystem.SendQueue;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;

namespace General.FlashLoader.Subsystem.View
{
    public partial class FlashLoaderForm : ProjectFormBase, IFlashInteractive
    {
        private FlashWindow m_FlashWindow;

        private FlashSendThread m_FlashSendThread;

        public event FlashDataEventHandler FlashDataRevceived;

        protected FlashLoaderForm()
        {
            Initalize();
        }

        public FlashLoaderForm(string appName, IDataPackage dataPackage)
            : base(appName, dataPackage)
        {
            AppConfig = dataPackage.Config.AppConfigs.FirstOrDefault(f => f.AppName == appName);
            Initalize();
        }

        private void Initalize()
        {
            InitializeComponent();

            LoadFlash();

        }

        private void LoadFlash()
        {
            FlashLog.IsLogOn = true;
            try
            {
                var file = Path.Combine(Path.Combine(DataPackage.Config.SystemDicrectory.BaseDirectory, AppName),
                    "Flash\\FlashLoader.xml");

                AppLog.Info("Start , Create flash window which file={0}", file);
                var logger = LogMgr.GetLogger("FlashLog");
                logger.Info("Create FlashLog success.");
                m_FlashWindow = new FlashWindow(file, logger)
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Padding = new Padding(0),
                };

                AppLog.Info("Call flash window .ctor sucuess.");

                m_FlashWindow.OnReceiveFromFlash += FlashWindowOnOnReceiveFromFlash;
                m_FlashWindow.MouseEnter += FlashWindowOnMouseEnter;

                AppLog.Info("Add flash windown events sucuess.");

                m_FlashSendThread = new FlashSendThread(m_FlashWindow);

                AppLog.Info("Create FlashSendThread instance sucess.");

                m_FlashSendThread.RunWorkerAsync();

                AppLog.Info("Create flash window Sucessed. which file={0}", file);
            }
            catch (Exception e)
            {
                AppLog.Error("Can not create flash window, {0}", e.ToString());
            }

            Controls.Add(m_FlashWindow);
        }

        protected override void OnClosed(EventArgs e)
        {
            Controls.Clear();

            base.OnClosed(e);
        }

        public void SetValue(FlashCommandType cmdType, string value)
        {
            if (m_FlashSendThread != null)
            {
                m_FlashSendThread.Push(new FlashData(cmdType, value));
            }
        }

        private void FlashWindowOnOnReceiveFromFlash(string cmd, string value)
        {
            var handler = FlashDataRevceived;
            if (handler != null)
            {
                FlashCommandType cmdType;
                if (Enum.TryParse(cmd, true, out cmdType))
                {
                    handler(cmdType, value);
                }
                else
                {
                    AppLog.Error("Can not parser flash command 【{0}】", cmd);
                }
            }
        }

        public void SetVisible(bool visible)
        {
            if (m_FlashWindow != null)
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
            else
            {
                AppLog.Warn("Can not set visible of flash window, because the flash window is not created.");
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        private void FlashWindowOnMouseEnter(object sender, EventArgs eventArgs)
        {

        }
    }
}
