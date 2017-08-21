using System.Windows;
using System.Windows.Controls;
using Subway.WuHanLine6.GIS.Config;

namespace Subway.WuHanLine6.GIS.Views.CommonView
{
    /// <summary>
    /// StationNameView.xaml 的交互逻辑
    /// </summary>
    public partial class StationNameView : UserControl
    {
        public StationNameView()
        {
            InitializeComponent();

        }

        public static readonly DependencyProperty StationNameProperty = DependencyProperty.Register(
            "StationName", typeof(StationName), typeof(StationNameView), new PropertyMetadata(default(StationName), OnStationNameChanged));

        private static void OnStationNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((StationNameView)d).OnStationChanged();
        }

        private void OnStationChanged()
        {
            if (StationName != null)
            {
                this.DataContext = StationName;
            }
        }

        public StationName StationName { get { return (StationName)GetValue(StationNameProperty); } set { SetValue(StationNameProperty, value); } }
    }
}
