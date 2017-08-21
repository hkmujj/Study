using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine9.Resource.Keys;
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
    public class EventInfoController : ControllerBase<EventInfoViewModel>
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EventInfoController()
        {
            NextPage = new DelegateCommand((NexPage));
            LastPage = new DelegateCommand((LastPageResponse));
            SetLevel = new DelegateCommand<string>((SetLevelResponse));
            Help = new DelegateCommand((HelpResponse));
            Confirm = new DelegateCommand(ConfirmMethod);

        }

        private void ConfirmMethod()
        {
            if (ViewModel.ConfirmEventInfo == default(IEventInfo))
            {
                return;
            }
            ViewModel.EventManagerService.ConfirmFault(ViewModel.ConfirmEventInfo);
            ViewModel.ConfirmEventInfo = ViewModel.EventManagerService.GetUnConfirmEvent();
            ChangedDisplay();

        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            if (ViewModel.EventManagerService != null)
            {
                ViewModel.EventManagerService.CurrentEventChanged += ChangedDisplay;
            }
            ChangedDisplay();
        }

        private void HelpResponse()
        {
            Select();
            if (ViewModel.Select == EventInfo.Empty)
            {
                return;
            }
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToKeyEvent>().Publish(StateKeys.事件帮助);
        }

        private void SetLevelResponse(string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                ViewModel.EventManagerService.SetEveltLevel(EventLevel.Normal);
                ChangedDisplay();
                return;
            }
            EventLevel level;
            if (!Enum.TryParse(arg, out level))
            {
                return;
            }
            ViewModel.EventManagerService.SetEveltLevel(level);
            ChangedDisplay();
        }

        private void LastPageResponse()
        {
            ViewModel.EventManagerService.LastPage();
            ChangedDisplay();
        }

        private void NexPage()
        {
            ViewModel.EventManagerService.NextPage();
            ChangedDisplay();
        }

        private void ChangedDisplay()
        {
            if (ViewModel.EventManagerService == null)
            {
                return;
            }
            var s = ViewModel.EventManagerService.GetCurrentPageInfo();
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                ViewModel.DisplayInfos.Clear();
                ViewModel.DisplayInfos.AddRange(GetInfos(s));
                ViewModel.Page = ViewModel.EventManagerService.CurrentPage;
                ViewModel.TotalPage = ViewModel.EventManagerService.MaxPage;
                ViewModel.ConfirmEventInfo = ViewModel.EventManagerService.GetUnConfirmEvent();
                ViewModel.CurrentLevel = ViewModel.EventManagerService.GetHightLevel();
                Select();
            }));

        }

        private void Select()
        {
            ViewModel.Select = ViewModel.DisplayInfos.FirstOrDefault(f => f.IsChecked);
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
        /// 下一页
        /// </summary>
        public ICommand NextPage { get; private set; }

        /// <summary>
        /// 上一页
        /// </summary>
        public ICommand LastPage { get; private set; }

        /// <summary>
        /// 设置筛选条件
        /// </summary>
        public ICommand SetLevel { get; private set; }
        /// <summary>
        /// 故障帮助
        /// </summary>
        public ICommand Help { get; private set; }
        /// <summary>
        /// 故障确认
        /// </summary>
        public ICommand Confirm { get; private set; }

    }
}