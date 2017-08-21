using System.ComponentModel.Composition;
using System.Windows.Controls;
using Engine.Angola.Diesel.Constant;
using Engine.Angola.Diesel.ViewModel;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.Angola.Diesel.View.Shell
{
    /// <summary>
    /// AngolaDieselShellContent.xaml 的交互逻辑
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.ShellContentRegion)]
    public partial class AngolaDieselShellContent : UserControl
    {
        [ImportingConstructor]
        public AngolaDieselShellContent(AngolaDieselShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
