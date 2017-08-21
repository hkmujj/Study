using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class TodayFaultDownActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            var mm = ViewModel.Value.Domain.FaultManagerViewModel.Model.TodayFaultItems;
            mm.GotoNextPage();
        }
    }
}