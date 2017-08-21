using System.Drawing;

namespace Urban.Wuxi.TMS.运行
{
    public class DoorState
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstDoorLogicId">门对应的第一个逻辑位</param>
        /// <param name="rect">门所在位置大小</param>
        /// <param name="doorName">门名称</param>
        public DoorState(int firstDoorLogicId, RectangleF rect, string doorName)
        {
            m_Rect = rect;
            m_DoorName = doorName;
            m_DoorState = doorState.通讯故障;
            m_LogicIdArr = new int[7];
            for (int i = 0; i < m_LogicIdArr.Length; i++)
            {
                m_LogicIdArr[i] = firstDoorLogicId + i;
            }
        }

        /// <summary>
        /// 门状态更新
        /// </summary>
        /// <param name="doorState"></param>
        public void DoorStateUpdata(bool[] doorState)
        {
            if (doorState.Length != 7) return;
            for (int i = 0; i < doorState.Length; i++)
            {
                if (doorState[i])
                {
                    m_DoorState = (doorState)i;
                    break;
                }
                m_DoorState = DoorState.doorState.通讯故障;
            }
        }

        /// <summary>
        /// 门状态绘制
        /// </summary>
        /// <param name="g"></param>
        public void DrawDoorState(Graphics g)
        {
            g.FillRectangle(GetDoorsBrush(), m_Rect);
            g.DrawString(m_DoorName, FormatStyle.m_Font10B, FormatStyle.m_BlackBrush, m_Rect);
        }

        private SolidBrush GetDoorsBrush()
        {
            switch (m_DoorState)
            {
                case doorState.通讯故障:
                    return FormatStyle.m_OrangeBrush;
                case doorState.隔离:
                    return FormatStyle.m_WhiteBrush;
                case doorState.紧急解锁:
                    return  new SolidBrush(Color.BlueViolet);
                case doorState.障碍检测:
                    return FormatStyle.m_BlueBrush;
                case doorState.关:
                    return FormatStyle.m_GreenBrush;
                case doorState.开:
                    return FormatStyle.m_GreyBrush;
                case doorState.故障:
                    return FormatStyle.m_RedBrush;
            }
            return FormatStyle.m_OrangeBrush;
        }

        private bool[] m_LogicIdBools;
        //逻辑号
        private readonly int[] m_LogicIdArr;
        /// <summary>
        /// 门逻辑接口
        /// </summary>
        public int[] LogicIdArr { get { return m_LogicIdArr; } }

        private readonly RectangleF m_Rect;

        private readonly string m_DoorName;

        private doorState m_DoorState;

        enum doorState : int
        {
            通讯故障,
            隔离,
            紧急解锁,
            障碍检测,
            关,
            开,
            故障,
        }
    }
}