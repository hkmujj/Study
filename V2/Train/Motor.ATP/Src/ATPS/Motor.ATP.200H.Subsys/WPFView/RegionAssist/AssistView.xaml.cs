using System.Windows.Input;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._200H.Subsys.Constant;

namespace Motor.ATP._200H.Subsys.WPFView.RegionAssist
{
    /// <summary>
    /// AssistView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionAssist)]
    public partial class AssistView
    {
        public AssistView()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
