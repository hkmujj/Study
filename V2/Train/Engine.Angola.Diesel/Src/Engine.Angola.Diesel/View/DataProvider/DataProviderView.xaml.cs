using Engine.Angola.Diesel.ViewModel;

namespace Engine.Angola.Diesel.View.DataProvider
{
    /// <summary>
    /// DataProviderView.xaml 的交互逻辑
    /// </summary>
    public partial class DataProviderView 
    {
        public DataProviderView(AngolaDieselShellViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
