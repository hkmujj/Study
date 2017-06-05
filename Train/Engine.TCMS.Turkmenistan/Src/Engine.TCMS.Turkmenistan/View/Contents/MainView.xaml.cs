using System.Windows.Controls;
using Engine.TCMS.Turkmenistan.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.TCMS.Turkmenistan.View.Contents
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentDownContent, IsDefaultView = true)]
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
