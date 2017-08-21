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
using Engine.TCMS.Turkmenistan.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.TCMS.Turkmenistan.View.Buttons
{
    /// <summary>
    /// ButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentDownButton, IsDefaultView = true)]
    public partial class ButtonView : UserControl
    {
        public ButtonView()
        {
            InitializeComponent();
            
        }
    }
}
