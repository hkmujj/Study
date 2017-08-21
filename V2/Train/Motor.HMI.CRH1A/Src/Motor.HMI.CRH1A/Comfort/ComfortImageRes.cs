using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.HighVoltage;

namespace Motor.HMI.CRH1A.Comfort
{
    public class ComfortImageRes
    {
        private static readonly ComfortImageRes m_Instance = new ComfortImageRes();
        public static ComfortImageRes Instance
        {
            get { return m_Instance; }
        }

        private ComfortImageRes()
        {

        }

        /// <summary>
        /// 所有的图, 从对象配置表中获得
        /// </summary>
        public Image[] Images { set; get; }

        /// <summary>
        /// 获取灯的图于
        /// </summary>
        /// <param name="isLight">是否亮</param>
        /// <returns></returns>
        public Image GetLightImage(bool isLight)
        {
            if (Images != null)
            {
                if (isLight)
                {
                    return Images[13];
                }
                return Images[12];
            }
            return null;
        }

        /// <summary>
        /// 获得车单元的图片
        /// </summary>
        /// <param name="comfortUnit"></param>
        /// <returns></returns>
        public Image GetTrainUnitImage(ComfortUnit comfortUnit)
        {
            if (Images == null)
            {
                return null;
            }

            var off = 0;
            switch (comfortUnit.RoomType)
            {
                case RoomType.Head:
                    off = 1;
                    break;
                case RoomType.Body:
                    off = 0;
                    break;
                case RoomType.Tail:
                    off = 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (comfortUnit.HvacState)
            {
                case HvacState.Normal:
                    //off += 0;
                    break;
                case HvacState.CutOff:
                    off += 9;
                    break;
                case HvacState.Fault:
                    off += 3;
                    break;
                case HvacState.TurnOff:
                    off += 6;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Images[off];
        }

        public Image GetTemperAdjustImage(ComfortButtonType btnType)
        {
            switch (btnType)
            {
                case ComfortButtonType.TemperatureUp:
                    return Images[14];
                case ComfortButtonType.TemperatureDown:
                    return Images[15];
                default:
                    throw new ArgumentOutOfRangeException("btnType");
            }
        }
    }
}
