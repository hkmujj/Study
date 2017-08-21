using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.TCMS.CRH400BF.Constant;

namespace Motor.TCMS.CRH400BF.View.Contents
{
    /// <summary>
    /// MainEquipmentCutOffContentView.xaml 的交互逻辑
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewExport(RegionName = RegionNames.MainContentContent)]
    public partial class MainEquipmentCutOffContentView 
    {
        public MainEquipmentCutOffContentView()
        {
            InitializeComponent();
        }
    }
}
