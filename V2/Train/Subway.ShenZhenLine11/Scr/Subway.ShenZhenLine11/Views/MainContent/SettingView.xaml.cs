using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.ShenZhenAttribute;
using Subway.ShenZhenLine11.Views.MainRoot;
using System.Windows.Controls;

namespace Subway.ShenZhenLine11.Views.MainContent
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent)]
    [SuperiorNavigator(Type = typeof(MainShell))]
    [TitleName(Name = "设置")]
    public partial class SettingView : UserControl
    {
        public SettingView()
        {
            InitializeComponent();
        }
    }
}