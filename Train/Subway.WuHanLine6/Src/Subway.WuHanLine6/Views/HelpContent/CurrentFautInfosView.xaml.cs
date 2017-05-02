using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Attributes;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Views.Button;

namespace Subway.WuHanLine6.Views.HelpContent
{
    /// <summary>
    /// CurrentFautInfosView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.FaultContent)]
    [ParentView(typeof(FaultViewShell))]
    [BrotherView(typeof(CurrentFaultViewButton))]
    public partial class CurrentFautInfosView : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CurrentFautInfosView()
        {
            InitializeComponent();
        }
    }
}