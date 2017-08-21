using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCTCS3ViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCTCS3ViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureSelectedLevel;
            EnsurceContent = PopupViewStringKeys.String300TEnsureCTCS3Content;
        }
    }
}