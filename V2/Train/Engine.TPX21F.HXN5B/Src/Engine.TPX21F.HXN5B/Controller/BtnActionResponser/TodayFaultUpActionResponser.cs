using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class TodayFaultUpActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            var mm = ViewModel.Value.Domain.FaultManagerViewModel.Model.TodayFaultItems;
            mm.GotoPrePage();
        }
    }
}