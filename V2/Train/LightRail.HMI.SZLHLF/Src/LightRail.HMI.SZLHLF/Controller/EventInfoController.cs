using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using LightRail.HMI.SZLHLF.Interface;
using LightRail.HMI.SZLHLF.Model.EventModel;
using LightRail.HMI.SZLHLF.ViewModel;
using Microsoft.Practices.Prism;
using MMI.Facility.WPFInfrastructure.Interfaces;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export]
    public class EventInfoController : ControllerBase<Lazy<EventInfoViewModel>>
    {
        [ImportingConstructor]
        public EventInfoController(Lazy<EventInfoViewModel> viewModel)
            : base(viewModel)
        {
            
        }

        public override void Initalize()
        {
            if (ViewModel.Value.Model.EventManagerService != null)
            {
                ViewModel.Value.Model.EventManagerService.CurrentEventChanged += EventChanged;
            }

            EventChanged();
            LastCommand = new DelegateCommand(OnLastPage);
            NextCommand = new DelegateCommand(OnNextPage);
            EnterCommand = new DelegateCommand(OnConfirmEvent);
            HistoryOrCurrentCommand = new DelegateCommand(OnHistoryOrCurrent);
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
            if (ViewModel.Value.Model.EventManagerService == null)
            {
                return;
            }

            IEnumerable<IEventInfo> PageInfo = null;

            PageInfo = ViewModel.Value.Model.EventManagerService.GetPageInfo();
            ViewModel.Value.Model.TotalCount = ViewModel.Value.Model.EventManagerService.GetTotalCount();

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                ViewModel.Value.Model.EventInfoDisplayItems.Clear();
                ViewModel.Value.Model.CurPageNum = ViewModel.Value.Model.EventManagerService.CurrentPage;
                ViewModel.Value.Model.MaxPageNum = ViewModel.Value.Model.EventManagerService.MaxPage;
                ViewModel.Value.Model.EventInfoDisplayItems.AddRange(GetInfos(PageInfo));
                ViewModel.Value.Model.CurEventLevel = ViewModel.Value.Model.EventManagerService.CurrentLevel;

                ViewModel.Value.Model.UnConfirmEventCount =
                    ViewModel.Value.Model.EventManagerService.GetUnConfirmEventCount;

                if (ViewModel.Value.Model.WaitConfirmEvent == null)
                {
                    ViewModel.Value.Model.WaitConfirmEvent =
                        ViewModel.Value.Model.EventManagerService.GetUnConfirmEvent();
                }
                else
                {
                    ViewModel.Value.Model.UnConfirmEventCount =
                        ViewModel.Value.Model.EventManagerService.GetUnConfirmEventCount + 1;
                }

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
            var PageNum = ViewModel.Value.Model.EventManagerService.GetMaxPageNum();
            for (var i = 0; i < 10; ++i)
            {
                if (i < count)
                {
                    tmp[i].Number = (ViewModel.Value.Model.CurPageNum - 1) * PageNum + i + 1;
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
            ViewModel.Value.Model.EventManagerService.LastPage();

            UpdateDisplayItems();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void OnNextPage()
        {
            ViewModel.Value.Model.EventManagerService.NextPage();

            UpdateDisplayItems();
        }

        public ICommand EnterCommand { get; private set; }
        /// <summary>
        /// 确认事件
        /// </summary>
        public void OnConfirmEvent()
        {
            if (ViewModel.Value.Model.EventManagerService == null)
            {
                return;
            }

            //先更新已经确认的事件，然后再显示后续未确认的事件
            if (ViewModel.Value.Model.WaitConfirmEvent != null)
            {
                ViewModel.Value.Model.EventManagerService.ConfirmFault(ViewModel.Value.Model.WaitConfirmEvent);
            }

            ViewModel.Value.Model.UnConfirmEventCount = ViewModel.Value.Model.EventManagerService.GetUnConfirmEventCount;
            ViewModel.Value.Model.WaitConfirmEvent = ViewModel.Value.Model.EventManagerService.GetUnConfirmEvent();

            UpdateDisplayItems();
        }
        
        private EventLevel m_EventLevel;
        /// <summary>
        /// 设置事件等级
        /// </summary>
        public EventLevel EventLevel
        {
            get { return m_EventLevel; }
            set
            {
                if (value == m_EventLevel)
                {
                    return;
                }
                m_EventLevel = value;
                ViewModel.Value.Model.EventManagerService.SetEveltLevel(m_EventLevel);
                UpdateDisplayItems();
                RaisePropertyChanged(() => EventLevel);
            }
        }

        /// <summary>
        /// 选中事件
        /// </summary>
        public void SelectEvent()
        {
            ViewModel.Value.Model.SelectEvent =
                ViewModel.Value.Model.EventInfoDisplayItems.FirstOrDefault(f => f.IsChecked);
        }

        public ICommand HistoryOrCurrentCommand { get; private set; }

        private void OnHistoryOrCurrent()
        {
            ViewModel.Value.Model.HistoryOrCurrent = !ViewModel.Value.Model.HistoryOrCurrent;
            ViewModel.Value.Model.EventManagerService.SetHistoryOrCurrent(ViewModel.Value.Model.HistoryOrCurrent);
            UpdateDisplayItems();
        }
    }
}