using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Subway.WuHanLine6.Controller;
using Subway.WuHanLine6.FaultInfos;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 故障Model
    /// </summary>
    [Export]
    public class FaultInfoModel : ModelBase
    {
        private IEnumerable<FaultInfo> m_CurrentFaultInfo;
        private FaultInfo m_NoConfirmInfo;
        private Visibility m_HasFault;
        private Visibility m_TripsVisibility;
        private string m_PageInfo;
        private FaultInfo m_TripInfo;
        private Visibility m_NoFaultConfim;
        private bool m_IsCurrent;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller"></param>
        [ImportingConstructor]
        public FaultInfoModel(FaultInfoController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            CurrentFaultInfo = new List<FaultInfo>();
            TripsVisibility = Visibility.Hidden;
            HasFault = Visibility.Hidden;
            Instance = this;
        }

        /// <summary>
        /// 是否今日故障 true 今日股指  false 历史故障
        /// </summary>
        public bool IsCurrent
        {
            get { return m_IsCurrent; }
            set
            {
                if (value == m_IsCurrent)
                {
                    return;
                }
                m_IsCurrent = value;
                RaisePropertyChanged(() => IsCurrent);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static FaultInfoModel Instance { get; private set; }

        /// <summary>
        /// 页面信息
        /// </summary>
        public string PageInfo
        {
            get { return m_PageInfo; }
            set
            {
                if (value == m_PageInfo)
                {
                    return;
                }
                m_PageInfo = value;
                RaisePropertyChanged(() => PageInfo);
            }
        }

        /// <summary>
        /// 当前显示的故障列表
        /// </summary>
        public IEnumerable<FaultInfo> CurrentFaultInfo
        {
            get { return m_CurrentFaultInfo; }
            set
            {
                if (Equals(value, m_CurrentFaultInfo))
                {
                    return;
                }
                m_CurrentFaultInfo = value;
                Controller.CurrentChanged();
                RaisePropertyChanged(() => CurrentFaultInfo);
            }
        }

        /// <summary>
        /// 故障控制类
        /// </summary>
        public FaultInfoController Controller { get; private set; }

        /// <summary>
        /// 提示信息显示
        /// </summary>
        public Visibility TripsVisibility
        {
            get { return m_TripsVisibility; }
            set
            {
                if (value == m_TripsVisibility)
                {
                    return;
                }
                m_TripsVisibility = value;
                RaisePropertyChanged(() => TripsVisibility);
            }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public FaultInfo TripInfo
        {
            get { return m_TripInfo; }
            set
            {
                if (Equals(value, m_TripInfo))
                {
                    return;
                }
                m_TripInfo = value;
                RaisePropertyChanged(() => TripInfo);
            }
        }

        /// <summary>
        /// 未确认故障
        /// </summary>
        public FaultInfo NoConfirmInfo
        {
            get { return m_NoConfirmInfo; }
            set
            {
                if (Equals(value, m_NoConfirmInfo))
                {
                    return;
                }
                m_NoConfirmInfo = value;
                RaisePropertyChanged(() => NoConfirmInfo);
            }
        }

        /// <summary>
        /// 未确认故障显示
        /// </summary>
        public Visibility NoFaultConfim
        {
            get { return m_NoFaultConfim; }
            set
            {
                if (value == m_NoFaultConfim)
                {
                    return;
                }
                m_NoFaultConfim = value;
                RaisePropertyChanged(() => NoFaultConfim);
            }
        }

        /// <summary>
        /// 是否有未确认的1，2 级故障
        /// </summary>
        public Visibility HasFault
        {
            get { return m_HasFault; }
            set
            {
                if (value == m_HasFault)
                {
                    return;
                }
                m_HasFault = value;
                Controller.NavigatorToCurrent.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => HasFault);
            }
        }
    }
}