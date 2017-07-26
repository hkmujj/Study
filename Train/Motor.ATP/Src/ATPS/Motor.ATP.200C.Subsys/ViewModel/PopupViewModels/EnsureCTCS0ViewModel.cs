using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Resources.String;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCTCS0ViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCTCS0ViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureCTCS0;
            EnsurceContent = ATP200CStringKeys.StringEnsureCTCS0Content;
            PopViewTitleContent = PopupViewStringKeys.StringCTCS0;
        }
    }
}