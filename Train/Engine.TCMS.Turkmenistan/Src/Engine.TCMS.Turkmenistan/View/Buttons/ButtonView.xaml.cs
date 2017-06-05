using System.Windows.Controls;
using Engine.TCMS.Turkmenistan.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.TCMS.Turkmenistan.View.Buttons
{
    /// <summary>
    /// ButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentDownButton, IsDefaultView = true)]
    public partial class ButtonView : UserControl
    {
        public ButtonView()
        {
            InitializeComponent();
            
        }
    }
}
