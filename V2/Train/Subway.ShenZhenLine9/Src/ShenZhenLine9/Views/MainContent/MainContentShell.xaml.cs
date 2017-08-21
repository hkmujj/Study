using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;
using Subway.ShenZhenLine9.CusstomAttribute;
using Subway.ShenZhenLine9.Views.Root;

namespace Subway.ShenZhenLine9.Views.MainContent
{
    /// <summary>
    ///     MainContentShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent)]
    [Parent(typeof(MainShell))]
    public partial class MainContentShell : UserControl
    {
        /// <summary>
        /// </summary>
        public MainContentShell()
        {
            InitializeComponent();
        }

      
    }
}
