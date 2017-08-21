using System.Windows.Controls;
using Engine.LCDM.HDX2.Entity.Constant;
using Engine.LCDM.HDX2.Entity.Extension;
using Engine.LCDM.HDX2.Resource;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HDX2.Entity.View.MainRegion
{
    /// <summary>
    /// SoftVersionView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class SoftVersionView : UserControl
    {
        public SoftVersionView()
        {
            InitializeComponent();

            HXD2ResourceManager.ResourceChanged += this.ResourceManagerOnResourceChanged;
        }
    }
}
