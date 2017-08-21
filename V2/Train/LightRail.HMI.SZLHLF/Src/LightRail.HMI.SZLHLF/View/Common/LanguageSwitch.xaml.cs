using LightRail.HMI.SZLHLF.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;
using System.Windows.Controls;

namespace LightRail.HMI.SZLHLF.View.Common
{
    /// <summary>
    /// LanguageSwitch.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.LanguageSet)]
    public partial class LanguageSwitch : UserControl
    {
        public LanguageSwitch()
        {
            InitializeComponent();
        }
    }
}
