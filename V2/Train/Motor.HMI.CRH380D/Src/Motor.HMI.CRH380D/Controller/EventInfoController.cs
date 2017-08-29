using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Interfaces;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.ViewModel;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class EventInfoController : ControllerBase<EventInfoModel>
    {
        [ImportingConstructor]
        public EventInfoController()
        {
            
        }

        public override void Initalize()
        {
            if (ViewModel.EventManagerService != null)
            {
                ViewModel.EventManagerService.CurrentEventChanged += EventChanged;
            }

            EventChanged();
            LastCommand = new DelegateCommand(OnLastPage);
            NextCommand = new DelegateCommand(OnNextPage);
            EnterCommand = new DelegateCommand(OnConfirmEvent);
        }

        public void EventChanged()
        {
            UpdateDisplayItems();
        }

        /// <summary>
        /// 更新显示
        /// </summary>
        private void UpdateDisplayItems()
        {
            if (ViewModel.EventManagerService == null)
            {
                return;
            }

            IEnumerable<IEventInfo> PageInfo = null;

            PageInfo = ViewModel.EventManagerService.GetPageInfo();
            
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                ViewModel.EventInfoDisplayItems.Clear();
                ViewModel.CurPageNum = ViewModel.EventManagerService.CurrentPage;
                ViewModel.MaxPageNum = ViewModel.EventManagerService.MaxPage;
                if (PageInfo != null)
                {
                    ViewModel.EventInfoDisplayItems.AddRange(GetInfos(PageInfo));
                }

                //确认事件有优先级，所以可能会随时更新
                //if (ViewModel.WaitConfirmEvent == null)
                {
                    ViewModel.WaitConfirmEvent =
                        ViewModel.EventManagerService.GetUnConfirmEvent(false);
                }
                
                UpdateButtonEnable();
                SelectEvent();
            }));
        }

        /// <summary>
        /// 当前需要显示的列表的编号
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        private IEnumerable<IEventInfo> GetInfos(IEnumerable<IEventInfo> infos)
        {
            var tmp = infos.OrderByDescending(o => o.HappenTime).ToList();
            var count = tmp.Count();
            var PageNum = ViewModel.EventManagerService.GetMaxPageNum();
            for (var i = 0; i < 10; ++i)
            {
                if (i < count)
                {
                    tmp[i].Number = (ViewModel.CurPageNum - 1) * PageNum + i + 1;
                }
            }
            return tmp;
        }

        public ICommand LastCommand { get; private set; }

        public ICommand NextCommand { get; private set; }

        /// <summary>
        /// 上一页
        /// </summary>
        public void OnLastPage()
        {
            ViewModel.EventManagerService.LastPage();

            UpdateDisplayItems();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void OnNextPage()
        {
            ViewModel.EventManagerService.NextPage();

            UpdateDisplayItems();
        }

        public ICommand EnterCommand { get; private set; }
        /// <summary>
        /// 确认事件
        /// </summary>
        public void OnConfirmEvent()
        {
            if (ViewModel.EventManagerService == null)
            {
                return;
            }

            //先更新已经确认的事件，然后再显示后续未确认的事件
            if (ViewModel.WaitConfirmEvent != null)
            {
                ViewModel.EventManagerService.ConfirmFault(ViewModel.WaitConfirmEvent);
            }

            ViewModel.WaitConfirmEvent = ViewModel.EventManagerService.GetUnConfirmEvent(true);

            UpdateDisplayItems();
        }
        
        /// <summary>
        /// 选中事件
        /// </summary>
        public void SelectEvent()
        {
            ViewModel.SelectEvent =
                ViewModel.EventInfoDisplayItems.FirstOrDefault(f => f.IsChecked);
        }
        
        /// <summary>
        /// 更新警报汇总子系统状态
        /// </summary>
        public void UpdateButtonEnable()
        {
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn1Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.高压);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn2Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.牵引);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn3Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.车门);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn4Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.控制和通讯);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn5Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.辅助供电);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn6Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.制动);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn7Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.空调);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn8Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.前部);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn9Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.蓄电池供电);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn10Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.供风);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn11Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.卫生设备);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn12Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.转向架);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn13Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.信息系统);
            DomainViewModel.Instacnce.Model.WarringSumMenuModel.Btn14Enable = ViewModel.EventManagerService.GetEventSystemStatus(EventSystemState.火灾探测器);
        }

        /// <summary>
        /// 设置当前事件页面（故障/警报）
        /// </summary>
        /// <param name="levl"></param>
        public void SetEventPageState(EventPageState eventPageState)
        {
            ViewModel.EventManagerService.SetEventPageState(eventPageState);
        }

        /// <summary>
        /// 设置当前故障所属系统（主要针对警报）  备注：此时警报从所有发生过的故障中查询，后期根据具体需求调整
        /// </summary>
        /// <param name="system"></param>
        public void SetEventSystem(EventSystemState system)
        {
            ViewModel.EventManagerService.SetEventSystem(system);
        }

        /// <summary>
        /// 设置当前故障或者历史故障
        /// </summary>
        public void SetHistoryOrCurrent(bool temp)
        {
            ViewModel.EventManagerService.SetHistoryOrCurrent(temp);
        }

        /// <summary>
        /// 进入故障报告、当前故障、警报记录、当前警报页面时，需要提前更新数据
        /// </summary>
        public void Update()
        {
            UpdateDisplayItems();
        }
    }
}