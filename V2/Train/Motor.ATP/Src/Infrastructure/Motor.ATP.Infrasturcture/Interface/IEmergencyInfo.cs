using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motor.ATP.Infrasturcture.Interface
{
    public interface IEmergencyInfo : IATPPartial, IVisibility
    {
        /// <summary>
        /// 紧急消息
        /// </summary>
        EmergencyInfoType InfoType { get; }

        /// <summary>
        /// 是否有效
        /// </summary>
        bool IsEffective { get; }

    }

    public enum EmergencyInfoType
    {
        Unkown = 0,
        EmergencySignal = 1,
    }
}
