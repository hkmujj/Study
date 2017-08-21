using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Resource.Keys;
using Subway.ShenZhenLine9.CusstomAttribute;
using Subway.ShenZhenLine9.Envets;
using Subway.ShenZhenLine9.Interfaces;
using Subway.ShenZhenLine9.Models.Units;
using Subway.ShenZhenLine9.ViewModels;

namespace Subway.ShenZhenLine9.Controller.ViewModelController
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class ShenZhenLine9Controller : ControllerBase<ShenZhenLine9ViewModel>
    {
        private readonly DispatcherTimer m_Timer;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager">区域管理器</param>
        /// <param name="eventAggregator">事件聚合器</param>
        [ImportingConstructor]
        public ShenZhenLine9Controller(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
            eventAggregator.GetEvent<NavigatorEvent>().Subscribe(NavigatorToView, ThreadOption.UIThread);
            eventAggregator.GetEvent<BoolDataChangedEvent>().Subscribe((BoolChanedAction));
            eventAggregator.GetEvent<NavigatorToKeyEvent>().Subscribe((NavigatorToKey), ThreadOption.UIThread);
            NavigatorToKeyCommand = new DelegateCommand<string>(NavigatorToKey);
            m_Timer = new DispatcherTimer();
            m_Timer.Tick += (sener, args) =>
            {
                if (ViewModel != null)
                {
                    ViewModel.CurrentTime = DateTime.Now;
                }
            };
            m_Timer.Interval = new TimeSpan(0, 0, 1);
            m_Timer.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public IStateFactory StateFactory { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand NavigatorToKeyCommand { get; private set; }
        private void NavigatorToKey(string s)
        {
            if (StateKeys.主页面 == s)
            {
                ViewModel.MasterPageViewModel.IsCheckDoor = true;
            }
            ViewModel.UpdateState(StateFactory.GetOrCreateState(s));
        }

        private void BoolChanedAction(DataChangedEventArgs<bool> e)
        {
            e.Data.UpdateIfContain(InBoolKeys.黑屏, b => ViewModel.IsStart = b);
            ViewModel.MasterPageViewModel.DoorViewModel.DoorUnits.ForEach(f => f.DataChanged(e.Data));
            ViewModel.MasterPageViewModel.AirConditionViewModel.AllAirConditionUnits.ForEach(f => f.DataChanged(e.Data));
            ViewModel.MasterPageViewModel.AssistViewModel.AssistUnits.ForEach(f => f.DataChanged(e.Data));
            ViewModel.MasterPageViewModel.EmergencyTalkViewModel.EnmergencyTalkUnits.ForEach(f => f.DataChanged(e.Data));
            ViewModel.MasterPageViewModel.BrakeViewModel.BrakeUnits.ForEach(f => f.DataChanged(e.Data));
            ViewModel.MasterPageViewModel.LCUViewModel.AllUnit.ForEach(f => f.DataChanged(e.Data));

            e.Data.UpdateIfContain(InBoolKeys.旁路信息提示, b =>
            {
                StateFactory.GetAllState().ForEach(f => f.Btn04.IsFault = b);
            });
            e.Data.UpdateIfContain(InBoolKeys.牵引封锁信息提示, b =>
            {
                StateFactory.GetAllState().ForEach(f => f.Btn05.IsFault = b);
            });
            //故障
            e.Data.Where(w => w.Value).ForEach(f => ViewModel.EventInfoViewModel.EventManagerService.HappenFault(f.Key));
            //更新旁路状态
            ViewModel.BypassViewModel.AllUnits.ForEach(f => f.DataChanged(e.Data));
            UpdateIntervalAndEvacuateDoorState(e);
            UpdateConnectedPower(e.Data);
        }

        private void UpdateConnectedPower(IDictionary<int, bool> data)
        {
            data.UpdateIfContain(InBoolKeys.母线断路器1断开, b =>
            {
                if (b)
                {
                    ViewModel.MasterPageViewModel.AssistViewModel.Generatrix1 = Generatrix.Open;
                }
            });
            data.UpdateIfContain(InBoolKeys.母线断路器1闭合, b =>
            {
                if (b)
                {
                    ViewModel.MasterPageViewModel.AssistViewModel.Generatrix1 = Generatrix.Close;
                }
            });
            data.UpdateIfContain(InBoolKeys.母线断路器2断开, b =>
            {
                if (b)
                {
                    ViewModel.MasterPageViewModel.AssistViewModel.Generatrix2 = Generatrix.Open;
                }
            });
            data.UpdateIfContain(InBoolKeys.母线断路器2闭合, b =>
            {
                if (b)
                {
                    ViewModel.MasterPageViewModel.AssistViewModel.Generatrix2 = Generatrix.Close;
                }
            });
            data.UpdateIfContain(InBoolKeys.母线断路器3断开, b =>
            {
                if (b)
                {
                    ViewModel.MasterPageViewModel.AssistViewModel.Generatrix3 = Generatrix.Open;
                }
            });
            data.UpdateIfContain(InBoolKeys.母线断路器3闭合, b =>
            {
                if (b)
                {
                    ViewModel.MasterPageViewModel.AssistViewModel.Generatrix3 = Generatrix.Close;
                }
            });
            data.UpdateIfContain(InBoolKeys.并网供电, b => ViewModel.MasterPageViewModel.AssistViewModel.PowerGrid = b);
        }

        private void UpdateIntervalAndEvacuateDoorState(DataChangedEventArgs<bool> e)
        {
            e.Data.UpdateIfContain(InBoolKeys.疏散门1未锁闭, b => ViewModel.MasterPageViewModel.DoorViewModel.EvacuateDoor1Opne = b);
            e.Data.UpdateIfContain(InBoolKeys.疏散门2未锁闭, b => ViewModel.MasterPageViewModel.DoorViewModel.EvacuateDoor2Opne = b);
            e.Data.UpdateIfContain(InBoolKeys.间隔门1未锁闭, b => ViewModel.MasterPageViewModel.DoorViewModel.IntervalDoor1Opne = b);
            e.Data.UpdateIfContain(InBoolKeys.间隔门1未锁闭, b => ViewModel.MasterPageViewModel.DoorViewModel.IntervalDoor2Opne = b);
        }
        private void NavigatorToView(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return;
            }
            var parent = GetType().Assembly.GetType(s).GetCustomAttributes(typeof(ParentAttribute), false).FirstOrDefault() as ParentAttribute;
            if (parent != null)
            {
                NavigatorToView(parent.Type.FullName);
            }
            var att = GetType().Assembly.GetType(s)?.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as ViewExportAttribute;
            if (att != null)
            {
                RegionManager.RequestNavigate(att.RegionName, s);
            }
            var borths = GetType().Assembly.GetType(s).GetCustomAttributes(typeof(BrotherAttribute), false).Cast<BrotherAttribute>().ToList();
            if (borths.Count != 0)
            {
                borths.ForEach(f => NavigatorToView(f.Type.FullName));
            }
        }

        /// <summary>
        /// 黑屏状态变化
        /// </summary>
        public void StartChanged()
        {
            if (ViewModel.IsStart)
            {
                EventAggregator.GetEvent<NavigatorToKeyEvent>().Publish(StateKeys.启动界面);
                ViewModel.AllCourceViewModel.ForEach(f => f.Value.Start());
                Task.Factory.StartNew(s =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    EventAggregator.GetEvent<NavigatorToKeyEvent>().Publish(StateKeys.主页面);
                }, null);
            }
            else
            {
                ViewModel.AllCourceViewModel.ForEach(f => f.Value.Clear());
                EventAggregator.GetEvent<NavigatorToKeyEvent>().Publish(StateKeys.黑屏);
            }
        }
        /// <summary>
        /// 区域管理器
        /// </summary>
        protected IRegionManager RegionManager { get; }
        /// <summary>
        /// 事件聚合器
        /// </summary>
        protected IEventAggregator EventAggregator { get; }
    }
}