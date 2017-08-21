using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Resources.String;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCTCS2ViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCTCS2ViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureCTCS2;
            EnsurceContent = ATP200HStringKeys.StringEnsureCTCS2Content;
            PopViewTitleContent= PopupViewStringKeys.StringTitleEnsureCTCS2;
        }
    }
}