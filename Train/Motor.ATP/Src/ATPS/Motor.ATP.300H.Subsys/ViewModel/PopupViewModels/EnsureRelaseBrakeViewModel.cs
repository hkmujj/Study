using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureRelaseBrakeViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureRelaseBrakeViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureRelaseBrake;
            EnsurceContent = PopupViewStringKeys.StringEnsureRelaseBrakeContent;
        }
    }
}