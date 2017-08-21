using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine7.Envets;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Interfaces;
using Subway.ShenZhenLine7.Models.Units;
using Subway.ShenZhenLine7.Resource.Keys;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    [Export(typeof(ICourceStatusChanged))]
    public class HSCBViewModel : ViewModelBase
    {
        private HighSpeedState m_Car5HighSpeedState;
        private HighSpeedState m_Car4HighSpeedState;
        private HighSpeedState m_Car3HighSpeedState;
        private HighSpeedState m_Car2HighSpeedState;
        private WorkPowerState m_Car5WorkPowerState;
        private WorkPowerState m_Car2WorkPowerState;
        private PantographState m_Car5PantographState;
        private PantographState m_Car2PantographState;

        /// <summary>
        /// 
        /// </summary>
        public HSCBViewModel()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<BoolDataChangedEvent>()
                .Subscribe((NetWorkResponse), ThreadOption.UIThread);
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<FloatDataChangedEvent>()
                .Subscribe(FloatChanged, ThreadOption.UIThread);
            m_Args = new Dictionary<string, Action>();
            InitArgs();
            this.Cast(WorkPowerState.UnConnect);
            this.Cast(PantographState.Down);
            this.Cast(HighSpeedState.Open);
        }

        private void FloatChanged(DataChangedEventArgs<float> obj)
        {
            obj.Data.UpdateIfContain(InFloatKeys.车2网流, f => { Car2NetStreamValue = f; });
            obj.Data.UpdateIfContain(InFloatKeys.车5网流, f => { Car5NetStreamValue = f; });
        }

        private void InitArgs()
        {
            m_Args.Add(InBoolKeys.车2车间电源未连接, () => Car2WorkPowerState = WorkPowerState.UnConnect);
            m_Args.Add(InBoolKeys.车2车间电源连接未供电, () => Car2WorkPowerState = WorkPowerState.Connect);
            m_Args.Add(InBoolKeys.车2车间电源连接且供电, () => Car2WorkPowerState = WorkPowerState.ConnectPower);
            m_Args.Add(InBoolKeys.车5车间电源未连接, () => Car5WorkPowerState = WorkPowerState.UnConnect);
            m_Args.Add(InBoolKeys.车5车间电源连接未供电, () => Car5WorkPowerState = WorkPowerState.Connect);
            m_Args.Add(InBoolKeys.车5车间电源连接且供电, () => Car5WorkPowerState = WorkPowerState.ConnectPower);
            m_Args.Add(InBoolKeys.车2受电弓降下, () => Car2PantographState = PantographState.Down);
            m_Args.Add(InBoolKeys.车2受电弓升起, () => Car2PantographState = PantographState.Up);
            m_Args.Add(InBoolKeys.车2受电弓降下且故障, () => Car2PantographState = PantographState.DownFault);
            m_Args.Add(InBoolKeys.车2受电弓升起且故障, () => Car2PantographState = PantographState.UpFault);
            m_Args.Add(InBoolKeys.车5受电弓降下, () => Car5PantographState = PantographState.Down);
            m_Args.Add(InBoolKeys.车5受电弓升起, () => Car5PantographState = PantographState.Up);
            m_Args.Add(InBoolKeys.车5受电弓降下且故障, () => Car5PantographState = PantographState.DownFault);
            m_Args.Add(InBoolKeys.车5受电弓升起且故障, () => Car5PantographState = PantographState.UpFault);
            m_Args.Add(InBoolKeys.车2高速断路器闭合, () => Car2HighSpeedState = HighSpeedState.Close);
            m_Args.Add(InBoolKeys.车2高速断路器断开, () => Car2HighSpeedState = HighSpeedState.Open);
            m_Args.Add(InBoolKeys.车2高速断路器断开故障, () => Car2HighSpeedState = HighSpeedState.OpedFault);
            m_Args.Add(InBoolKeys.车2高速断路器闭合故障, () => Car2HighSpeedState = HighSpeedState.CloseFault);
            m_Args.Add(InBoolKeys.车3高速断路器闭合, () => Car3HighSpeedState = HighSpeedState.Close);
            m_Args.Add(InBoolKeys.车3高速断路器断开, () => Car3HighSpeedState = HighSpeedState.Open);
            m_Args.Add(InBoolKeys.车3高速断路器断开故障, () => Car3HighSpeedState = HighSpeedState.OpedFault);
            m_Args.Add(InBoolKeys.车3高速断路器闭合故障, () => Car3HighSpeedState = HighSpeedState.CloseFault);
            m_Args.Add(InBoolKeys.车4高速断路器闭合, () => Car4HighSpeedState = HighSpeedState.Close);
            m_Args.Add(InBoolKeys.车4高速断路器断开, () => Car4HighSpeedState = HighSpeedState.Open);
            m_Args.Add(InBoolKeys.车4高速断路器断开故障, () => Car4HighSpeedState = HighSpeedState.OpedFault);
            m_Args.Add(InBoolKeys.车4高速断路器闭合故障, () => Car4HighSpeedState = HighSpeedState.CloseFault);
            m_Args.Add(InBoolKeys.车5高速断路器闭合, () => Car5HighSpeedState = HighSpeedState.Close);
            m_Args.Add(InBoolKeys.车5高速断路器断开, () => Car5HighSpeedState = HighSpeedState.Open);
            m_Args.Add(InBoolKeys.车5高速断路器断开故障, () => Car5HighSpeedState = HighSpeedState.OpedFault);
            m_Args.Add(InBoolKeys.车5高速断路器闭合故障, () => Car5HighSpeedState = HighSpeedState.CloseFault);
        }

        private readonly Dictionary<string, Action> m_Args;
        private double m_Car5NetStreamValue;
        private double m_Car2NetStreamValue;

        private void NetWorkResponse(DataChangedEventArgs<bool> dataChangedEventArgs)
        {
            dataChangedEventArgs.Data.UpdateIfContainWhenAllTrue(m_Args, null);
        }

        /// <summary>
        /// 
        /// </summary>
        public PantographState Car2PantographState
        {
            get { return m_Car2PantographState; }
            set
            {
                if (value == m_Car2PantographState)
                {
                    return;
                }
                m_Car2PantographState = value;
                RaisePropertyChanged(() => Car2PantographState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PantographState Car5PantographState
        {
            get { return m_Car5PantographState; }
            set
            {
                if (value == m_Car5PantographState)
                {
                    return;
                }
                m_Car5PantographState = value;
                RaisePropertyChanged(() => Car5PantographState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WorkPowerState Car2WorkPowerState
        {
            get { return m_Car2WorkPowerState; }
            set
            {
                if (value == m_Car2WorkPowerState)
                {
                    return;
                }
                m_Car2WorkPowerState = value;
                RaisePropertyChanged(() => Car2WorkPowerState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WorkPowerState Car5WorkPowerState
        {
            get { return m_Car5WorkPowerState; }
            set
            {
                if (value == m_Car5WorkPowerState)
                {
                    return;
                }
                m_Car5WorkPowerState = value;
                RaisePropertyChanged(() => Car5WorkPowerState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HighSpeedState Car2HighSpeedState
        {
            get { return m_Car2HighSpeedState; }
            set
            {
                if (value == m_Car2HighSpeedState)
                {
                    return;
                }
                m_Car2HighSpeedState = value;
                RaisePropertyChanged(() => Car2HighSpeedState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HighSpeedState Car3HighSpeedState
        {
            get { return m_Car3HighSpeedState; }
            set
            {
                if (value == m_Car3HighSpeedState)
                {
                    return;
                }
                m_Car3HighSpeedState = value;
                RaisePropertyChanged(() => Car3HighSpeedState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HighSpeedState Car4HighSpeedState
        {
            get { return m_Car4HighSpeedState; }
            set
            {
                if (value == m_Car4HighSpeedState)
                {
                    return;
                }
                m_Car4HighSpeedState = value;
                RaisePropertyChanged(() => Car4HighSpeedState);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HighSpeedState Car5HighSpeedState
        {
            get { return m_Car5HighSpeedState; }
            set
            {
                if (value == m_Car5HighSpeedState)
                {
                    return;
                }
                m_Car5HighSpeedState = value;
                RaisePropertyChanged(() => Car5HighSpeedState);
            }
        }

        /// <summary>
        /// 车2网流值
        /// </summary>
        public double Car2NetStreamValue
        {
            get { return m_Car2NetStreamValue; }
            set
            {
                if (value.Equals(m_Car2NetStreamValue))
                {
                    return;
                }
                m_Car2NetStreamValue = value;
                RaisePropertyChanged(() => Car2NetStreamValue);
            }
        }

        /// <summary>
        /// 车5网流值
        /// </summary>
        public double Car5NetStreamValue
        {
            get { return m_Car5NetStreamValue; }
            set
            {
                if (value.Equals(m_Car5NetStreamValue))
                {
                    return;
                }
                m_Car5NetStreamValue = value;
                RaisePropertyChanged(() => Car5NetStreamValue);
            }
        }
    }
}