using System.Windows;
using Urban.ATC.Siemens.WPF.Control.ViewModel;

namespace Urban.ATC.Siemens.WPF.Control.View
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage
    {
        private SiemensData data;
        public MainPage()
        {
            InitializeComponent();
            DataContextChanged += MainPage_DataContextChanged;
            Grid.MouseDown += Grid_PreviewMouseDown;
            
           
        }

        private void Grid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!e.Handled)
            {
                data.OperationCommand.Execute(null);
            }
        }

        private void MainPage_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                data = e.NewValue as SiemensData;
            }
        }





    }
}