using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.YdConfigCreater.ViewModel;

namespace MMITool.Addin.YdConfigCreater.View
{
    /// <summary>
    /// ShellView.xaml 的交互逻辑
    /// </summary>
    [ViewExport("ShellView")]
    public partial class ShellView 
    {
        [ImportingConstructor]
        public ShellView(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
