using System.Windows.Controls;
using LightRail.HMI.SZLHLF.Constant;
using LightRail.HMI.SZLHLF.Extension;
using MMI.Facility.WPFInfrastructure.Behaviors;
using LightRail.HMI.SZLHLF.Resources;

namespace LightRail.HMI.SZLHLF.View.Contents
{
    /// <summary>
    /// RootContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentUpContent, IsDefaultView = true)]
    public partial class RootContentView : UserControl
    {
        public RootContentView()
        {
            InitializeComponent();

            SZLHLFResourceManager.ResourceChanged += this.ResourceChanged;
        }
    }
}
