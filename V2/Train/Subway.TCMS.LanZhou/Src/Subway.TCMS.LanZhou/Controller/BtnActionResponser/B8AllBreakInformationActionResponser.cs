using System.ComponentModel.Composition;
using Subway.TCMS.LanZhou.Model.BtnStragy;
using Subway.TCMS.LanZhou.View.Contents.BreakDownInformation;

namespace Subway.TCMS.LanZhou.Controller.BtnActionResponser
{
    [Export]
    class B8AllBreakInformationActionResponser : OrdinaryActionResponser
    {
        public override void UpdateState()
        {
            BtnStateProvider state = StateProvider;
            state.IsEnabled = true;
            state.IsSelected = Domain.Model.CurrentContentViewType == typeof(BreakDownInformationList);

            base.UpdateState();
        }

        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            var fm = Domain.FalutViewModel.Model.CurrentShowingFaultModel.AllShowingPage;
        }
    }
}