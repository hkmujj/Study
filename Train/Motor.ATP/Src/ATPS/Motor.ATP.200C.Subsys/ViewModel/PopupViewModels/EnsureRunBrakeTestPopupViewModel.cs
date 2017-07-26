using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Constant;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureRunBrakeTestPopupViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureRunBrakeTestPopupViewModel()
        {
            PopupViewName = PopupContentViewNames.EnsureEventView;
            EnsurceContent = PopupViewStringKeys.StringContentEnsureRunBrakeTest;
            TitleContent = PopupViewStringKeys.StringTitleEnsureRunBrakeTest;
        }
    }
}