using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.ATC.Siemens.WPF.Control.Constant;
using Urban.ATC.Siemens.WPF.Interface;

namespace Urban.ATC.Siemens.WPF.Control.View
{
    /// <summary>
    /// StartView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRoot)]
    public partial class StartView : IViewContent
    {
        public StartView()
        {
            InitializeComponent();
        }

        public void NavagateTo(string fullName)
        {

        }

        public string ViewName { get; set; }
    }
}
