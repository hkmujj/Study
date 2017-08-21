using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine11.Event;
using Subway.ShenZhenLine11.ViewModels;
using Subway.ShenZhenLine11.Views.HelpChild;

namespace Subway.ShenZhenLine11.Controller
{
    [Export]
    public class HelpViewContrller : ControllerBase<HelpViewModel>
    {
        [ImportingConstructor]
        public HelpViewContrller(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            AllHelpViews = new Dictionary<string, string>();
            Navigator = new DelegateCommand<string>(NavigatorMethod);
            InitViewDic();
        }

        private void NavigatorMethod(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            if (AllHelpViews.ContainsKey(obj))
            {
                EventAggregator.GetEvent<NavigatorToEvent>().Publish(new NavigatorToEvent.NavigatorArgs()
                {
                    Names = AllHelpViews[obj],
                });
            }

        }

        private void InitViewDic()
        {
            AllHelpViews.Add("空调系统", typeof(AirConditionHelpView).FullName);
            AllHelpViews.Add("辅助电源", typeof(AssistPowerHelpView).FullName);
            AllHelpViews.Add("紧急对讲", typeof(EmergencyTalkHelpView).FullName);
            AllHelpViews.Add("制动状态", typeof(BrakeHelpView).FullName);
            AllHelpViews.Add("牵引状态", typeof(TracrtionHelpView).FullName);
            AllHelpViews.Add("烟温探测系统", typeof(SmokeHelpView).FullName);
            AllHelpViews.Add("空压机", typeof(AirPumpHelpView).FullName);
            AllHelpViews.Add("声音", typeof(SoundHelpView).FullName);
            AllHelpViews.Add("门状态", typeof(DoorHelpView).FullName);
            AllHelpViews.Add("受电弓/HSCB",typeof(PantographHSCBHelpView).FullName);
        }

        protected Dictionary<string, string> AllHelpViews { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }
        public ICommand Navigator { get; private set; }
    }
}