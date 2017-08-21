using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;

//.PopupView;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureOSPopupViewModel : EnsureEventPopupViewModelBase
    {
        protected override void UpdateState()
        {
            if (ATP.RegionFStateProvier.OnSightStateProvider.EnterOrQuitState == EnterOrQuit.Enter)
            {
                EnsurceContent = PopupViewStringKeys.StringEnsureOSContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureOS;
            }
            else
            {
                EnsurceContent = PopupViewStringKeys.StringEnsureQuitOSContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureQuitOS;
            }
        }
    }
}