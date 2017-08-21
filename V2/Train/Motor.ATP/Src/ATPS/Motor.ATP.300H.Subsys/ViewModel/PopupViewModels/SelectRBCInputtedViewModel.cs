using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SelectRBCInputtedViewModel : DriverPopupViewModelBase
    {
        public SelectRBCInputtedViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleInputingRBCNumber;
            PopupViewName = PopupContentViewNames.SelectRBCInputtedView;
        }
    }
}