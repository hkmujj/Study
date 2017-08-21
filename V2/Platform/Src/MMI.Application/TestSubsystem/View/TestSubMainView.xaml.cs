
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using TestSubsystem.ViewModel;

namespace TestSubsystem.View
{
    /// <summary>
    /// TestSubMainView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class TestSubMainView
    {
        [ImportingConstructor]
        public TestSubMainView(TestSubMainViewModel viewModel, IRegionManager regionManager)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
