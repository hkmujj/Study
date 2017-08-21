using System.Windows.Controls;
using System.Windows.Input;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Tram.CBTC.Casco.Constant;

namespace Tram.CBTC.Casco.View.Contents.RegionE
{
    /// <summary>
    /// RegionE1Layout.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionE1)]
    public partial class RegionE1Layout : UserControl
    {
        public RegionE1Layout()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Popup.IsOpen = !Popup.IsOpen;
        }
    }
}
