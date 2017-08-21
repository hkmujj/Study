using System.ComponentModel.Composition;
using Subway.TCMS.LanZhou.View.Contents.AirCondition;

namespace Subway.TCMS.LanZhou.Controller.BtnActionResponser
{
    [Export]
    class B3AirConditionActionResponser : OrdinaryActionResponser
    {
        public override void ResponseClick()
        {
            RequestNavigateToContent(typeof(AirConditionSetting));
        }
    }
}
