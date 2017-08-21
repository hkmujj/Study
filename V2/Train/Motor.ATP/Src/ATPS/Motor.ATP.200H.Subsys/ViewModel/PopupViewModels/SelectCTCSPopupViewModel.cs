using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SelectCTCSPopupViewModel : DriverPopupViewModelBase
    {
        public SelectCTCSPopupViewModel()
        {
            PopViewTitleContent = PopupViewStringKeys.StringTitleSelectCTCS;
            TitleContent = PopupViewStringKeys.StringTitleSelectCTCS;
            PopupViewName = PopupContentViewNames.SelecCTCSView;
        }
    }
}