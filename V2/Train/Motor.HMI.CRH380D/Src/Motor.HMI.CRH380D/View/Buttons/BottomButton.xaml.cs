using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.HMI.CRH380D.Constant;

namespace Motor.HMI.CRH380D.View.Buttons
{
    /// <summary>
    /// BottomButton.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentDownButton, IsDefaultView = true)]
    public partial class BottomButton : UserControl
    {
        public BottomButton()
        {
            InitializeComponent();
        }
    }
}
