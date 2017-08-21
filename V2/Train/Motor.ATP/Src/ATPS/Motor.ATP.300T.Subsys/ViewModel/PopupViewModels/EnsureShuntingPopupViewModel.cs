using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Resources.Strings;

//.PopupView;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureShuntingPopupViewModel : EnsureEventPopupViewModelBase
    {
        protected override void UpdateState()
        {
            if (ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState == EnterOrQuit.Enter)
            {
                EnsurceContent = PopupViewStringKeys.StringEnsureShuntingContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureShunting;
            }
            else
            {
                EnsurceContent = PopupViewStringKeys.StringEnsureQuitShuntingContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureQuitShunting;
            }
        }
    }
}