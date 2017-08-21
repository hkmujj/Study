using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Interface.Attibutes;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View
{
    /// <summary>
    /// EventInfoView.xaml 的交互逻辑
    /// </summary>

    [TitleName("事件信息")]
    [ViewExport(RegionName = RegionNames.MainRootShell, IsDefaultView = false)]
    public partial class EventInfoView
    {
        public EventInfoView()
        {
            InitializeComponent();
            TitleView.PreviewMouseDown += TitleView_PreviewMouseDown;
        }

        private void TitleView_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }
    }
}