using System.Windows;
using Engine._6A.Constance;
using Engine._6A.Interface.ViewModel;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common
{
    /// <summary>
    /// StartingView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class StartingView
    {
        public StartingView()
        {
            InitializeComponent();
            IsVisibleChanged += StartingView_IsVisibleChanged;
        }

        void StartingView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && !(bool)e.OldValue && Starting.DataContext != null)
            {
                var tmp = Starting.DataContext as IStartingViewModel;
                if (tmp != null)
                {
                    tmp.ViewDispaly.Execute(null);
                }
            }
        }
    }
}
