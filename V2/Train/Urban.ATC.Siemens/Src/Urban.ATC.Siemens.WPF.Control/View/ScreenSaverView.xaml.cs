using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.ATC.Siemens.WPF.Control.Constant;
using Urban.ATC.Siemens.WPF.Control.ViewModel;

namespace Urban.ATC.Siemens.WPF.Control.View
{
    /// <summary>
    /// ScreenSaverView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRoot)]
    public partial class ScreenSaverView : UserControl
    {
        private SiemensData m_Data;
        public ScreenSaverView()
        {
            InitializeComponent();
            DataContextChanged += ScreenSaverView_DataContextChanged;
            Grid.MouseDown += Grid_PreviewMouseDown;
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            m_Data?.ScreenSaverCommand.Execute(null);
        }

        private void ScreenSaverView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                m_Data = e.NewValue as SiemensData;
            }
        }
    }
}
