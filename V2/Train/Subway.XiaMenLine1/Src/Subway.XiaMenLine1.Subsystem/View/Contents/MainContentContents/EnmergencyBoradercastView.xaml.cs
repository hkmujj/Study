using System.Windows;
using System.Windows.Input;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Interface.Attibutes;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Controls;
using Subway.XiaMenLine1.Subsystem.ViewModels;

namespace Subway.XiaMenLine1.Subsystem.View.Contents.MainContentContents
{
    /// <summary>
    /// EnmergencyBoradercastView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContentContentRegion)]
    [TitleName("紧急广播")]
    public partial class EnmergencyBoradercastView
    {
        public EnmergencyBoradercastView()
        {
            InitializeComponent();
        }
    }
}
