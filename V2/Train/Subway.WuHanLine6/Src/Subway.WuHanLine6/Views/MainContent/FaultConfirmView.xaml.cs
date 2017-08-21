using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Attributes;
using Subway.WuHanLine6.Constance;

namespace Subway.WuHanLine6.Views.MainContent
{
    /// <summary>
    /// FaultConfirmView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.FaultConfirm)]
    [ParentView(typeof(MainContentShell))]
    public partial class FaultConfirmView : UserControl
    {
        /// <summary>
        ///
        /// </summary>
        public FaultConfirmView()
        {
            InitializeComponent();
        }
    }
}