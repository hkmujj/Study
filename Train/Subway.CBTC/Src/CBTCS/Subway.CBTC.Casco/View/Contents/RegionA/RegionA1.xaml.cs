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
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.CBTC.Casco.Constant;

namespace Subway.CBTC.Casco.View.Contents.RegionA
{
    /// <summary>
    /// RegionA1.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentA1)]
    public partial class RegionA1 : UserControl
    {
        public RegionA1()
        {
            InitializeComponent();
        }
    }
}
