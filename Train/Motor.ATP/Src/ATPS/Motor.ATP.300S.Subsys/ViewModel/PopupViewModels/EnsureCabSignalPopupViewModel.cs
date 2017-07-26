using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300S.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300S.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCabSignalPopupViewModel : EnsureEventPopupViewModelBase, INavigationAware
    {
        public EnsureCabSignalPopupViewModel()
        {
            PopupViewName = PopupContentViewNames.EnsureEventView;
            TitleContent = PopupViewStringKeys.StringTitleEnsureCabSignal;

        }

        /// <summary>Called when the implementer has been navigated to.</summary>
        /// <param name="navigationContext">The navigation context.</param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            EnsurceContent = ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState == EnterOrQuit.Enter
                ? PopupViewStringKeys.StringEnsureCabSignalContent
                : PopupViewStringKeys.StringEnsureQuitCabSignalContent;
        }

    }
}