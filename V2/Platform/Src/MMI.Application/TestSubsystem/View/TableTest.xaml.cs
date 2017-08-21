using MMI.Facility.WPFInfrastructure.Behaviors;
using TestSubsystem.Constant;


namespace TestSubsystem.View
{
    /// <summary>
    /// TableTest.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.SubViewContent)]
    public partial class TableTest
    {
        public TableTest()
        {
            InitializeComponent();
            HeaderName = "string";
        }

        public string HeaderName { get; private set; }

    }
}
