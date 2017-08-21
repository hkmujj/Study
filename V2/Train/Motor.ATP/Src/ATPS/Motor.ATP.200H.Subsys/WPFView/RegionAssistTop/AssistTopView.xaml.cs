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
using Motor.ATP._200H.Subsys.Constant;

namespace Motor.ATP._200H.Subsys.WPFView.RegionAssistTop
{
    /// <summary>
    /// AssistTopView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionAssistTop)]
    public partial class AssistTopView : UserControl
    {
        public AssistTopView()
        {
            InitializeComponent();
        }
    }
}
