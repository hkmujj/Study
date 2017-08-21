using System.Windows.Controls;
using System.Windows.Input;
using LightRail.HMI.SZLHLF.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace LightRail.HMI.SZLHLF.View.Common
{
    /// <summary>
    /// MalfunctionInfoView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentUpContent)]
    public partial class MalfunctionInfoView : UserControl
    {
        public MalfunctionInfoView()
        {
            InitializeComponent();
        }

        private void MalfunctionInfoView_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
