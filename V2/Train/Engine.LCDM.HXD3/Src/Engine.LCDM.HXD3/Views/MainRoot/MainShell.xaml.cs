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
using Engine.LCDM.HXD3.LCMDAtt;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HXD3.Views.MainRoot
{
    /// <summary>
    /// MainShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRoot,IsDefaultView = true)]
    public partial class MainShell : UserControl
    {
        public MainShell()
        {
            InitializeComponent();
        }

    }
}
