using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.View
{
    /// <summary>
    /// SelectExePathView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = ConfigurationRegionNames.SelectFileRegion)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SelectExePathView
    {
        private readonly SelectExePathViewModel m_SelectExePathViewModel;

        [ImportingConstructor]
        public SelectExePathView(SelectExePathViewModel viewModel)
        {
            m_SelectExePathViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        //todo move to Controller command
        private void Validation_OnError(object sender, ValidationErrorEventArgs e)
        {
            m_SelectExePathViewModel.Model.HasValidationError = true;
        }
    }
}
