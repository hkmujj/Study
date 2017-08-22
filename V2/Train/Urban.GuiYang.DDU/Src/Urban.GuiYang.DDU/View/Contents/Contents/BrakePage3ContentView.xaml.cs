﻿using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.View.Contents.Help;

namespace Urban.GuiYang.DDU.View.Contents.Contents
{
    /// <summary>
    /// BrakePage3ContentView.xaml 的交互逻辑
    /// </summary>
     [ViewExport(RegionName = RegionNames.ContentContentContent)]
     [HelpView(typeof(BrakeHelpView))]
    public partial class BrakePage3ContentView 
    {
        public BrakePage3ContentView()
        {
            InitializeComponent();
        }

       
    }
}
