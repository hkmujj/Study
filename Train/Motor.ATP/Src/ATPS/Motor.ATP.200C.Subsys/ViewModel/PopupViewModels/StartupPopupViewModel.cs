using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;

//.PopupView;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class StartupPopupViewModel : EnsureEventPopupViewModelBase
    {
        public StartupPopupViewModel()
        {
            PopViewTitleContent = PopupViewStringKeys.StringStartUp;
            TitleContent = PopupViewStringKeys.StringStartUpDevEnsure;
            EnsurceContent = PopupViewStringKeys.StringEnsureStartUpContent;
        }
    }
}