using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300T.Subsys.Constant;
using Motor.ATP.Infrasturcture.Control.Infomation;
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

namespace Motor.ATP._300T.Subsys.WPFView.RegionC
{
    /// <summary>
    /// NextControlTypeView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionC01)]
    public partial class NextControlTypeView : UserControl
    {
        public NextControlTypeView()
        {
            InitializeComponent();
        }
    }
}
