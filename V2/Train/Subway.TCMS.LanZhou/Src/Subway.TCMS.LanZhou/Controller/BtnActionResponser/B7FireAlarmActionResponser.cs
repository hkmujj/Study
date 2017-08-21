using System.ComponentModel.Composition;
using Subway.TCMS.LanZhou.View.Contents;

namespace Subway.TCMS.LanZhou.Controller.BtnActionResponser
{
    [Export]
    class B7FireAlarmActionResponser : OrdinaryActionResponser
    {
        public override void ResponseClick()
        {
            RequestNavigateToContent(typeof(FireAlarm));
        }
    }
}
