using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Constant;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class CheckSoftwareInfoViewModel : DriverPopupViewModelBase
    {
        public CheckSoftwareInfoViewModel()
        {
            PopupViewName = PopupContentViewNames.CheckSoftwareVersionInfoView;
            TitleContent = PopupViewStringKeys.StringSoftwareVersionInfo;
            PopViewTitleContent = PopupViewStringKeys.StringSoftwareVersion;

            DMIVersion = "V01-23";
            MachineVersion = "v2.9.2.0";
        }

        public string DMIVersion { get; set; }

        public string MachineVersion { get; set; }
    }
}