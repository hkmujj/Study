﻿using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Interface;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.HelpChildren
{
    /// <summary>
    /// HelpAssistView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpTableRegion, Priority = 1)]
    public partial class HelpAssistView : ITabItemInfoProvider
    {
        public HelpAssistView()
        {
            InitializeComponent();
        }

        public string HeadName
        {
            get { return "辅助"; }
        }


    }
}
