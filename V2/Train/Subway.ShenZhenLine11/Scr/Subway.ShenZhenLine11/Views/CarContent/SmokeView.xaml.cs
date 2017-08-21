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
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.ShenZhenAttribute;
using Subway.ShenZhenLine11.Views.MainMaster;

namespace Subway.ShenZhenLine11.Views.CarContent
{
    /// <summary>
    /// SmokeView.xaml 的交互逻辑
    /// </summary>
    [TitleName(Name = "烟火状态")]
    [SuperiorNavigator(Type = typeof(MainMasterShell))]
    [ViewExport(RegionName = RegionNames.CarContent)]
    public partial class SmokeView : UserControl
    {
        public SmokeView()
        {
            InitializeComponent();
        }
    }
}
