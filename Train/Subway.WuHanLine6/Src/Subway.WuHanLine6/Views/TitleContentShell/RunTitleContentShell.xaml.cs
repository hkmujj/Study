using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Attributes;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Views.MainRoot;

namespace Subway.WuHanLine6.Views.TitleContentShell
{
    /// <summary>
    /// RunTitleContentShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TitleContentRegion)]
    [ParentView(typeof(MainShell))]
    public partial class RunTitleContentShell : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RunTitleContentShell()
        {
            InitializeComponent();
        }
    }
}