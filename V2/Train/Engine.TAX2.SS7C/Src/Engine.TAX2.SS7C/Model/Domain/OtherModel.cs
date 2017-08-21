using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Engine.TAX2.SS7C.Model.Domain.Details;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain
{

    [Export]
    public class OtherModel : NotificationObject
    {
        private float m_Softversion;
        private float m_TrainNumber;
        private DateTime m_SimTime;
        private TimeSpan m_AdjustSpan;
        private float m_SettingTrainNumber;
        private ModifyTimeModel m_ModifyTimeModel;
        private double m_OpacityPercent;

        [ImportingConstructor]
        public OtherModel(ModifyTimeModel modifyTimeModel)
        {
            ModifyTimeModel = modifyTimeModel;
        }

        public ModifyTimeModel ModifyTimeModel
        {
            get { return m_ModifyTimeModel; }
            private set
            {
                if (Equals(value, m_ModifyTimeModel))
                {
                    return;
                }

                m_ModifyTimeModel = value;
                RaisePropertyChanged(() => ModifyTimeModel);
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

        public double OpacityPercent
        {
            set
            {
                if (value.Equals(m_OpacityPercent))
                {
                    return;
                }

                m_OpacityPercent = value;
                RaisePropertyChanged(() => OpacityPercent);
            }
            get { return m_OpacityPercent; }
        }
    }
}