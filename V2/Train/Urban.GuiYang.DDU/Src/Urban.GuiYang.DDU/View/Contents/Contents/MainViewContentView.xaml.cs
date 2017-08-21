using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.View.Help;

namespace Urban.GuiYang.DDU.View.Contents.Contents
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentContentContent, IsDefaultView = true)]
    [HelpView(typeof(MainHelpViewOne))]
    public partial class MainViewContentView
    {
        public MainViewContentView()
        {
            InitializeComponent();
        }
    }
}
