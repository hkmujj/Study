using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Resources;
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
using Engine.LCDM.HDX2.Entity.Constant;
using Engine.LCDM.HDX2.Entity.Extension;
using Engine.LCDM.HDX2.Entity.ViewModel;
using Engine.LCDM.HDX2.Resource;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HDX2.Entity.View
{
    /// <summary>
    /// LCDMRootView.xaml 的交互逻辑
    /// </summary>
    //[Export]
    [ViewExport(RegionName = RegionNames.WholeRegion)]
    public partial class LCDMRootView
    {
        [ImportingConstructor]
        public LCDMRootView(HXD2ViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

            HXD2ResourceManager.ResourceChanged += this.ResourceManagerOnResourceChanged;
        }

    }
}
