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
using Engine.LCDM.HDX2.Entity.Constant;
using Engine.LCDM.HDX2.Entity.Extension;
using Engine.LCDM.HDX2.Resource;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HDX2.Entity.View.MainHeader
{
    /// <summary>
    /// MainHeaderView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainHeaderRegion)]
    public partial class MainHeaderView : UserControl
    {
        public MainHeaderView()
        {
            InitializeComponent();

            HXD2ResourceManager.ResourceChanged += this.ResourceManagerOnResourceChanged;

        }
    }
}
