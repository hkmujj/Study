using System.ComponentModel.Composition;
using Subway.TCMS.LanZhou.View.Contents.ExamineAndRepair;

namespace Subway.TCMS.LanZhou.Controller.BtnActionResponser
{
    [Export]
    class B6ExamineAndRepairActionResponser : OrdinaryActionResponser
    {
        public override void ResponseClick()
        {
            RequestNavigateToContent(typeof(ExamineAndRepairLogin));
        }
    }

}
