using System.ComponentModel.Composition;
using Subway.TCMS.LanZhou.Model.BtnStragy;
using Subway.TCMS.LanZhou.Resources.Strings;
using Subway.TCMS.LanZhou.View.Contents.BreakDownInformation;

namespace Subway.TCMS.LanZhou.Controller.BtnActionResponser
{
    [Export]
    class B4BreakDownActionResponser : OrdinaryActionResponser
    {

        public override void ResponseClick()
        {
            RequestNavigateToContent(StateInterfaceKeys.Root_全部故障, typeof (BreakDownInformationList));
        }
    }
}
