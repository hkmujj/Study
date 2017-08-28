﻿using System;
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
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HXD3.Views.SoftWare
{
    /// <summary>
    /// SoftWareInfo.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.SoftWareInstall)]
    public partial class SoftWareInfo : UserControl
    {
        public SoftWareInfo()
        {
            InitializeComponent();
        }
    }
}