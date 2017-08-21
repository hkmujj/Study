using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Interface.Attibutes;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Events;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.MainContentContents
{
    /// <summary>
    /// BoradercastSettingView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContentContentRegion)]
    [TitleName("广播设置")]
    public partial class BoradercastSettingView
    {
        public BoradercastSettingView()
        {
            InitializeComponent();
          
        }
    }
}
