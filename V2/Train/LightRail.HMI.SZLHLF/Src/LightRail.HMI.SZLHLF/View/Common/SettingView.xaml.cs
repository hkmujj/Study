using System.Windows.Controls;
using LightRail.HMI.SZLHLF.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace LightRail.HMI.SZLHLF.View.Common
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentUpContent)]
    public partial class SettingView : UserControl
    {
        public SettingView()
        {
            InitializeComponent();
        }
    }
}
