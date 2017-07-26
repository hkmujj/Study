using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCabSignalPopupViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCabSignalPopupViewModel()
        {
            PopupViewName = PopupContentViewNames.EnsureEventView;
            EnsurceContent = PopupViewStringKeys.StringEnsureCabSignalContent;
            TitleContent = PopupViewStringKeys.StringTitleEnsureCabSignal;
        }

        protected override void UpdateState()
        {
            EnsurceContent = ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState == EnterOrQuit.Enter
                ? PopupViewStringKeys.StringEnsureCabSignalContent
                : PopupViewStringKeys.StringEnsureQuitCabSignalContent;
        }
    }
}