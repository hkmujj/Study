using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Tester.BatchDataSender.ViewModel;

namespace MMI.Tester.BatchDataSender.View
{
    /// <summary>
    /// RootView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class ShellView : UserControl
    {
        [ImportingConstructor]
        public ShellView(RootViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
