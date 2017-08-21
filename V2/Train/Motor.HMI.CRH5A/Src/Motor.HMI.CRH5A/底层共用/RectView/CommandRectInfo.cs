using System;
using System.Drawing;

namespace Motor.HMI.CRH5A.底层共用.RectView
{
    public class CommandRectInfo : RectState
    {
        public int CarNum { get; private set; }
        public bool IsCar16 { get; set; }
        public int LogicIndex { get; private set; }
        public CommandRectInfo(int trainId, int firstLogicId, RectangleF[] rect, int[] commandIdArrArr, int sendDataId, int carNum = -1)
            : base(trainId, firstLogicId, rect)
        {
            SendDataId = sendDataId;
            CarNum = carNum;
            m_CommandIdArr = commandIdArrArr;
            LogicIndex = firstLogicId;
        }

        /// <summary>
        /// 状态变化
        /// </summary>
        /// <param name="nParaGreen">绿色</param>
        /// <param name="nParaRed">红色</param>
        /// <param name="nParaFork">叉</param>
        [Obsolete]
        public new void ChangeRectState(bool nParaGreen, bool nParaRed, bool nParaFork)
        {
            if (nParaRed)
                CurrentRectState = RectStateName.Red;
            else if (nParaFork)
                CurrentRectState = RectStateName.Fork;
            else
                CurrentRectState = RectStateName.Green;
        }

        public void ChangeState(bool bPara)
        {
            CurrentRectState = bPara ? RectStateName.Fork : RectStateName.Green;
        }

        public override void ChangeRectState(CRH5ABase obj, bool nParaGreen, bool nParaRed, bool nParaYellow)
        {
            if (obj.ScreenIdentification == ScreenIdentification.ScreenTs)
            {
                if (nParaRed)
                    CurrentRectState = RectStateName.Red;
                else if (nParaYellow)
                    CurrentRectState = RectStateName.Fork;
                else
                    CurrentRectState = RectStateName.Green;
            }
            else
            {
                if (nParaRed)
                    CurrentRectState = RectStateName.Red;
                else if (nParaYellow)
                    CurrentRectState = RectStateName.Yellow;
                else
                    CurrentRectState = RectStateName.Green;
            }
        }

        public override bool HasInit(CRH5ABase obj = null)
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

        public override RectangleF GetRectLocal(CRH5ABase activeObj = null)
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
        public MarshallingType ShowMarshallingType { get; set; }

        /// <summary>
        /// 获取当前命令框在所有矩形框中的编号
        /// </summary>
        /// <returns></returns>
        public int CurrentCommandIdInAllRects
        {
            get { return m_CommandIdArr[CurrentRectId]; }
        }

        //发送位
        public int SendDataId { get; private set; }

        private readonly int[] m_CommandIdArr; //编号
    }
}