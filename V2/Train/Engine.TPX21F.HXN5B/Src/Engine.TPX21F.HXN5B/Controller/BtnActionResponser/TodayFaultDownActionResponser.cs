using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class TodayFaultDownActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            var mm = ViewModel.Value.Domain.FaultManagerViewModel.Model.TodayFaultItems;
            mm.GotoNextPage();
        }
    }
}