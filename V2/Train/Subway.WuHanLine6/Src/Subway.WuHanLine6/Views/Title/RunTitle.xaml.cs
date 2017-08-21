using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Constance;

namespace Subway.WuHanLine6.Views.Title
{
    /// <summary>
    /// RunTitle.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TitleRegion)]
    public partial class RunTitle : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RunTitle()
        {
            InitializeComponent();
        }
    }
}