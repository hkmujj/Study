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
using Subway.ShenZhenLine7.Constance;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.Views.Help
{
    /// <summary>
    /// AirConditionHelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, Priority = 1)]
    public partial class AirConditionHelpView : ITabItemInfoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public AirConditionHelpView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string HeadName => "空调";
    }
}
