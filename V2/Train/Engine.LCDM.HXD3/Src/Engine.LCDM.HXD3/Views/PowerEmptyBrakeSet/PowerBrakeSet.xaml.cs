using Engine.LCDM.HXD3.Constance;
using MMI.Facility.WPFInfrastructure.Behaviors;
using System.Windows.Controls;

namespace Engine.LCDM.HXD3.Views.PowerEmptyBrakeSet
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.PowerBrakeSet,IsDefaultView=true)]
    public partial class PowerBrakeSet : UserControl
    {
        public PowerBrakeSet()
        {
            InitializeComponent();
        }
    }
}
