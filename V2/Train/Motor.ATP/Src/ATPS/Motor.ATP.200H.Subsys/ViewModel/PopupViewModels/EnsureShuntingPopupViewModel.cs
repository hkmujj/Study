using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Resources.String;

//.PopupView;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureShuntingPopupViewModel : EnsureEventPopupViewModelBase
    {
        protected override void UpdateState()
        {
            if (ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState != EnterOrQuit.Enter)
            {
                EnsurceContent = ATP200HStringKeys.StringEnsureQuitShuntingContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureQuitShunting;
                PopViewTitleContent = PopupViewStringKeys.StringTitleEnsureShunting;
            }
            else
            {
                EnsurceContent = ATP200HStringKeys.StringEnsureShuntingContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureShunting;
                PopViewTitleContent = ATP200HStringKeys.StringTitleEnsureShunting;
            }
        }
    }
}