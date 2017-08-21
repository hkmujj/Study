using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.ATC.Siemens.WPF.Control.Constant;
using Urban.ATC.Siemens.WPF.Control.ViewModel;
using Urban.ATC.Siemens.WPF.Interface;

namespace Urban.ATC.Siemens.WPF.Control.View
{
    /// <summary>
    /// InputScreen.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.General)]
    public partial class InputScreen : IInputContent
    {
        public InputScreen()
        {
            InitializeComponent();
        }

        public void Sunbmit(string sPara)
        {
            ((SiemensData)(this.DataContext)).InputScreen.Submit.Execute(sPara);
        }

        public void Input(string sPara)
        {
            ((SiemensData)(this.DataContext)).InputScreen.Input.Execute(sPara);
        }
    }
}