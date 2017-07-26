using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Constant;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCabSignalPopupViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCabSignalPopupViewModel()
        {
            PopupViewName = PopupContentViewNames.EnsureEventView;
            EnsurceContent = PopupViewStringKeys.StringEnsureCabSignalContent;
            TitleContent = PopupViewStringKeys.StringTitleEnsureCabSignal;
            PopViewTitleContent= PopupViewStringKeys.StringTitleEnsureCabSignal;
        }

        protected override void UpdateState()
        {
            EnsurceContent = ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState == EnterOrQuit.Enter
                ? PopupViewStringKeys.StringEnsureCabSignalContent
                : PopupViewStringKeys.StringEnsureQuitCabSignalContent;
        }
    }
}