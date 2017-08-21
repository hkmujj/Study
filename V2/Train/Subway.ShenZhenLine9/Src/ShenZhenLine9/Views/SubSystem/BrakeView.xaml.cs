using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;
using Subway.ShenZhenLine9.CusstomAttribute;
using Subway.ShenZhenLine9.Views.HightRegion;
using Subway.ShenZhenLine9.Views.MainContent;

namespace Subway.ShenZhenLine9.Views.SubSystem
{
    /// <summary>
    ///     BrakeView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.CarRegion)]
    [Parent(typeof(MainContentShell))]
    [Brother(typeof(BrakeCylinderView))]
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
