﻿using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine9.Constance;
using Subway.ShenZhenLine9.CusstomAttribute;
using Subway.ShenZhenLine9.Views.HightRegion;
using Subway.ShenZhenLine9.Views.MainContent;

namespace Subway.ShenZhenLine9.Views.SubSystem
{
    /// <summary>
    ///     EmergencyTalkView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.CarRegion)]
    [Parent(typeof (MainContentShell))]
    [Brother(typeof(HightSpeedView))]
    public partial class EmergencyTalkView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public EmergencyTalkView()
        {
            InitializeComponent();
        }
    }
}