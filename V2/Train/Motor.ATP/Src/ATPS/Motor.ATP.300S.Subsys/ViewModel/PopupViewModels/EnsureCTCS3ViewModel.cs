using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;

namespace Motor.ATP._300S.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCTCS3ViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCTCS3ViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureCTCS3;
            EnsurceContent = PopupViewStringKeys.StringEnsureCTCS3Content;
        }
    }
}