using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIDataDebugger.ViewModel;
using MMITool.Infrastructure;

namespace MMITool.Addin.MMIDataDebugger.View
{
    /// <summary>
    /// CommunicationDataPageView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent)]
    public partial class CommunicationDataPageView : UserControl
    {
        [ImportingConstructor]
        public CommunicationDataPageView(DataViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
