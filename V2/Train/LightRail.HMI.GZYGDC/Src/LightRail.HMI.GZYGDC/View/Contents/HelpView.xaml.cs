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
using LightRail.HMI.GZYGDC.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace LightRail.HMI.GZYGDC.View.Contents
{
    /// <summary>
    /// HelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.CenterContent)]
    public partial class HelpView : UserControl
    {
        public HelpView()
        {
            InitializeComponent();
        }
    }
}
