using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300T.Subsys.Constant;
using Motor.ATP._300T.Subsys.ViewModel.PopupViewModels;
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

namespace Motor.ATP._300T.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// ExamineSoftwareVersionViewModel.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent)]
    public partial class CheckSoftwareVersionView
    {
        [ImportingConstructor]
        public CheckSoftwareVersionView(CheckSoftwareVersionViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
