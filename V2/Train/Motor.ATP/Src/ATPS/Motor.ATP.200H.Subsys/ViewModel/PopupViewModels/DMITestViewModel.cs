using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.WPFView.PopupViews.Contents;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class DMITestViewModel : DriverPopupViewModelBase
    {
        public DMITestViewModel()
        {
            PopViewTitleContent = PopupViewStringKeys.StringDMITest;
            TitleContent = PopupViewStringKeys.StringDMIFuncTest;

            PopupViewName = typeof(DMITestView).FullName;
        }
    }
}