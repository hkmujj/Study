using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Engine.LCDM.HXD3.Constance;
using Engine.LCDM.HXD3.Interfaces;
using Engine.LCDM.HXD3.LCMDAtt;
using Engine.LCDM.HXD3.Views.LanguageChoose;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HXD3.Views.BottomButton
{
    /// <summary>
    /// LanguageChangeButton.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.BottomButton)]
    [Active(ControlType = typeof(LanguageSwitch))]
    public partial class LanguageChangeButton : IButtons
    {
        public LanguageChangeButton()
        {
            InitializeComponent();
            F4 = ButtonF4;
            F8 = ButtonF8;
        }

        public Button F1 { get; set; }
        public Button F2 { get; set; }
        public Button F3 { get; set; }
        public Button F4 { get; set; }
        public Button F5 { get; set; }
        public Button F6 { get; set; }
        public Button F7 { get; set; }
        public Button F8 { get; set; }
    }
}
