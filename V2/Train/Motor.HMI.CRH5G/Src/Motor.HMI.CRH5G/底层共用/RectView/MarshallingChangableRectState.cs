using System.Drawing;

namespace Motor.HMI.CRH5G.底层共用.RectView
{
    public class MarshallingChangableRectState : RectState
    {
        /// <summary>
        /// 显示时的单双组
        /// </summary>
        public MarshallingType ShowMarshallingType { set; get; }
        /// <summary>
        /// 当前车辆编号
        /// </summary>
        public int CarNum { get; set; }

        /// <summary>
        /// 是否切换到后八车（只在重连下有效）
        /// </summary>
        public bool IsCar16 { get; private set; }

        public MarshallingChangableRectState(int trainId, int firstLogicId, RectangleF[] rect, int carNum = -1, MarshallingType showMarshallingType = MarshallingType.SingleMarshalling)
            : base(trainId, firstLogicId, rect)
        {
            CarNum = carNum;
            ShowMarshallingType = showMarshallingType;
        }

        public override bool HasInit(CRH5GBase obj = null)
        {
            if (obj == null)
            {
                return base.HasInit();
            }
            else
            {
                if (this.TrainNumbIs16)
                {
                    if (ShowMarshallingType == MarshallingType.SingleMarshalling)
                    {
                        if (IsCar16)
                        {
                            return CarNum > 8;
                        }
                        else
                        {
                            return CarNum <= 8 && CarNum > 0;
                        }
                    }

                }
                else
                {
                    return CarNum <= 8 && CarNum > 0;
                }
            }

            return base.HasInit();

        }

        public void ChangeIsCar16()
        {
            IsCar16 = !IsCar16;
        }

        public override RectangleF GetRectLocal(CRH5GBase activeObj = null)
        {
            // if (activeObj == null || activeObj.ScreenIdentification == ScreenIdentification.ScreenTS)
            if (activeObj == null)
            {
                return base.GetRectLocal(null);
            }


            if (ShowMarshallingType == MarshallingType.SingleMarshalling)
            {
                switch (TrainInsideType)
                {
                    case TrainInside.Inside8:
                        CurrentRectId = 2;
                        return RectArr[2];
                    case TrainInside.Normal16:
                        CurrentRectId = 4;
                        return RectArr[4];
                    case TrainInside.Inside16:
                        CurrentRectId = 5;
                        return RectArr[5];
                    default:
                        CurrentRectId = 0;
                        return RectArr[0];
                }
            }
            switch (TrainInsideType)
            {
                case TrainInside.Inside8:
                    CurrentRectId = 3;
                    return RectArr[3];
                case TrainInside.Normal16:
                    CurrentRectId = 1;
                    return RectArr[1];
                case TrainInside.Inside16:
                    CurrentRectId = 3;
                    return RectArr[3];
                default:
                    CurrentRectId = 1;
                    return RectArr[1];

            }
        }
    }
}