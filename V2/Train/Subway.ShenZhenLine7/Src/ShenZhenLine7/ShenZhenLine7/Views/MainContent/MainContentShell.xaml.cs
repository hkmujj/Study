using System.Collections.Specialized;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine7.Constance;
using Subway.ShenZhenLine7.CusstomAttribute;
using Subway.ShenZhenLine7.ViewModels;
using Subway.ShenZhenLine7.Views.Root;

namespace Subway.ShenZhenLine7.Views.MainContent
{
    /// <summary>
    ///     MainContentShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent)]
    [Parent(typeof(MainShell))]
    public partial class MainContentShell : UserControl
    {
        /// <summary>
        /// </summary>
        public MainContentShell()
        {
            InitializeComponent();
            this.Loaded += MainContentShell_Loaded;

        }

        private void MainContentShell_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<IRegionManager>().Regions[RegionNames.CarRegion].ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var data = ((ShenZhenLine7ViewModel)DataContext).Controller.StateFactory;
                var conten = e.NewItems[0].ToString();
                var key = data.GetKey(conten);
                foreach (var child in ButtonGrid.Children)
                {
                    var btn = child as RadioButton;
                    if (btn.Content.ToString() == key)
                    {
                        btn.IsChecked = true;
                    }
                }
            }
        }
    }
}
