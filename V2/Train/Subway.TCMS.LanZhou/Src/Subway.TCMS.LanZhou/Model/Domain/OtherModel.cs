using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Model.Domain.Other;

namespace Subway.TCMS.LanZhou.Model.Domain
{
    [Export]
    public class OtherModel : NotificationObject
    {
        private TimeSpan m_ShowingTimeDifference;
        private DateTime m_NowInDDU;
        private UserControlName m_PageName;
        private List<CarFireAlarmItem> m_FireAlarm;
        private  CarCommunicationStatusData m_CarCommunicationStatus;

        public OtherModel()
        {
            m_CarCommunicationStatus = new CarCommunicationStatusData();
        }
        public  CarCommunicationStatusData CarCommunicationStatus
        {
            get { return m_CarCommunicationStatus; }
            set
            {
                if (value == m_CarCommunicationStatus)
                {
                    return;
                }
                m_CarCommunicationStatus = value;
                RaisePropertyChanged(() => CarCommunicationStatus);
            }
        }

        public List<CarFireAlarmItem> FireAlarm
        {
            get { return m_FireAlarm; }
            set
            {
                if (value == m_FireAlarm)
                {
                    return;
                }
                m_FireAlarm = value;
                RaisePropertyChanged(() => FireAlarm);
            }
        }

        public UserControlName PageName
        {
            get { return m_PageName; }
            set
            {
                m_PageName = value;
                RaisePropertyChanged(() => PageName);
            }
        }



        public TimeSpan ShowingTimeDifference
        {
            get { return m_ShowingTimeDifference; }
            set
            {
                if (value.Equals(m_ShowingTimeDifference))
                {
                    return;
                }

                m_ShowingTimeDifference = value;
                if (m_ShowingTimeDifference == null)
                {
                    m_ShowingTimeDifference = TimeSpan.Zero;
                }
                RaisePropertyChanged(() => ShowingTimeDifference);
                RaisePropertyChanged(() => ShowingDateTime);
            }
        }
        public DateTime NowInDDU
        {
            set
            {
                if (value.Equals(m_NowInDDU))
                {
                    return;
                }

                m_NowInDDU = value;
                RaisePropertyChanged(() => NowInDDU);
                RaisePropertyChanged(() => ShowingDateTime);
            }
            get { return m_NowInDDU; }
        }

        public DateTime ShowingDateTime
        {
            get { return m_NowInDDU.Add(ShowingTimeDifference); }
        }
    }
}