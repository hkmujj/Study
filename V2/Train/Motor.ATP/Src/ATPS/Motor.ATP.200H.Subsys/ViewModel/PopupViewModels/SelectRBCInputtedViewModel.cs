using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Constant;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
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