
using System.Windows.Controls;
using Engine.LCDM.HXD3.Constance;
using Engine.LCDM.HXD3.Interfaces;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Engine.LCDM.HXD3.LCMDAtt;
using Engine.LCDM.HXD3.Views.Flow;


namespace Engine.LCDM.HXD3.Views.BottomButton
{
    /// <summary>
    /// MainBottomButton.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.BottomButton, IsDefaultView = true)]
    //[Active(ControlType = typeof(FlowView))]
    [Deactive(RegionName = RegionNames.PowerBrakeSet)]
    [Deactive(RegionName = RegionNames.LanguageSet)]
    [Deactive(RegionName = RegionNames.SoftWareInstall)]
    [Deactive(RegionName = RegionNames.TrainNumbSetting)]
    [Deactive(RegionName=RegionNames.DateSetting)]
    public partial class MainBottomButton : IButtons
    {
        public MainBottomButton()
        {
            InitializeComponent();
            F3 = ButtonF3;
            F7 = ButtonF7;
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
