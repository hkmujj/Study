using System.Windows;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.TCMS.LanZhou.Constant;

namespace Subway.TCMS.LanZhou.View.Contents.AirCondition
{
    /// <summary>
    /// AirConditionSetting.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentContent)]
    public partial class AirConditionSetting : UserControl
    {
        public AirConditionSetting()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rbnt1.IsChecked = Rbnt2.IsChecked = Rbnt3.IsChecked = false;
            Rbnt4.IsChecked = Rbnt5.IsChecked = Rbnt6.IsChecked = false;
        }
    }
}
