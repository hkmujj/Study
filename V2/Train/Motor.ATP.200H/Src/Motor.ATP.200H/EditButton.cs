using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class EditButton : baseClass
    {
        private readonly Button[] m_GButton = new Button[8];

        public override string GetInfo()
        {
            return "ÐéÄâÓ²¼þ°´Å¥";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            nErrorObjectIndex = -1;
            return true;
        }

        public void InitData()
        {
            for (int i = 0; i < 8; i++)
            {
                m_GButton[i] = new Button();
                m_GButton[i].SetButtonColor(192, 192, 192);
                m_GButton[i].SetButtonText("F" + (i+1).ToString());
                m_GButton[i].SetButtonRect(805, i*75, 100, 72);
            }
        }

        public void GetValue()
        {


        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            OnDraw(dcGs);
            base.paint(dcGs);
        }

        public void OnDraw(Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                m_GButton[i].OnDraw(g);
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            ButtonStatus.ClearManaulUp();

            for (int i = 0; i < 8; i++)
            {
                if (nPoint.X < m_GButton[i].RectPosition.X + m_GButton[i].RectPosition.Width &&
                    nPoint.X > m_GButton[i].RectPosition.X && nPoint.Y > m_GButton[i].RectPosition.Y &&
                    nPoint.Y < m_GButton[i].RectPosition.Y + m_GButton[i].RectPosition.Height)
                {
                    m_GButton[i].OnButtonDown();
                    ButtonStatus.ManualIsRightButtonDown[i] = true;
                }
            }
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            ButtonStatus.ClearManaulDown();

            for (int i = 0; i < 8; i++)
            {
                if (nPoint.X < m_GButton[i].RectPosition.X + m_GButton[i].RectPosition.Width &&
                    nPoint.X > m_GButton[i].RectPosition.X && nPoint.Y > m_GButton[i].RectPosition.Y &&
                    nPoint.Y < m_GButton[i].RectPosition.Y + m_GButton[i].RectPosition.Height)
                {
                    m_GButton[i].OnButtonUp();
                    ButtonStatus.ManualIsRightButtonUp[i] = true;
                }
            }
            return true;
        }
    }

}
