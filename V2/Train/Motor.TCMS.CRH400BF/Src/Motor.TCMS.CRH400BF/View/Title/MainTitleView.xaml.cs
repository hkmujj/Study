using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.TCMS.CRH400BF.Constant;

namespace Motor.TCMS.CRH400BF.View.Title
{
    /// <summary>
    /// MainContentTitle.xaml 的交互逻辑
    [ViewExport(RegionName = RegionNames.MainContentTitle)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainContentTitle 
    {
        public MainContentTitle()
        {
            InitializeComponent();
        }
    }
}
