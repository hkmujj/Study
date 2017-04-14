using System;
using System.IO;
using System.Windows.Forms;
using CommonUtil.Util;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Log;

namespace MMI.Facility.Control.Entry
{
    public class EntryPoint
    {
        /// <summary>
        /// 主窗口关闭事件
        /// </summary>
        public event FormClosedEventHandler FormClosed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Load;


        public void Run(string[] args)
        {
            var sysConfig = DataSerialization.DeserializeFromXmlFile<SystemConfig>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("Config\\{0}", SystemConfig.FileName)));
            SysLog.Info(string.Format("Current applicatio run in {0} model.", sysConfig.StartModel));
            var flow = FlowFactory.CreateFlowController(sysConfig.StartModel);
            flow.FormClosed += OnFormClosed;
            flow.Load += FlowOnLoad;
            flow.Run();
        }

        private void FlowOnLoad(object sender, EventArgs eventArgs)
        {
            var handler = Load;
            if (handler != null)
            {
                handler(sender, eventArgs);
            }
        }

        protected virtual void OnFormClosed(object sender , FormClosedEventArgs e)
        {
            var handler = FormClosed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
