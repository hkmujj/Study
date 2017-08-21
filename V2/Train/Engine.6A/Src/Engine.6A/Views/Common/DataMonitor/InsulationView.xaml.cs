using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Engine._6A.Constance;
using Engine._6A.Interface;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.DataMonitor
{
    /// <summary>
    /// InsulationView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(ViewRegionArrayDataType.Type1, new string[] { RegionNames.DataMonitorTabRegion, "true", "2", RegionNames.Axle8DataMonitorTabRegion, "false", "2" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class InsulationView : UserControl, ITabItemInfoProvider
    {
        public InsulationView()
        {
            InitializeComponent();
        }

        public string HeadName { get { return "绝 缘"; } }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var sd = sender as FrameworkElement;
            if (sd.DataContext != null)
            {

            }
        }
    }
}
