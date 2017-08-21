using System.Windows.Controls;
using LightRail.HMI.SZLHLF.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace LightRail.HMI.SZLHLF.View.Contents
{
    /// <summary>
    /// ContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentUp)]
    public partial class ContentView : UserControl
    {
        public ContentView()
        {
            InitializeComponent();
        }
    }
}
