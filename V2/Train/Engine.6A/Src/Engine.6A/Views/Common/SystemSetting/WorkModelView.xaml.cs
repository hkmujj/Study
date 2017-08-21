using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.SystemSetting
{
    /// <summary>
    /// WorkModelView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.SystemTabTabRegion, Priority = 0)]
    public partial class WorkModelView : ITabItemInfoProvider
    {
        public WorkModelView()
        {
            InitializeComponent();
        }

        public string HeadName
        {
            get { return "工作模式"; }
        }
    }
}
