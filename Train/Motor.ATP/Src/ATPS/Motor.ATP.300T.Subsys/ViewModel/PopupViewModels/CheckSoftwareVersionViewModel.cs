using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300T.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class CheckSoftwareVersionViewModel : DriverPopupViewModelBase
    {
        public CheckSoftwareVersionViewModel()
        {
            TitleContent = PopupViewStringKeys.StringCheckSoftwareVersion;
            PopupViewName = PopupContentViewNames.CheckSoftwareVersionView;
        }
    }
}