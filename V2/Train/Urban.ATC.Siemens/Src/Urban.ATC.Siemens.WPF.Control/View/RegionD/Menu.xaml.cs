using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.ATC.Siemens.WPF.Control.Constant;

namespace Urban.ATC.Siemens.WPF.Control.View.RegionD
{
    /// <summary>
    /// Menu.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.Menu)]
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }
    }
}