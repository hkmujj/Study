using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH3C380B.Base.底层共用;

namespace Motor.HMI.CRH3C380B.Base.Restart
{
    public class ReconfigViewConfigingPage : ReconfigViewPage
    {
        private static int m_ConfigingCount;

        private const int MaxConfigingCunt = 20;

        private GDIRectText m_ConstText;

        private Rectangle m_OutlineRectangle;

        public override void Init()
        {
            m_ConstText = new GDIRectText
            {
                OutLineRectangle = new Rectangle(60, 540, 700, 45),
                Text = "确定配置中…",
                DrawFont = FontsItems.FontC18,
                BkColor = Color.White,
                TextColor = Color.Black,
                TextFormat =
                    new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center}
            };
            m_OutlineRectangle = Rectangle.Inflate(m_ConstText.OutLineRectangle, -5, -5);
        }

        public override void OnDraw(Graphics g)
        {
            if (m_ConfigingCount++ < MaxConfigingCunt)
            {
                m_ConstText.OnDraw(g);
                g.DrawRectangle(Pens.Black, m_OutlineRectangle);
            }
            else
            {
                ReconfigState = ReconfigState.ConfigCompleted;
            }
        }

        public override void Reset()
        {
            ReconfigState = ReconfigState.Configing;
            m_ConfigingCount = 0;
        }
    }
}