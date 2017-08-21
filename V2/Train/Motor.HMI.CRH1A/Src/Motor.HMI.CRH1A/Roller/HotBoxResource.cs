using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH1A.Roller
{
    public class HotBoxResource
    {
        private readonly CRH1BaseClass m_ViewClass;

        public HotBoxResource(CRH1BaseClass viewClass)
        {
            m_ViewClass = viewClass;
        }

        public void ChangeState(ControllerButtonName buttonName, bool value)
        {
            var index = 0;
            switch (buttonName)
            {
                case ControllerButtonName.ResetAlarm :
                    index = 0;
                    break;
                case ControllerButtonName.RelaseLock :
                    index = 1;
                    break;
                default :
                    throw new ArgumentOutOfRangeException("buttonName");
            }

            m_ViewClass.OnPost(CmdType.SetBoolValue, m_ViewClass.UIObj.OutBoolList[index], value ? 1 : 0, 0);

        }
    }
}
