﻿using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.TCMS.CRH400BF.Constant;

namespace Motor.TCMS.CRH400BF.View.Contents.Tow.Detail
{
    /// <summary>
    /// TowCoolSystemContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentTowContent)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TowCoolSystemContentView 
    {
        public TowCoolSystemContentView()
        {
            InitializeComponent();
        }
    }
}