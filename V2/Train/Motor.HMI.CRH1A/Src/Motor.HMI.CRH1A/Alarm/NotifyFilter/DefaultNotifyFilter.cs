using System.Drawing;
using Motor.HMI.CRH1A.Alarm.Fault;

namespace Motor.HMI.CRH1A.Alarm.NotifyFilter
{
    class DefaultNotifyFilter: INotifyFilter
    {
        public bool Filter(ExceptionData data)
        {

            if (FaultTypeHelper.GetFaultShowAttribute(data.ExType).Color == Color.Black)
            {
                return false;
            }

            return true;

        }
    }
}
