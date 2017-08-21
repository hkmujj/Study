using System.Windows;
using Engine._6A.Constance;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common
{
    /// <summary>
    /// MainContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class MainContentView
    {
        public MainContentView()
        {
            InitializeComponent();
            DataContextChanged += MainContentView_DataContextChanged;
        }

        void MainContentView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
