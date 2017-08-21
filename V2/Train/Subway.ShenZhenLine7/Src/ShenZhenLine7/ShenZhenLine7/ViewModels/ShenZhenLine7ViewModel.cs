using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.ShenZhenLine7.Controller.ViewModelController;
using Subway.ShenZhenLine7.Envets;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// ViewModel
    /// </summary>
    [Export]
    public class ShenZhenLine7ViewModel : NotificationObject, IDataListener
    {
        private DateTime m_CurrentTime;
        private bool m_IsStart;
        private IState m_CurrentState;

        /// <summary>
        /// 事件聚合器
        /// </summary>
        public IEventAggregator EventAggregator { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller">控制类</param>
        /// <param name="eventAggregator">事件聚合器</param>
        [ImportingConstructor]
        public ShenZhenLine7ViewModel(ShenZhenLine7Controller controller, IEventAggregator eventAggregator)
        {
            controller.ViewModel = this;
            Controller = controller;
            EventAggregator = eventAggregator;
        }
        /// <summary>
        /// 
        /// </summary>
        [ImportMany]
        public List<Lazy<ICourceStatusChanged>> AllCourceViewModel;
        /// <summary>
        /// 当前状态
        /// </summary>
        public IState CurrentState
        {
            get { return m_CurrentState; }
            private set
            {
                if (Equals(value, m_CurrentState))
                {
                    return;
                }
                m_CurrentState = value;
                m_CurrentState?.UpdateSate();
                RaisePropertyChanged(() => CurrentState);
            }
        }

        /// <summary>
        /// 更新State
        /// </summary>
        /// <param name="state"></param>
        public void UpdateState(IState state)
        {
            CurrentState = state;
        }
        /// <summary>
        /// 控制类
        /// </summary>
        public ShenZhenLine7Controller Controller { get; }
        /// <summary>
        /// MasterPageViewModel
        /// </summary>
        [Import]
        public MasterPageViewModel MasterPageViewModel { get; private set; }
        /// <summary>
        /// 事件信息ViewModel
        /// </summary>
        [Import]
        public EventInfoViewModel EventInfoViewModel { get; private set; }
        /// <summary>
        /// 旁路ViewModel
        /// </summary>
        [Import]
        public BypassViewModel BypassViewModel { get; private set; }
        /// <summary>
        /// 牵引封锁
        /// </summary>
        [Import]
        public TractionLockViewModel TractionLockViewModel { get; private set; }
        /// <summary>
        /// 站点设置
        /// </summary>
        [Import]
        public StationViewModel StationViewModel { get; private set; }
        /// <summary>
        /// 基础信息
        /// </summary>
        [Import]
        public BaseInfoViewModel BaseInfoViewModel { get; private set; }
        /// <summary>
        /// 课程开始
        /// </summary>
        public bool IsStart
        {
            get { return m_IsStart; }
            set
            {
                if (value == m_IsStart)
                {
                    return;
                }
                m_IsStart = value;
                Controller.StartChanged();
                RaisePropertyChanged(() => IsStart);
            }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentTime
        {
            get { return m_CurrentTime; }
            set
            {
                if (value.Equals(m_CurrentTime))
                {
                    return;
                }
                m_CurrentTime = value;
                RaisePropertyChanged(() => CurrentTime);
            }
        }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            EventAggregator.GetEvent<BoolDataChangedEvent>().Publish(new DataChangedEventArgs<bool>() { Data = dataChangedArgs.NewValue });
        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            EventAggregator.GetEvent<FloatDataChangedEvent>().Publish(new DataChangedEventArgs<float>() { Data = dataChangedArgs.NewValue });
        }

        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {

        }
    }
}