using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Motor.HMI.CRH1A.Config.Editer.ViewModel;

namespace Motor.HMI.CRH1A.Config.Editer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private EditerViewModel m_ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            m_ViewModel = new EditerViewModel();
            this.DataContext = m_ViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_ViewModel.Initalize();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            m_ViewModel.Save();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
