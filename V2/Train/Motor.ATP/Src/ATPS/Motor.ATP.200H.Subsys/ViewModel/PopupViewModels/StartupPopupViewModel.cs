using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Resources.String;

//.PopupView;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class StartupPopupViewModel : EnsureEventPopupViewModelBase
    {
        public StartupPopupViewModel()
        {
            PopViewTitleContent = PopupViewStringKeys.StringTitleEnsureStartUp;
            TitleContent = PopupViewStringKeys.StringStartUpDevEnsure;
            EnsurceContent = ATP200HStringKeys.StirngStartUp;
        }
    }
}