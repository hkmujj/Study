using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Model.Domain.Emergency
{
    [Export]
    public class EmergencyModel : NotificationObject
    {

        private bool m_IsReleaseSpeed;
        private bool m_SpeedLimit100;
        private bool m_SpeedLimitVisible;
        private bool m_SpeedLimit;
        private bool m_AccelerationAlarmVisible;
        private AccelerationAlarmType m_AccelerationAlarmType;

        public bool IsReleaseSpeed
        {
            get { return m_IsReleaseSpeed; }
            set
            {
                if (value == m_IsReleaseSpeed)
                {
                    return;
                }

                m_IsReleaseSpeed = value;
                RaisePropertyChanged(() => IsReleaseSpeed);
            }
        }

        public bool SpeedLimit100
        {
            get { return m_SpeedLimit100; }
            set
            {
                if (value == m_SpeedLimit100)
                {
                    return;
                }

                m_SpeedLimit100 = value;
                RaisePropertyChanged(() => SpeedLimit100);
            }
        }

        public bool SpeedLimitVisible
        {
            get { return m_SpeedLimitVisible; }
            set
            {
                if (value == m_SpeedLimitVisible)
                {
                    return;
                }

                m_SpeedLimitVisible = value;
                RaisePropertyChanged(() => SpeedLimitVisible);
            }
        }

        public bool SpeedLimit
        {
            get { return m_SpeedLimit; }
            set
            {
                if (value == m_SpeedLimit)
                {
                    return;
                }

                m_SpeedLimit = value;
                RaisePropertyChanged(() => SpeedLimit);
            }
        }

        public bool AccelerationAlarmVisible
        {
            get { return m_AccelerationAlarmVisible; }
            set
            {
                if (value == m_AccelerationAlarmVisible)
                {
                    return;
                }

                m_AccelerationAlarmVisible = value;
                RaisePropertyChanged(() => AccelerationAlarmVisible);
            }
        }

        public AccelerationAlarmType AccelerationAlarmType
        {
            set
            {
                if (value == m_AccelerationAlarmType)
                {
                    return;
                }

                m_AccelerationAlarmType = value;
                RaisePropertyChanged(() => AccelerationAlarmType);
            }
            get { return m_AccelerationAlarmType; }
        }
    }
}
