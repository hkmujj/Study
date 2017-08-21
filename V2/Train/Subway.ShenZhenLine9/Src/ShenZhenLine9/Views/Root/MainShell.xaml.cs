using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;

namespace Subway.ShenZhenLine9.Views.Root
{
    /// <summary>
    ///     MainShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.Root)]
    public partial class MainShell : UserControl
    {
        /// <summary>
        /// </summary>
        public MainShell()
        {
            InitializeComponent();
        }
    }
}
