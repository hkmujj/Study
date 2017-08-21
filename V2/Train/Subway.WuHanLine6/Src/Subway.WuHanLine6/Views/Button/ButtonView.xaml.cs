using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Constance;

namespace Subway.WuHanLine6.Views.Button
{
    /// <summary>
    /// ButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.BottomButton, IsDefaultView = true)]
    public partial class ButtonView : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ButtonView()
        {
            InitializeComponent();
        }
    }
}