﻿using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Attributes;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Views.MainContent;

namespace Subway.WuHanLine6.Views.Conntent
{
    /// <summary>
    /// AirConditionStatusView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion)]
    [ParentView(typeof(MainContentShell))]
    public partial class AirConditionStatusView : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AirConditionStatusView()
        {
            InitializeComponent();
        }
    }
}