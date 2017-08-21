using System.Drawing;

namespace Urban.Wuxi.TMS.����
{
    public class DoorState
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstDoorLogicId">�Ŷ�Ӧ�ĵ�һ���߼�λ</param>
        /// <param name="rect">������λ�ô�С</param>
        /// <param name="doorName">������</param>
        public DoorState(int firstDoorLogicId, RectangleF rect, string doorName)
        {
            m_Rect = rect;
            m_DoorName = doorName;
            m_DoorState = doorState.ͨѶ����;
            m_LogicIdArr = new int[7];
            for (int i = 0; i < m_LogicIdArr.Length; i++)
            {
                m_LogicIdArr[i] = firstDoorLogicId + i;
            }
        }

        /// <summary>
        /// ��״̬����
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
                m_DoorState = DoorState.doorState.ͨѶ����;
            }
        }

        /// <summary>
        /// ��״̬����
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
                case doorState.ͨѶ����:
                    return FormatStyle.m_OrangeBrush;
                case doorState.����:
                    return FormatStyle.m_WhiteBrush;
                case doorState.��������:
                    return  new SolidBrush(Color.BlueViolet);
                case doorState.�ϰ����:
                    return FormatStyle.m_BlueBrush;
                case doorState.��:
                    return FormatStyle.m_GreenBrush;
                case doorState.��:
                    return FormatStyle.m_GreyBrush;
                case doorState.����:
                    return FormatStyle.m_RedBrush;
            }
            return FormatStyle.m_OrangeBrush;
        }

        private bool[] m_LogicIdBools;
        //�߼���
        private readonly int[] m_LogicIdArr;
        /// <summary>
        /// ���߼��ӿ�
        /// </summary>
        public int[] LogicIdArr { get { return m_LogicIdArr; } }

        private readonly RectangleF m_Rect;

        private readonly string m_DoorName;

        private doorState m_DoorState;

        enum doorState : int
        {
            ͨѶ����,
            ����,
            ��������,
            �ϰ����,
            ��,
            ��,
            ����,
        }
    }
}