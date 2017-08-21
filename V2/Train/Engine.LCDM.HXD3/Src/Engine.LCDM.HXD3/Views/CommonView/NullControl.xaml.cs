using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using Engine.LCDM.HXD3.Constance;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HXD3.Views.CommonView
{
    /// <summary>
    /// NullControl.xaml 的交互逻辑
    /// </summary>
    [ViewExport(ViewRegionArrayDataType.Type1, new string[]
    {
        RegionNames.LanguageSet, "true", "0", RegionNames.SoftWareInstall, "true", "0" ,
        RegionNames.TrainNumbSetting,"true","0",RegionNames.PowerBrakeSet,"true","0",
        RegionNames.DateSetting,"true","0"
    })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class NullControl : UserControl
    {
        public NullControl()
        {
            InitializeComponent();
        }
    }
}
