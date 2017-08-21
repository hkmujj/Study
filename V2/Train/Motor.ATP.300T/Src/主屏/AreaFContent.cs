using System.Drawing;

namespace Motor.ATP._300T.Ö÷ÆÁ
{
    public class AreaFContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        public AreaFContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
        }

        public void DrawAreaF(Graphics g)
        {
            m_ATPMainScreen.OpenFunBtnCtcs300T.Paint(g);
        }
    }
}