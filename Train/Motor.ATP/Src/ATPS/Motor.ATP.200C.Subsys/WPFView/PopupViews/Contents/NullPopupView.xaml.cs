using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._200C.Subsys.Constant;

namespace Motor.ATP._200C.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// NullPopupView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent, IsDefaultView = true)]
    public partial class NullPopupView
    {
        public NullPopupView()
        {
            InitializeComponent();
        }
    }
}
