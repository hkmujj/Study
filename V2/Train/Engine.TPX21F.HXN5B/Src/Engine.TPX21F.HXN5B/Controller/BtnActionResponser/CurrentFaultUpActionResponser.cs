using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class CurrentFaultUpActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            var mm = ViewModel.Value.Domain.FaultManagerViewModel.Model.CurrentFaultItems;
            mm.GotoPrePage();
        }
    }
}