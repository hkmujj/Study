using System.Drawing;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.车门
{
    /// <summary>
    /// 车门类
    /// </summary>
    public class DoorUnit
    {
        /// <summary>
        /// 允许绘制
        /// </summary>
        public bool Drawing { get; set; }

        /// <summary>
        /// 车厢编号
        /// </summary>
        public int TrainID { get; private set; }

        private readonly string m_Name;
        private RectangleF m_RectF;
        private readonly RectangleF m_RectF8;
        private readonly RectangleF m_RectF8Inside;
        private readonly RectangleF m_RectF16;
        private readonly RectangleF m_RectF16Inside;

        /// <summary>
        /// 当前门的状态
        /// </summary>
        public DoorStatus TheDoorState { get; private set; }

        /// <summary>
        /// 当前编组方式是否为16编组
        /// </summary>
        public bool Marshalling16 { get; set; }

        /// <summary>
        /// 换端
        /// </summary>
        public bool Inside { get; set; }

        public int LogicFault { get; private set; }

        public int LogicIdOfOpen { get; private set; }

        public int LogicIdOfLocked { get; private set; }

        public int LogicIdOfUnkown { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainID">车厢号0-16</param>
        /// <param name="doorName">车门名称</param>
        /// <param name="rectF">车门位置数组，包括8车，8车换端，16车，16车换端</param>
        /// <param name="doorStateOfLogicID">门状态判断量，包括门故障，门开，门锁闭</param>
        public DoorUnit(int trainID, string doorName, RectangleF[] rectF, int[] doorStateOfLogicID)
        {
            TheDoorState = DoorStatus.Closed;
            TrainID = trainID;
            m_Name = doorName;
            if (rectF.Length == 4)
            {
                m_RectF8 = rectF[0];
                m_RectF8Inside = rectF[1];
                m_RectF16 = rectF[2];
                m_RectF16Inside = rectF[3];
            }
            if (doorStateOfLogicID.Length == 4)
            {
                LogicFault = doorStateOfLogicID[0];
                LogicIdOfOpen = doorStateOfLogicID[1];
                LogicIdOfLocked = doorStateOfLogicID[2];
                LogicIdOfUnkown = doorStateOfLogicID[3];
            }
        }

        /// <summary>
        /// 绘制门
        /// </summary>
        /// <param name="g"></param>
        public void DrawDoorState(Graphics g)
        {
            if (!Drawing || string.IsNullOrEmpty(m_Name))
            {
                return;
            }

            //位置
            UpdateRegion();

            if (TheDoorState == DoorStatus.Unknown)
            {
                g.DrawImage(CommonImages.StateUnkown, m_RectF);
            }
            else
            {
                g.FillRectangle(GetDoorStateColor(TheDoorState), m_RectF);
                g.DrawString(m_Name,
                    FontsItems.FontC10,
                    (TheDoorState == DoorStatus.Fault || TheDoorState == DoorStatus.Closed)
                        ? SolidBrushsItems.WhiteBrush
                        : SolidBrushsItems.BlackBrush,
                    m_RectF,
                    FontsItems.TheAlignment(FontRelated.居中));
                g.DrawRectangle(PenItems.WhiltPen, Rectangle.Round(m_RectF));
            }
        }

        private void UpdateRegion()
        {
            if (!Marshalling16 && !Inside)
            {
                m_RectF = m_RectF8;
            }
            else if (!Marshalling16 && Inside)
            {
                m_RectF = m_RectF8Inside;
            }
            else if (Marshalling16 && !Inside)
            {
                m_RectF = m_RectF16;
            }
            else
            {
                m_RectF = m_RectF16Inside;
            }
        }

        /// <summary>
        /// 更新门状态变化
        /// </summary>
        /// <param name="boolOfFault">门故障</param>
        /// <param name="boolOfOpen">门开</param>
        /// <param name="boolOfLocked">门锁闭</param>
        /// <param name="isUnkown"></param>
        public void Update(bool boolOfFault, bool boolOfOpen, bool boolOfLocked, bool isUnkown)
        {
            if (isUnkown)
            {
                TheDoorState = DoorStatus.Unknown;
            }
            else if (boolOfFault)
            {
                TheDoorState = DoorStatus.Fault;
            }
            else if (boolOfLocked)
            {
                TheDoorState = DoorStatus.Locked;
            }
            else if (boolOfOpen)
            {
                TheDoorState = DoorStatus.Open;
            }
            else
            {
                TheDoorState = DoorStatus.Closed;
            }
        }



        private SolidBrush GetDoorStateColor(DoorStatus doorState)
        {
            switch (doorState)
            {
                case DoorStatus.Fault:
                    return SolidBrushsItems.RedBrush1;
                case DoorStatus.Open:
                    return SolidBrushsItems.WhiteBrush;
                case DoorStatus.Locked:
                    return SolidBrushsItems.YellowBrush;
                case DoorStatus.Closed:
                    return SolidBrushsItems.BlackBrush;
                default:
                    return SolidBrushsItems.BlackBrush;
            }
        }

        public enum DoorStatus
        {
            Unknown,
            Fault,
            Open,
            Locked,
            Closed
        }
    }
}