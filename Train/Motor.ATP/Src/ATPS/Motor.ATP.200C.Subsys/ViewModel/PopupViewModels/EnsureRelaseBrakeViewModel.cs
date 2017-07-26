using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureRelaseBrakeViewModel : EnsureEventPopupViewModelBase
    {
        public EnsureRelaseBrakeViewModel()
        {
            PopViewTitleContent = PopupViewStringKeys.StringRelase;
            TitleContent = PopupViewStringKeys.StringTitleEnsureRelaseBrake;
            EnsurceContent = PopupViewStringKeys.StringEnsureRelaseBrakeContent;
        }
    }
}