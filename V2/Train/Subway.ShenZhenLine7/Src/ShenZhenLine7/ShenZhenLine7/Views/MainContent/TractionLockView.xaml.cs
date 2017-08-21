using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine7.Constance;
using Subway.ShenZhenLine7.CusstomAttribute;
using Subway.ShenZhenLine7.Views.Root;

namespace Subway.ShenZhenLine7.Views.MainContent
{
    /// <summary>
    ///     TractionLockView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent)]
    [Parent(typeof (MainShell))]
    public partial class TractionLockView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public TractionLockView()
        {
            InitializeComponent();
        }
    }
}
