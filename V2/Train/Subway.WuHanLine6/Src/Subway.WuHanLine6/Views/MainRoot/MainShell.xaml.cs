using System.Windows;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.ViewModels;

namespace Subway.WuHanLine6.Views.MainRoot
{
    /// <summary>
    /// MainShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRoot, IsDefaultView = true)]
    public partial class MainShell : UserControl
    {
        /// <summary>
        ///
        /// </summary>
        public MainShell()
        {
            InitializeComponent();
            this.IsVisibleChanged += MainShell_IsVisibleChanged;
        }

        private void MainShell_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                var data = (this.DataContext as WuHanViewModel);
                if (data != null)
                {
                    data.Controller.Load.Execute(null);
                }
            }
        }
    }
}