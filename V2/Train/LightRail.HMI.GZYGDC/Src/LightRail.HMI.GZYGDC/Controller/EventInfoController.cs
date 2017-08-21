using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using CommonUtil.Util;
using DevExpress.Mvvm.POCO;
using LightRail.HMI.GZYGDC.Interface;
using LightRail.HMI.GZYGDC.Model.Units;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.GZYGDC.Controller
{
    [Export]
    public class EventInfoController : ControllerBase<Lazy<EventInfoViewModel>>
    {

        


        [ImportingConstructor]
        public EventInfoController(Lazy<EventInfoViewModel> viewModel) : base(viewModel)
        {
           
        }


        public override void Initalize()
        {
            if (ViewModel.Value.Model.EventManagerService != null)
            {
                ViewModel.Value.Model.EventManagerService.CurrentEventChanged += EventChanged;
            }

            EventChanged();
        }


        public void EventChanged()
        {
            UpdateDisplayItems();
        }

        /// <summary>
        /// 更新显示
        /// </summary>
        /// <param name="bCurrentEvent">当前页事件/历史事件</param>
        private void UpdateDisplayItems(bool bCurrentEvent = true)
        {
            if (ViewModel.Value.Model.EventManagerService == null)
            {
                return;
            }

            IEnumerable<IEventInfo> PageInfo = null;

            if (bCurrentEvent)
            {
                PageInfo = ViewModel.Value.Model.EventManagerService.GetCurrentPageInfo();
            }
            else
            {
                PageInfo = ViewModel.Value.Model.EventManagerService.GetHistoryEventInfos();
            }

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                ViewModel.Value.Model.EventInfoDisplayItems.Clear();
                ViewModel.Value.Model.EventInfoDisplayItems.AddRange(GetInfos(PageInfo));
                ViewModel.Value.Model.CurPageNum = ViewModel.Value.Model.EventManagerService.CurrentPage;
                ViewModel.Value.Model.MaxPageNum = ViewModel.Value.Model.EventManagerService.MaxPage;
                ViewModel.Value.Model.TotalCount =
                    ViewModel.Value.Model.EventManagerService.GetHistoryEventInfos().Count();
                // ViewModel.Value.Model.CurEventLevel = ViewModel.Value.Model.EventManagerService.GetHightLevel();
                ViewModel.Value.Model.CurEventLevel = ViewModel.Value.Model.EventManagerService.CurrentLevel;

                ViewModel.Value.Model.UnConfirmEventCount = ViewModel.Value.Model.EventManagerService.GetUnConfirmEventCount;

                if (ViewModel.Value.Model.WaitConfirmEvent == null)
                {
                    ViewModel.Value.Model.WaitConfirmEvent = ViewModel.Value.Model.EventManagerService.GetUnConfirmEvent();
                }
                else
                {
                    ViewModel.Value.Model.UnConfirmEventCount = ViewModel.Value.Model.EventManagerService.GetUnConfirmEventCount + 1;
                }

                SelectEvent();

            }));
        }


        private IEnumerable<IEventInfo> GetInfos(IEnumerable<IEventInfo> infos)
        {
            var tmp = infos.OrderByDescending(o => o.HappenTime).ToList();

            for (var i = 0; i < 10; ++i)
            {
                if (i >= tmp.Count())
                {
                    tmp.Add(EventInfo.Empty);
                }
                if (tmp[i] == EventInfo.Empty)
                {
                    continue;
                }
                tmp[i].Number = i + 1;
            }
            return tmp;
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void LastPage()
        {
            ViewModel.Value.Model.EventManagerService.LastPage();

            UpdateDisplayItems();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            ViewModel.Value.Model.EventManagerService.NextPage();

            UpdateDisplayItems();
        }

        /// <summary>
        /// 确认事件
        /// </summary>
        public void ConfirmEvent()
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

        /// <summary>
        /// 设置事件等级
        /// </summary>
        /// <param name="strLevel"></param>
        public void SetEventLevel(string strLevel)
        {
            EventLevel level;

            if (string.IsNullOrWhiteSpace(strLevel))
            {
                level = EventLevel.Normal;
            }
            else
            {
                if (!Enum.TryParse(strLevel, out level))
                {
                    level = EventLevel.Normal;
                }
            }

            ViewModel.Value.Model.EventManagerService.SetEveltLevel(level);

            UpdateDisplayItems();
        }


        /// <summary>
        /// 选中事件
        /// </summary>
        public void SelectEvent()
        {
            ViewModel.Value.Model.SelectEvent =
                ViewModel.Value.Model.EventInfoDisplayItems.FirstOrDefault(f => f.IsChecked);
        }


        /// <summary>
        /// 显示历史事件
        /// </summary>
        public void ShowHistoryEvent()
        {
            UpdateDisplayItems(false);
        }
    }
}