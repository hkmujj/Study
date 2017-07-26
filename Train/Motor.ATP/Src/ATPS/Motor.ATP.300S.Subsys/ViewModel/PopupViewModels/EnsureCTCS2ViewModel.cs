using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;

namespace Motor.ATP._300S.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCTCS2ViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCTCS2ViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureCTCS2;
            EnsurceContent = PopupViewStringKeys.StringEnsureCTCS2Content;
        }
    }
}