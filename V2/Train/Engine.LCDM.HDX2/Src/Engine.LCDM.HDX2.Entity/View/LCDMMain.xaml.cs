using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Constant;
using Engine.LCDM.HDX2.Entity.Extension;
using Engine.LCDM.HDX2.Resource;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HDX2.Entity.View
{
    /// <summary>
    /// LCDMMain.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RootRegion)]
    public partial class LCDMMain 
    {
        public LCDMMain()
        {
            InitializeComponent();

            HXD2ResourceManager.ResourceChanged += this.ResourceManagerOnResourceChanged;
        }
    }
}
