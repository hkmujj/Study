using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;

//.PopupView;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureShuntingPopupViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureShuntingPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureShunting;
            EnsurceContent = PopupViewStringKeys.StringEnsureShuntingContent;
        }

        protected override void UpdateState()
        {
            EnsurceContent = ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState != EnterOrQuit.Enter
                ? PopupViewStringKeys.StringEnsureQuitShuntingContent
                : PopupViewStringKeys.StringEnsureShuntingContent;
        }
    }
}