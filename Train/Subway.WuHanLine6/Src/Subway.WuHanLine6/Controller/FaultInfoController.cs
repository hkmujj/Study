using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Controller.BtnResponse;
using Subway.WuHanLine6.Events;
using Subway.WuHanLine6.FaultInfos;
using Subway.WuHanLine6.Models.Model;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    ///     故障控制类
    /// </summary>
    [Export]
    public class FaultInfoController : ControllerBase<FaultInfoModel>
    {
        private readonly DispatcherTimer timer;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="faultInfoManager"></param>
        [ImportingConstructor]
        public FaultInfoController(FaultInfoManager faultInfoManager)
        {
            FaultInfoManager = faultInfoManager;
            NavigatorToCurrent =
                new DelegateCommand(
                    () => ServiceLocator.Current.GetInstance<CurrentFaultViewNavigator>().ButtonClick(),
                    (() => ViewModel.HasFault == Visibility.Visible));
            ShowTrips = new DelegateCommand<FaultInfo>(args =>
            {
                ViewModel.TripsVisibility = Visibility.Visible;
                ViewModel.TripInfo = args;
            });
            HideTrips = new DelegateCommand((() =>
           {
               ViewModel.TripsVisibility = Visibility.Hidden;
               ViewModel.TripInfo = null;
           }));
            Confirm = new DelegateCommand(() =>
            {
                ViewModel.NoConfirmInfo.Confirm();
                ViewModel.NoConfirmInfo = null;
                RefreshFaultInfo();
                FaultChanged(0);
            });
            NexPage = new DelegateCommand((() => FaultInfoManager.NextPage()));
            LastPage = new DelegateCommand((() => FaultInfoManager.LastPage()));
            ChangingLevel = new DelegateCommand<ChangingLevelCommandArgs>(ChangingLevelAction);
            faultInfoManager.CurrentPageChanged += FaultChanged;
            faultInfoManager.CountChanged += FaultChanged;
            faultInfoManager.MaxPageChanged += FaultChanged;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += (sender, arg) => { RefreshFaultInfo(); };
            timer.Start();
            ChangedCurrent = new DelegateCommand(() =>
            {
                ViewModel.IsCurrent = !ViewModel.IsCurrent;
                FaultInfoManager.ChangingType(ViewModel.IsCurrent ? FaultCurrent.Day : FaultCurrent.Histort);
            });
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<FaultViewChangedEvent>()
                .Subscribe((args) =>
                {
                    FaultInfoManager.Reset(args.Level, args.Current);
                    if (args.Current == FaultCurrent.Histort)
                    {
                        ViewModel.IsCurrent = false;
                    }
                });
        }

        /// <summary>
        ///     故障管理类
        /// </summary>
        public FaultInfoManager FaultInfoManager { get; }

        /// <summary>
        ///     显示操作提示
        /// </summary>
        public ICommand ShowTrips { get; private set; }

        /// <summary>
        ///     隐藏操作提示
        /// </summary>
        public ICommand HideTrips { get; private set; }

        /// <summary>
        ///     导航到当前故障页
        /// </summary>
        public DelegateCommand NavigatorToCurrent { get; private set; }

        /// <summary>
        ///     修改显示的故障等级
        /// </summary>
        public ICommand ChangingLevel { get; private set; }

        /// <summary>
        ///     下一页
        /// </summary>
        public ICommand NexPage { get; private set; }

        /// <summary>
        ///     上一页
        /// </summary>
        public ICommand LastPage { get; private set; }

        /// <summary>
        /// 今日故障 历史故障切换
        /// </summary>
        public ICommand ChangedCurrent { get; private set; }

        /// <summary>
        ///     故障确认
        /// </summary>
        public ICommand Confirm { get; set; }

        private void RefreshFaultInfo()
        {
            var tmp = FaultInfoManager.CurrentData.Where(w => !w.IsConfirm);
            ViewModel.NoFaultConfim = tmp.Any() ? Visibility.Visible : Visibility.Hidden;
            if (ViewModel.NoConfirmInfo != null)
            {
                var tmpList = tmp.ToList();
                var index = tmpList.FindIndex(f => f.LogicNumber == ViewModel.NoConfirmInfo.LogicNumber);
                if (index != -1)
                {
                    if (index == tmpList.Count - 1)
                    {
                        ViewModel.NoConfirmInfo = tmpList[0];
                    }
                    else
                    {
                        ViewModel.NoConfirmInfo = tmpList[index + 1];
                    }
                }
            }
            else
            {
                ViewModel.NoConfirmInfo = tmp.Any() ? tmp.First() : null;
            }
        }

        private void ChangingLevelAction(ChangingLevelCommandArgs obj)
        {
            if (obj == null)
            {
                return;
            }
            FaultInfoManager.ChangedLevel(obj.Level);
        }

        private void FaultChanged(int obj)
        {
            ViewModel.HasFault =
                FaultInfoManager.CurrentData.Any(
                    w => (w.Level == FaultLevel.Red || w.Level == FaultLevel.Yellow))
                    ? Visibility.Visible
                    : Visibility.Hidden;
            ViewModel.CurrentFaultInfo = FaultInfoManager.GetCurrentPageInfo();
            ViewModel.PageInfo = string.Format("{0}/{1}", FaultInfoManager.CurrenPage, FaultInfoManager.MaxPage);
            RefreshFaultInfo();
        }

        /// <summary>
        ///     改变界面
        /// </summary>
        public void CurrentChanged()
        {
            if (ViewModel.CurrentFaultInfo.Count() >= FaultInfoManager.PageNum)
            {
                return;
            }
            var tmp = ViewModel.CurrentFaultInfo.ToList();
            while (tmp.Count < FaultInfoManager.PageNum)
            {
                tmp.Add(null);
            }

            ViewModel.CurrentFaultInfo = tmp;
        }
    }

    /// <summary>
    ///     切换等级命令参数
    /// </summary>
    public class ChangingLevelCommandArgs
    {
        /// <summary>
        ///     等级
        /// </summary>
        public FaultLevel Level { get; set; }
    }
}