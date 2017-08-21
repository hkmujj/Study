using System;
using System.Drawing;

namespace Motor.HMI.CRH5A.底层共用.RectView
{
    /// <summary>
    /// 矩形框
    /// </summary>
    public class RectState
    {
        /// <summary>
        /// 第一个8车       
        /// 第二个16车
        /// 第三个8车换端
        /// 第四个16车换端 
        /// </summary>
        public RectangleF[] RectArr { get; set; }

        /// <summary>
        /// 车辆换端状态
        /// </summary>
        public TrainInside TrainInsideType { get; set; }

        /// <summary>
        /// 当前是16车编组
        /// </summary>
        public bool TrainNumbIs16 { get; set; }

        /// <summary>
        /// 8车情况下是否需要绘制
        /// </summary>
        protected bool DrawIn8 { set; get; }

        protected int TheTrainId;

        /// <summary>
        /// 判断状态的逻辑数组
        /// </summary>
        public int[] LogicIds { get; protected set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        protected RectStateName CurrentRectState
        {
            get { return m_CurrentRectState; }
            set
            {
                if (m_CurrentRectState == value)
                {
                    return;
                }
                m_CurrentRectState = value;
            }
        }


        /// <summary>
        /// 当前矩形框编号
        /// </summary>
        protected int CurrentRectId;

        private RectStateName m_CurrentRectState;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="trainId">车厢号</param>
        /// <param name="firstLogicId">第一个逻辑号</param>
        /// <param name="rect">矩形框(第一个8车 第二个16车 第三个8车换端 第四个16车换端)</param>
        public RectState(int trainId, int firstLogicId, RectangleF[] rect)
        {
            if (rect.Length < 4) return;
            RectArr = new RectangleF[rect.Length];
            rect.CopyTo(RectArr, 0);
            CurrentRectState = RectStateName.Black;
            TrainInsideType = TrainInside.Normal8;
            TrainNumbIs16 = false;
            DrawIn8 = trainId <= 8;
            TheTrainId = trainId;
            LogicIds = new[] { firstLogicId, firstLogicId + 1, firstLogicId + 2 };
        }

        /// <summary>
        /// 状态变化
        /// </summary>
        /// <param name="nParaGreen">绿色</param>
        /// <param name="nParaRed">红色</param>
        /// <param name="nParaYellow">黄色</param>
        [Obsolete]
        public void ChangeRectState(bool nParaGreen, bool nParaRed, bool nParaYellow)
        {
            if (nParaRed)
                CurrentRectState = RectStateName.Red;
            else if (nParaYellow)
                CurrentRectState = RectStateName.Yellow;
            else
                CurrentRectState = RectStateName.Green;
        }

        public virtual void ChangeRectState(CRH5ABase obj, bool nParaGreen, bool nParaRed, bool nParaYellow)
        {
            //if (obj.ScreenIdentification == ScreenIdentification.ScreenTS)
            //{
            //    if (nParaRed)
            //        CurrentRectState = RectStateName.Red;
            //    else if (nParaYellow)
            //        CurrentRectState = RectStateName.Yellow;
            //    else
            //        CurrentRectState = RectStateName.Green;
            //}
            //else
            //{
            if (nParaRed)
                CurrentRectState = RectStateName.Red;
            else if (nParaYellow)
                CurrentRectState = RectStateName.Fork;
            else if (nParaGreen)
                CurrentRectState = RectStateName.Green;
            else
                CurrentRectState = RectStateName.Black;
            //}
        }

        /// <summary>
        /// 根据换端信息改变位置
        /// </summary>
        /// <returns></returns>
        public virtual RectangleF GetRectLocal(CRH5ABase activeObj = null)
        {
            switch (TrainInsideType)
            {
                case TrainInside.Inside8:
                    CurrentRectId = 2;
                    return RectArr[2];
                case TrainInside.Normal16:
                    CurrentRectId = 1;
                    return RectArr[1];
                case TrainInside.Inside16:
                    CurrentRectId = 3;
                    return RectArr[3];
                default:
                    CurrentRectId = 0;
                    return RectArr[0];
            }
        }

        /// <summary>
        /// 绘制状态
        /// </summary>
        /// <param name="g"></param>
        /// <param name="activeObj"></param>
        public void DrawRectState(Graphics g, CRH5ABase activeObj = null)
        {
            if (!HasInit(activeObj))
            {
                return; //初始化失败
            }

            switch (CurrentRectState)
            {
                case RectStateName.Red:
                    g.FillRectangle(SolidBrushsItems.RedBrush1, GetRectLocal(activeObj));
                    break;
                case RectStateName.Green:
                    g.FillRectangle(SolidBrushsItems.GreenBrush1, GetRectLocal(activeObj));
                    break;
                case RectStateName.Yellow:
                    g.FillRectangle(SolidBrushsItems.YellowBrush1, GetRectLocal(activeObj));
                    break;
                case RectStateName.Fork:
                    g.DrawLine(PenItems.WhitePen, GetRectLocal(activeObj).X, GetRectLocal(activeObj).Y, GetRectLocal(activeObj).X + GetRectLocal(activeObj).Width, GetRectLocal(activeObj).Y + GetRectLocal(activeObj).Height);
                    g.DrawLine(PenItems.WhitePen, GetRectLocal(activeObj).X + GetRectLocal(activeObj).Width, GetRectLocal(activeObj).Y, GetRectLocal(activeObj).X, GetRectLocal(activeObj).Y + GetRectLocal(activeObj).Height);
                    break;
                case RectStateName.Black:
                    break;
            }
            g.DrawRectangle(PenItems.WhitePen, Rectangle.Round(GetRectLocal(activeObj)));
        }

        public void RefreshStateColor(CRH5ABase baseClass)
        {
            if (baseClass.BoolList[LogicIds[0]])
            {
                CurrentRectState = RectStateName.Black;
            }
            else if (baseClass.BoolList[LogicIds[1]])
            {
                CurrentRectState = RectStateName.Green;
            }
            else if (baseClass.BoolList[LogicIds[2]])
            {
                CurrentRectState = RectStateName.Red;
            }
        }
        public virtual bool HasInit(CRH5ABase obj = null)
        {
            if (RectArr == null) return false;

            if (!TrainNumbIs16 && !DrawIn8) return false;

            return true;
        }



        /// <summary>
        /// 状态名
        /// </summary>
        /// 
        public enum RectStateName
        {
            /// <summary>
            /// 无状态、即黑色
            /// </summary>
            Black,

            /// <summary>
            /// 故障
            /// </summary>
            Red,

            /// <summary>
            /// 正常
            /// </summary>
            Green,

            /// <summary>
            /// 警告
            /// </summary>
            Yellow,

            /// <summary>
            /// 叉
            /// </summary>
            Fork,
        }

    }
}