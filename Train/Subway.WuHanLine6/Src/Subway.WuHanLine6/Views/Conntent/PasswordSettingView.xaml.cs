﻿using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.WuHanLine6.Attributes;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Views.MainContent;

namespace Subway.WuHanLine6.Views.Conntent
{
    /// <summary>
    ///     PasswordSettingView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion)]
    [ParentView(typeof (MainContentShell))]
    public partial class PasswordSettingView : UserControl
    {
        /// <summary>
        /// </summary>
        public PasswordSettingView()
        {
            InitializeComponent();
        }
    }
}
