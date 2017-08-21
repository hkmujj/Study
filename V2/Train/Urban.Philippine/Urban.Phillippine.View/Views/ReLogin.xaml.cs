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
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.Views
{
    /// <summary>
    /// ReLogin.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainShellRegion)]
    public partial class ReLogin : Window
    {
        public ReLogin()
        {
            InitializeComponent();
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            //ReLogin re = new ReLogin();
            //re.Owner = this.Parent;
            //re.ShowDialog();
            
            this.DialogResult = true;
            this.Close();
        }
        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            //ReLogin re = new ReLogin();
            //re.Owner = this.Parent;
            //re.ShowDialog();
            this.DialogResult = false;
            this.Close();
        }
    }
}
