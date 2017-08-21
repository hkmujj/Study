using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Resources.String;

//.PopupView;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureShuntingPopupViewModel : EnsureEventPopupViewModelBase
    {
        protected override void UpdateState()
        {
            if (ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState != EnterOrQuit.Enter)
            {
                EnsurceContent = ATP200CStringKeys.StringEnsureQuitShuntingContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureQuitShunting;
                PopViewTitleContent = PopupViewStringKeys.StringQuitShunting;
            }
            else
            {
                EnsurceContent = ATP200CStringKeys.StringEnsureShuntingContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureShunting;
                PopViewTitleContent = PopupViewStringKeys.StringControlTypeShunting;
            }
        }
    }
}