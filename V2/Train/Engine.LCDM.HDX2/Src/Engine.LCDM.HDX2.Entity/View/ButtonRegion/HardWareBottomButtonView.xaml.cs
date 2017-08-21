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
using Engine.LCDM.HDX2.Entity.Constant;
using Engine.LCDM.HDX2.Entity.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HDX2.Entity.View.ButtonRegion
{
    /// <summary>
    /// HardWareButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HardwareButtonRegion)]
    public partial class HardWareBottomButtonView : UserControl
    {
        [ImportingConstructor]
        public HardWareBottomButtonView(HardWareButtonViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
