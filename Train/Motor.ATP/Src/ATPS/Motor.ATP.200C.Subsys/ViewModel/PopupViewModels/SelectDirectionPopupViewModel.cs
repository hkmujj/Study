using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SelectDirectionPopupViewModel : DriverPopupViewModelBase
    {
        public SelectDirectionPopupViewModel()
        {
            PopViewTitleContent = PopupViewStringKeys.StringFrequency;
            TitleContent = PopupViewStringKeys.StringTitleSelectDirction;
            PopupViewName = PopupContentViewNames.SelecDirectionView;
        }
    }
}