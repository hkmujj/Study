using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;
using Subway.ShenZhenLine9.CusstomAttribute;
using Subway.ShenZhenLine9.Views.Root;

namespace Subway.ShenZhenLine9.Views.MainContent
{
    /// <summary>
    ///     BypassView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent)]
    [Parent(typeof (MainShell))]
    public partial class BypassView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public BypassView()
        {
            InitializeComponent();
        }
    }
}
