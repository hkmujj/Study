using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine7.Constance;
using Subway.ShenZhenLine7.CusstomAttribute;
using Subway.ShenZhenLine7.Views.HightRegion;
using Subway.ShenZhenLine7.Views.MainContent;

namespace Subway.ShenZhenLine7.Views.SubSystem
{
    /// <summary>
    ///     BrakeView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.CarRegion)]
    [Parent(typeof (MainContentShell))]
    [Brother(typeof(HightSpeedView))]
    public partial class BrakeView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public BrakeView()
        {
            InitializeComponent();
        }
    }
}
