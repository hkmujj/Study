using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace MMITool.Addin.MMIConfiguration.View
{
    /// <summary>
    /// ConfigurationRootView.xaml 的交互逻辑
    /// </summary>
    [ViewExport("ConfigurationRootView")]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ConfigurationRootView : UserControl
    {
        public ConfigurationRootView()
        {
            InitializeComponent();
        }
    }
}
