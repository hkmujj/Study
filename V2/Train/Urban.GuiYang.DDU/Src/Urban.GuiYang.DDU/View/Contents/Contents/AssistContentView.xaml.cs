using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.View.Contents.Help;

namespace Urban.GuiYang.DDU.View.Contents.Contents
{
    /// <summary>
    /// AssistContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentContentContent)]
    [HelpView((typeof(AssistHelpView)))]
    public partial class AssistContentView
    {
        public AssistContentView()
        {
            InitializeComponent();
        }
    }
}
