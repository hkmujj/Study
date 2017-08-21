using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SelectBrakeTestViewModel : DriverPopupViewModelBase
    {
        public SelectBrakeTestViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleSelectBrakeTest;
            PopupViewName = PopupContentViewNames.SelectBrakeTestView;
        }
    }
}