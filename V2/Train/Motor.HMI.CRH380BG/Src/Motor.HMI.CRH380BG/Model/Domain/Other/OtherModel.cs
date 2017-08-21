using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Model.Domain.Other
{
    [Export]
    public class OtherModel : NotificationObject
    {
        private DateTime m_SimTime;
        private TimeSpan m_AdjustSpan;
        private DateTime m_SettingTime;
        private double m_Light;
        private double m_Volume;
        

        public OtherModel()
        {
            Light = 20;
        }

        public double Light
        {
            get { return m_Light; }
            set
            {
                if (value.Equals(m_Light))
                {
                    return;
                }

                m_Light = value;
                RaisePropertyChanged(() => Light);
            }
        }

        public double Volume
        {
            get { return m_Volume; }
            set
            {
                if (value.Equals(m_Volume))
                {
                    return;
                }

                m_Volume = value;
                RaisePropertyChanged(() => Volume);
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
    }
}
