using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Resources.String;

//.PopupView;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureOSPopupViewModel : EnsureEventPopupViewModelBase
    {
        protected override void UpdateState()
        {
            if (ATP.RegionFStateProvier.OnSightStateProvider.EnterOrQuitState != EnterOrQuit.Enter)
            {
                EnsurceContent = ATP200CStringKeys.StringEnsureQuitOSContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureOS;
                PopViewTitleContent = PopupViewStringKeys.StringQuitOnSight;
            }
            else
            {
                EnsurceContent = ATP200CStringKeys.StringEnsureOSContent;
                TitleContent = PopupViewStringKeys.StringTitleEnsureOS;
                PopViewTitleContent = PopupViewStringKeys.StringControlTypeOnSight;
            }
        }
    }
}