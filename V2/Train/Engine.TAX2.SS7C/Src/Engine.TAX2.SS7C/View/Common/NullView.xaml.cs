using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.TAX2.SS7C.View.Common
{
    /// <summary>
    /// NullView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(ViewRegionArrayDataType.Type1, new[]
    {
        RegionNames.ContentModifyTime,
        "true",
        "0",
    })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class NullView
    {
        public NullView()
        {
            InitializeComponent();
        }
    }
}
