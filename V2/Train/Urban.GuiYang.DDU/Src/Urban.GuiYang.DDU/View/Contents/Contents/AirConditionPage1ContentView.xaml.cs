
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.View.Contents.Help;

namespace Urban.GuiYang.DDU.View.Contents.Contents
{
    /// <summary>
    /// AirConditionPage1ContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentContentContent)]
    [HelpView(typeof(AirConditionHelpView))]
    public partial class AirConditionPage1ContentView : UserControl
    {
        public AirConditionPage1ContentView()
        {
            InitializeComponent();
            
        }
    }
}
