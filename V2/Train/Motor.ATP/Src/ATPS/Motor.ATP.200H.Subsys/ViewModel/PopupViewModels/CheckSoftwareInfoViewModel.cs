using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Constant;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class CheckSoftwareInfoViewModel : DriverPopupViewModelBase
    {
        public CheckSoftwareInfoViewModel()
        {
            PopupViewName = PopupContentViewNames.CheckSoftwareVersionInfoView;
            TitleContent = PopupViewStringKeys.StringSoftwareVersionInfo;
            PopViewTitleContent = PopupViewStringKeys.StringSoftwareVersion;

            DMIVersion = "73";
            MachineVersion = "2.0.3";
        }

        public string DMIVersion { get; set; }

        public string MachineVersion { get; set; }
    }
}