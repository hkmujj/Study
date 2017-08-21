using System;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Other.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Other
{
    [Export]
    public class OtherModel : NotificationObject
    {
        private float m_Softversion;
        private float m_TrainNumber;
        private DateTime m_SimTime;
        private TimeSpan m_AdjustSpan;
        private DateTime m_SettingTime;
        private float m_SettingTrainNumber;
        private float m_OpacityPercent;

        [ImportingConstructor]
        public OtherModel(AccumulativeDataModel accumulativeDataModel)
        {
            AccumulativeDataModel = accumulativeDataModel;
        }

        public AccumulativeDataModel AccumulativeDataModel { get; private set; }

        public float OpacityPercent
        {
            get { return m_OpacityPercent; }
            set
            {
                if (value.Equals(m_OpacityPercent))
                {
                    return;
                }

                m_OpacityPercent = value;
                RaisePropertyChanged(() => OpacityPercent);
            }
        }

        /// <summary>
        /// 仿真时间
        /// </summary>
        public DateTime SimTime
        {
            set
            {
                if (value.Equals(m_SimTime))
                {
                    return;
                }

                m_SimTime = value;
                RaisePropertyChanged(() => SimTime);
                RaisePropertyChanged(() => ShowTime);
            }
            get { return m_SimTime; }
        }

        /// <summary>
        /// 修改时间的差值
        /// </summary>
        public TimeSpan AdjustSpan
        {
            set
            {
                if (value.Equals(m_AdjustSpan))
                {
                    return;
                }

                m_AdjustSpan = value;
                RaisePropertyChanged(() => AdjustSpan);
                RaisePropertyChanged(() => ShowTime);
            }
            get { return m_AdjustSpan; }
        }

        /// <summary>
        /// 显示时间
        /// </summary>
        public DateTime ShowTime { get { return SimTime + AdjustSpan; } }

        public DateTime SettingTime
        {
            get { return m_SettingTime; }
            set
            {
                if (value.Equals(m_SettingTime))
                {
                    return;
                }

                m_SettingTime = value;
                RaisePropertyChanged(() => SettingTime);
            }
        }

        public float Softversion
        {
            set
            {
                if (value.Equals(m_Softversion))
                {
                    return;
                }

                m_Softversion = value;
                RaisePropertyChanged(() => Softversion);
            }
            get { return m_Softversion; }
        }

        public float TrainNumber
        {
            set
            {
                if (value.Equals(m_TrainNumber))
                {
                    return;
                }

                m_TrainNumber = value;
                RaisePropertyChanged(() => TrainNumber);
            }
            get { return m_TrainNumber; }
        }

        public float SettingTrainNumber
        {
            get { return m_SettingTrainNumber; }
            set
            {
                if (value.Equals(m_SettingTrainNumber))
                {
                    return;
                }

                m_SettingTrainNumber = value;
                RaisePropertyChanged(() => SettingTrainNumber);
            }
        }
    }
}