using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.ViewModel.Domain.SecondLevel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller.Domain.SecondLevel
{
    [Export]
    public class SetWhellRController : ControllerBase<SetWhellRViewModel>
    {
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.OldR = 1161;
            ResetSetting();
        }

        public void ResetSetting()
        {
            ViewModel.Model.BindableCaretIndex = 0;
            ViewModel.Model.Text = "1250";
        }
    }
}