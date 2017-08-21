  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Engine.LCDM.HXD3.Constance;
using Engine.LCDM.HXD3.Extentions;
using Engine.LCDM.HXD3.Resource;
using Engine.LCDM.HXD3.ViewModels;
using Engine.LCDM.HXD3.Views.MainRoot;
using Engine.LCDM.HXD3.Views.PowerEmptyBrakeSet;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HXD3.Views.Shells
{
    /// <summary>
    /// DoMainShell.xaml 的交互逻辑
    /// </summary>
    public partial class DoMainShell : UserControl
    {
        public DoMainShell()
        {
            InitializeComponent();
            Loaded += DoMainShell_Loaded;
            this.Resources.MergedDictionaries.Add(LCDMResourceManager.Instance);
            LCDMResourceManager.ResourceChanged += this.ResourceChanged;
            
        }

       
        private void DoMainShell_Loaded(object sender, RoutedEventArgs e)
        {
            RegionManager.SetRegionManager(this, ServiceLocator.Current.GetInstance<IRegionManager>());
            var viewModel= ServiceLocator.Current.GetInstance<LCDMViewModel>();
            DataContext = viewModel;
        }
    }
}
