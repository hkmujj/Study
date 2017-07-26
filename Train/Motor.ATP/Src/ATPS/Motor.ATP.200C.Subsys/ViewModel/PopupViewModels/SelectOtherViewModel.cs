using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Constant;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SelectOtherViewModel : DriverPopupViewModelBase
    {
        public SelectOtherViewModel()
        {
            TitleContent = PopupViewStringKeys.StringOtherFuncSelect;
            PopViewTitleContent = PopupViewStringKeys.StringOther;
            PopupViewName = PopupContentViewNames.SelectOtherView;
        }
    }
}