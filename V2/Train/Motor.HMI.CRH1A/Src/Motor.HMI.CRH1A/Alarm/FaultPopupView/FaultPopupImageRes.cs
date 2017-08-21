using System;
using System.Drawing;
using Motor.HMI.CRH1A.Alarm.Fault;

namespace Motor.HMI.CRH1A.Alarm.FaultPopupView
{
    public class FaultPopupImageRes
    {
        /// <summary>
        /// 所有的图片
        /// </summary>
        public static Image[] Images { set; get; }

        public static Image GetImage(GT_FaultPopupView faultPopupView)
        {
            switch (faultPopupView.CurrntExceptionData.ExType)
            {
                case FaultType.OperError:
                    return null;
                case FaultType.Passenger:
                    break;
                case FaultType.A:
                    break;
                case FaultType.B:
                    break;
                case FaultType.E:
                    break;
                case FaultType.Manaul:
                    break;
                case FaultType.Event:
                    break;
                case FaultType.Warning:
                    return Images[0];
                case FaultType.Info:
                    return Images[1];
                default:
                    throw new ArgumentOutOfRangeException(string.Format("There is no image for FaultType = {0}.", faultPopupView.CurrntExceptionData.ExType));
            }
            return null;
        }
    }
}
