using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300T.Subsys.Constant;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
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