using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Resources.Keys;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToTodayFaultActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.Domain.FaultManagerViewModel.Controller.ChangeToTodayFaults();
            NavigateTo(StateKeys.Root_故障查询24小时事件);
        }
    }
}