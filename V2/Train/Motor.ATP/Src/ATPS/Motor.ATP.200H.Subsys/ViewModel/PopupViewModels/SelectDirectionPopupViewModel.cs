using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Constant;
using Motor.ATP._200H.Subsys.Resources.String;

//.PopupView;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SelectDirectionPopupViewModel : DriverPopupViewModelBase
    {
        public SelectDirectionPopupViewModel()
        {
            PopViewTitleContent = ATP200HStringKeys.StringSelecDirectionView;
            TitleContent = PopupViewStringKeys.StringTitleSelectDirction;
            PopupViewName = PopupContentViewNames.SelecDirectionView;
        }
    }
}