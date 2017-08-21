using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using Subway.ShenZhenLine11.ViewModels;

namespace Subway.ShenZhenLine11.Views.Shell
{
    /// <summary>
    /// DoMainShell.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(DoMainShell))]
    public partial class DoMainShell : UserControl
    {
        [ImportingConstructor]
        public DoMainShell(ShenZhenViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
