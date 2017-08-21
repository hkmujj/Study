using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Engine.LCDM.HXD3.Constance;
using Engine.LCDM.HXD3.Interfaces;
using Engine.LCDM.HXD3.LCMDAtt;
using Engine.LCDM.HXD3.Views.Flow;
using Engine.LCDM.HXD3.Views.PowerEmptyBrakeSet;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HXD3.Views.ChangeToGood
{
    /// <summary>
    /// BrakeSetButtonH.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.BottomButton)]
    [Active(ControlType = typeof(PowerBrakeSetH))]
    public partial class BrakeSetButtonH : IButtons
    {
        public BrakeSetButtonH()
        {
            InitializeComponent();
            F1 = ButtonF1;
            F2 = ButtonF2;
            F3 = ButtonF3;
            F4 = ButtonF4;
            F5 = ButtonF5;
            F6 = ButtonF6;
            F7 = ButtonF7;
            F8 = ButtonF8;
        }
        public Button F1 { get; private set; }
        public Button F2 { get; private set; }
        public Button F3 { get; private set; }
        public Button F4 { get; private set; }
        public Button F5 { get; private set; }
        public Button F6 { get; private set; }
        public Button F7 { get; private set; }
        public Button F8 { get; private set; }
    }
}
