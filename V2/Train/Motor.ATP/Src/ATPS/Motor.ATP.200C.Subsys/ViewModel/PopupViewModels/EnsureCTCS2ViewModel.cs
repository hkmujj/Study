using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Resources.String;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCTCS2ViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCTCS2ViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureCTCS2;
            EnsurceContent = ATP200CStringKeys.StringEnsureCTCS2Content;
            PopViewTitleContent= PopupViewStringKeys.StringCTCS2;
        }
    }
}