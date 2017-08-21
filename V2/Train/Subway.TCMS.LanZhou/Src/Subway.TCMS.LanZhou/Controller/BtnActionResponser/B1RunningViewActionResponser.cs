using System.ComponentModel.Composition;
using Subway.TCMS.LanZhou.View.Contents;

namespace Subway.TCMS.LanZhou.Controller.BtnActionResponser
{
    [Export]
    class B1RunningViewActionResponser : OrdinaryActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            RequestNavigateToContent(typeof(RunningView));
        }
    }
}
