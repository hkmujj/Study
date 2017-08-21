using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;

//.PopupView;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureOSPopupViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureOSPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureOS;
            EnsurceContent = PopupViewStringKeys.StringEnsureOSContent;
        }

        protected override void UpdateState()
        {
            EnsurceContent = ATP.RegionFStateProvier.OnSightStateProvider.EnterOrQuitState != EnterOrQuit.Enter
                ? PopupViewStringKeys.StringEnsureQuitOSContent
                : PopupViewStringKeys.StringEnsureOSContent;
        }
    }
}