using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Resources.String;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureRelaseBrakeViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureRelaseBrakeViewModel()
        {
            PopViewTitleContent = PopupViewStringKeys.StringTitleEnsureRelaseBrake;
            TitleContent = PopupViewStringKeys.StringTitleEnsureRelaseBrake;
            EnsurceContent = ATP200HStringKeys.StringEnsureRelaseBrakeContent;
        }
    }
}