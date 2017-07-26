using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCTCS2ViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCTCS2ViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureSelectedLevel;
            EnsurceContent = PopupViewStringKeys.String300TEnsureCTCS2Content;
        }
    }
}