using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util;

namespace Urban.Iran.HMI
{
    public class PISStationSelectUnit : GDIButton
    {
        private readonly GDIButton m_ChooseButton;

        private readonly GDIRectText m_ChoosedText;

        public string StaionName { set { m_ChoosedText.Text = value; } get { return m_ChoosedText.Text; } }

        public PISStationSelectUnit(GDIButton btn, GDIRectText txt)
        {
            m_ChooseButton = btn;
            m_ChoosedText = txt;
        }

        public void SetComponentColor(Color color)
        {
            m_ChooseButton.TextColor = color;
            m_ChoosedText.TextColor = color;
        }

        public override bool OnMouseDown(Point point)
        {
            if (m_ChooseButton.OutLineRectangle.Contains(point))
            {
                HandleUtil.OnHandle(ButtonDownEvent, this, null);
                return true;
            }
            return false;
        }

        public override void OnDraw(Graphics g)
        {

            if (Visible)
            {
                m_ChooseButton.OnDraw(g);

                m_ChoosedText.OnDraw(g);
            }
          
        }
    }
}