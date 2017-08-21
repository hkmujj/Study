using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Subway.TCMS.LanZhou.Constant;
using Subway.TCMS.LanZhou.View.Contents;
using Subway.TCMS.LanZhou.View.Contents.TrainStatus;

namespace Subway.TCMS.LanZhou.Controller.BtnActionResponser
{
    [Export]
    class B2TrainStatusActionResponser : OrdinaryActionResponser
    {
        public override void ResponseClick()
        {        
            RequestNavigateToContent(typeof(TrainStatusCommonView));
            RegionManager.RequestNavigate(RegionNames.TrainStatusContent, typeof(TrainTowStatusView).FullName);
            TowStatusButtonSelected(true);
        }
    }
}
