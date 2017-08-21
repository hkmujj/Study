using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Constant;
using Engine.TPX21F.HXN5B.Resources.Keys;
using Engine.TPX21F.HXN5B.View.Contents.Monitor.Detail;
using Microsoft.Practices.Prism.Regions;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToMonitorAssistMachineActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            RegionManager.RequestNavigate(RegionNames.ContentMonitorContent,
                typeof(MonitorAssistMachineContentView).FullName);
            NavigateTo(StateKeys.Root_监控器辅变);
        }
    }
}