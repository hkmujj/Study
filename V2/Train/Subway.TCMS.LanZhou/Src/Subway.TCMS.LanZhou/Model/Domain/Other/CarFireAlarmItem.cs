using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Other
{
    public class CarFireAlarmItem : NotificationObject
    {
        private CarFireAlarmStatus m_CarFireAlarmStatus;

        public CarFireAlarmItem(CarFireAlarmStatusConfig carFireAlarmStatusConfig)
        {
            CarFireAlarmStatusConfig = carFireAlarmStatusConfig;
        }

        public CarFireAlarmStatusConfig CarFireAlarmStatusConfig { get; private set; }  

        public CarFireAlarmStatus CarFireAlarmStatus
        {
            get { return m_CarFireAlarmStatus; }
            set
            {
                if (value == m_CarFireAlarmStatus)
                {
                    return;
                }
                m_CarFireAlarmStatus = value;
                RaisePropertyChanged(() => CarFireAlarmStatus);
            }
        }
    }
}
