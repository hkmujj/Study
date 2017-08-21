using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.ShenZhenAttribute;
using Subway.ShenZhenLine11.Views.MainRoot;

namespace Subway.ShenZhenLine11.Views.MainContent
{
    /// <summary>
    /// ReSetView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent)]
    [SuperiorNavigator(Type = typeof(MainShell))]
    [TitleName(Name = "复位设置")]
    public partial class ReSetView : UserControl
    {
        public ReSetView()
        {
            InitializeComponent();
        }
    }
}
