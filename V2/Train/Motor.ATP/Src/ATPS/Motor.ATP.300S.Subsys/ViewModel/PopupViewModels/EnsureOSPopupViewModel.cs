using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;

//.PopupView;

namespace Motor.ATP._300S.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureOSPopupViewModel : EnsureEventPopupViewModelBase, INavigationAware
    {
        public EnsureOSPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureOS;
        }

        /// <summary>Called when the implementer has been navigated to.</summary>
        /// <param name="navigationContext">The navigation context.</param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            EnsurceContent = ATP.RegionFStateProvier.OnSightStateProvider.EnterOrQuitState == EnterOrQuit.Enter
                ? PopupViewStringKeys.StringEnsureOSContent
                : PopupViewStringKeys.StringEnsureQuitOSContent;
        }
    }
}