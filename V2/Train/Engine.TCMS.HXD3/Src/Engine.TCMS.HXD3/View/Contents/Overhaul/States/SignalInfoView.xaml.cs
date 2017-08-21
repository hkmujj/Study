using Engine.TCMS.HXD3.Constant;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Resource.Keys;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.TCMS.HXD3.View.Contents.Overhaul.States
{
    /// <summary>
    /// SignalInfoView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionMainContentContent)]
    public partial class SignalInfoView 
    {
        public SignalInfoView()
        {
            InitializeComponent();
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ViewChangedEvent>().Subscribe((args) =>
            {
                if (args.Id.ToString().Equals(StateKeys.Root_检修状态_状态_信号信息))
                {
                    RadioButton1.IsChecked = true;
                }
            }, ThreadOption.UIThread);
        }
    }
}
