using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Resource.Keys;
using Subway.ShenZhenLine9.Envets;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    [Export(typeof(ICourceStatusChanged))]
    public class AirPumpViewModel : ViewModelBase
    {
        private AirPumpState m_Car3State;
        private AirPumpState m_Car4State;
        /// <summary>
        /// 
        /// </summary>
        public AirPumpViewModel()
        {
            var events = ServiceLocator.Current.GetInstance<IEventAggregator>();
            events.GetEvent<BoolDataChangedEvent>().Subscribe((NetWorkResponse), ThreadOption.UIThread);
            events.GetEvent<FloatDataChangedEvent>().Subscribe(FloatChangedResponse, ThreadOption.UIThread);

            m_ArgsDictionary = new Dictionary<string, Action>
            {
                {InBoolKeys.车3空压机断开, () => Car3State = AirPumpState.Off},
                {InBoolKeys.车3空压机运行, () => Car3State = AirPumpState.Run},
                {InBoolKeys.车3空压机严重故障, () => Car3State = AirPumpState.Fault},
                {InBoolKeys.车4空压机断开, () => Car4State = AirPumpState.Off},
                {InBoolKeys.车4空压机运行, () => Car4State = AirPumpState.Run},
                {InBoolKeys.车4空压机严重故障, () => Car4State = AirPumpState.Fault}
            };
        }

        private void FloatChangedResponse(DataChangedEventArgs<float> args)
        {
            args.Data.UpdateIfContain(InFloatKeys.车3空压机值, f => Car3AirPumpValue = f);
            args.Data.UpdateIfContain(InFloatKeys.车4空压机值, f => Car4AirPumpValue = f);
        }

        /// <summary>
        /// 初始化运行数据
        /// </summary>
        public override void Start()
        {
            Car3State = AirPumpState.Off;
            Car4State = AirPumpState.Off;
        }

        private readonly Dictionary<string, Action> m_ArgsDictionary;
        private double m_Car3AirPumpValue;
        private double m_Car4AirPumpValue;

        private void NetWorkResponse(DataChangedEventArgs<bool> dataChangedEventArgs)
        {
            dataChangedEventArgs.Data.UpdateIfContainWhenAllTrue(m_ArgsDictionary, null);

        }

        /// <summary>
        /// 车3 空压机状态
        /// </summary>
        public AirPumpState Car3State
        {
            get { return m_Car3State; }
            set
            {
                if (value == m_Car3State)
                {
                    return;
                }
                m_Car3State = value;
                RaisePropertyChanged(() => Car3State);
            }
        }

        /// <summary>
        /// 车4 空压机状态
        /// </summary>
        public AirPumpState Car4State
        {
            get { return m_Car4State; }
            set
            {
                if (value == m_Car4State)
                {
                    return;
                }
                m_Car4State = value;
                RaisePropertyChanged(() => Car4State);
            }
        }

        /// <summary>
        /// 车3空压机值
        /// </summary>
        public double Car3AirPumpValue
        {
            get { return m_Car3AirPumpValue; }
            set
            {
                if (value.Equals(m_Car3AirPumpValue))
                {
                    return;
                }
                m_Car3AirPumpValue = value;
                RaisePropertyChanged(() => Car3AirPumpValue);
            }
        }

        /// <summary>
        /// 车4空压机值
        /// </summary>
        public double Car4AirPumpValue
        {
            get { return m_Car4AirPumpValue; }
            set
            {
                if (value.Equals(m_Car4AirPumpValue))
                {
                    return;
                }
                m_Car4AirPumpValue = value;
                RaisePropertyChanged(() => Car4AirPumpValue);
            }
        }
    }

    /// <summary>
    /// 空压机状态
    /// </summary>
    public enum AirPumpState
    {
        /// <summary>
        /// 故障
        /// </summary>
        [Description("空气压缩机严重故障")]
        Fault,
        /// <summary>
        /// 运行
        /// </summary>
        [Description("空气压缩机运行，无故障")]
        Run,
        /// <summary>
        /// 关闭
        /// </summary>
        [Description("空气压缩机未启动")]
        Off
    }
}