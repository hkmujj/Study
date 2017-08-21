using MMI.Facility.WPFInfrastructure.Behaviors;
using TestSubsystem.Constant;
using TestSubsystem.Interface;

namespace TestSubsystem.View
{
    /// <summary>
    /// Test1.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TabRegion)]
    public partial class Test1 : ITabControlRegion
    {
        public Test1()
        {
            InitializeComponent();
            HeaderName = "Test1";
        }

        public string HeaderName { get; private set; }
    }
}
