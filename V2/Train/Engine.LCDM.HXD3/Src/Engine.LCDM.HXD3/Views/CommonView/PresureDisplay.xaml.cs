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
using Engine.LCDM.HXD3.Constance;

namespace Engine.LCDM.HXD3.Views.CommonView
{
    /// <summary>
    /// PresureDisplay.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.Pressure, IsDefaultView = true)]
    public partial class PresureDisplay : UserControl
    {
        public PresureDisplay()
        {
            InitializeComponent();
        }
    }
}
