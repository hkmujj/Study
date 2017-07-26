using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SelectCTCSPopupViewModel : DriverPopupViewModelBase
    {
        public SelectCTCSPopupViewModel()
        {
            PopViewTitleContent = PopupViewStringKeys.StringLevel;
            TitleContent = PopupViewStringKeys.StringTitleSelectCTCS;
            PopupViewName = PopupContentViewNames.SelecCTCSView;
        }
    }
}