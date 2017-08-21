using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300T.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCabSignalPopupViewModel : EnsureEventPopupViewModelBase, INavigationAware
    {
        protected override void UpdateState()
        {
            if (ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState == EnterOrQuit.Enter)
            {
                EnsurceContent = PopupViewStringKeys.StringEnsureCabSignalContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureCabSignal;
            }
            else
            {
                EnsurceContent = PopupViewStringKeys.StringEnsureQuitCabSignalContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureQuitCabSignal;
            }
        }

    }
}