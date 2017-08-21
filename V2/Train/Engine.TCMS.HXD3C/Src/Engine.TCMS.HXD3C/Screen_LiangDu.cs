using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class Screen_LiangDu : baseClass
    {
        public override string GetInfo()
        {
            return "��Ļ����";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            //3
            nErrorObjectIndex = -1;

            m_Rects = new Rectangle(0, 0, 800, 600);

            return true;
        }

        public override void paint(Graphics g)
        {
            g.FillRectangle(m_Background, m_Rects);
        }

        /// <summary>
        /// ��ʼ�������
        /// </summary>

        //���Ȼ���
        public static SolidBrush m_Background = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        public static SolidBrush m_BackgroundBrush0 = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
        public static SolidBrush m_BackgroundBrush1 = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        public static SolidBrush m_BackgroundBrush2 = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
        /// <summary>
        /// ���꼯
        /// </summary>
        public Rectangle m_Rects;
    }
}