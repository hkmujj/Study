using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Attributes;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Views.TitleContentShell;

namespace Subway.WuHanLine6.Views.MainContent
{
    /// <summary>
    /// MainContentShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent)]
    [ParentView(typeof(RunTitleContentShell))]
    public partial class MainContentShell : UserControl
    {
        /// <summary>
        ///构造函数
        /// </summary>
        public MainContentShell()
        {
            InitializeComponent();
        }
    }
}