using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Attributes;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Views.TitleContentShell;

namespace Subway.WuHanLine6.Views.HelpContent
{
    /// <summary>
    /// NetWorkHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpContentRegion)]
    [ParentView((typeof(HelpTitleContentShell)))]
    public partial class NetWorkHelpView : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NetWorkHelpView()
        {
            InitializeComponent();
        }
    }
}
