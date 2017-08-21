using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Constant;
using Engine.TPX21F.HXN5B.Resources.Keys;
using Engine.TPX21F.HXN5B.View.Contents.Monitor.Detail;
using Microsoft.Practices.Prism.Regions;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToMonitorECUActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            RegionManager.RequestNavigate(RegionNames.ContentMonitorContent,
                typeof(MonitorECUContentView).FullName);
            NavigateTo(StateKeys.Root_¼à¿ØÆ÷ECU);
        }
    }
}