using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ScreenLightSetting : baseClass
    {
        private Rectangle m_Rect;

        private SolidBrush m_Brush;

        public override string GetInfo()
        {
            return "调节屏幕亮度视图";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();

            m_Brush = new SolidBrush(Color.Empty);

            nErrorObjectIndex = -1;
            return true;
        }

        public void InitData()
        {
            m_Rect = new Rectangle(0, 0, 800, 600);
        }


        public override void paint(Graphics g)
        {
            m_Brush.Color =
                Color.FromArgb((VoiceSetting.MaxLight - VoiceSetting.Light) * 150 / VoiceSetting.MaxLight, 0, 0, 0);
            g.FillRectangle(m_Brush, m_Rect);
        }
    }
}
