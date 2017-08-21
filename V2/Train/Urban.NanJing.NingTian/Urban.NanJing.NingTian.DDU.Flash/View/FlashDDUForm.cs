using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonUtil.Util;
using flash;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using Urban.NanJing.NingTian.DDU.Flash.Interface;

namespace Urban.NanJing.NingTian.DDU.Flash.View
{
    public partial class FlashDDUForm : ProjectFormBase, IFlashInteractive
    {
        private FlashWindow m_FlashWindow;

        protected FlashDDUForm()
        {
            Initalize();
        }

        public FlashDDUForm(string appName, IDataPackage dataPackage)
            : base(appName, dataPackage)
        {
            AppConfig = dataPackage.Config.AppConfigs.FirstOrDefault(f => f.AppName == appName);
            Initalize();
        }

        protected override void OnClosed(EventArgs e)
        {
            Controls.Clear();

            base.OnClosed(e);
        }

        public event FlashDataEventHandler OnReceiveFromFlash;

        public void SetValue(string fun, string value)
        {
            try
            {
                m_FlashWindow.SetValue(fun, value);
            }
            catch (Exception e)
            {
                LogMgr.Info("Set flash vaule error,m_FlashWindow.SetValue({0}, {1}), {2}", fun, value, e.ToString());
            }
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

        public void SetValue(string fun, IEnumerable<string> values)
        {
            SetValue(fun, string.Join("#", values));
        }

        private void FlashWindowOnOnReceiveFromFlash(string cmd, string value)
        {
            var handler = OnReceiveFromFlash;
            if (handler != null)
            {
                handler(cmd, value);
            }
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
                var file = Path.Combine(DataPackage.Config.SystemDicrectory.BaseDirectory, "Flash\\人机界面.swf");
                LogMgr.Info("Start , Create flash window which file={0}", file);
                m_FlashWindow =
                    new FlashWindow(file)
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0),
                        Padding =  new Padding(0)
                    };
                m_FlashWindow.OnReceiveFromFlash += FlashWindowOnOnReceiveFromFlash;
                LogMgr.Info("Sucess, Create flash window which file={0}", file);
            }
            catch (Exception e)
            {
                LogMgr.Error("Can not create flash window", e.ToString());
            }

            Controls.Add(m_FlashWindow);
        }
    }
}
