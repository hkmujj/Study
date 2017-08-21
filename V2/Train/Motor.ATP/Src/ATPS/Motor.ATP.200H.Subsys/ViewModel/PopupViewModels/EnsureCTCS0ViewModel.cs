using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Resources.String;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureCTCS0ViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureCTCS0ViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureCTCS0;
            EnsurceContent = ATP200HStringKeys.StringEnsureCTCS0Content;
            PopViewTitleContent = PopupViewStringKeys.StringTitleEnsureCTCS0;
        }
    }
}