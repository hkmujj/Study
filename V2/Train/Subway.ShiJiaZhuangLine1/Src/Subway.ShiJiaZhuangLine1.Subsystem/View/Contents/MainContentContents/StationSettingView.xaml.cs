using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.Controls;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.MainContentContents
{
    /// <summary>
    /// StationSettingView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContentContentRegion)]
    [TitleName("站点设置")]
    public partial class StationSettingView
    {

        public StationSettingView()
        {
            InitializeComponent();
        }
    }
}
