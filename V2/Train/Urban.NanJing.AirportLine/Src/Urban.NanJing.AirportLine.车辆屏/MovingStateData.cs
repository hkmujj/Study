using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace Urban.NanJing.AirportLine.DDU
{
    public class MovingStateData
    {
        private RectText m_Rect;
        private string m_Label;
        public List<RectText> m_StateList = new List<RectText>();
        private Point m_LabelPoint;

        public MovingStateData(Rectangle rect, string label)
        {
            m_Label = label;
            m_Rect = new RectText(rect, "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Blue, 1, true, null, true);

            m_LabelPoint = new Point(rect.X + 6, rect.Y + 10);
            for (int i = 0; i < 6; i++)
            {
                m_StateList.Add(new RectText(new Rectangle(Common.m_InitialPosX + Common.m_FirstTrainRect.X + 73 * i, rect.Y + 2, Common.m_FirstTrainRect.Width, Common.m_FirstTrainRect.Height), "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Black, 1, true));
            }
        }

        public void OnDraw(Graphics g)
        {
            m_Rect.OnDraw(g);
            g.DrawString(m_Label, Common.m_16Font, Common.m_BlueBrush, m_LabelPoint);


            foreach (var v in m_StateList)
            {
                v.OnDraw(g);
            }
        }
    }

    public class MovingStateData2
    {
        private RectText m_Rect;
        private string m_Label1;
        private string m_Label2;

        public List<RectText> m_Draught = new List<RectText>();
        public List<RectText> m_Brake = new List<RectText>();
        private Point m_LabelPoint1;
        private Point m_LabelPoint2;

        public MovingStateData2(Rectangle rect, string label1, string label2)
        {
            m_Label1 = label1;
            m_Label2 = label2;

            m_Rect = new RectText(rect, "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Blue, 1, true, null, true);

            m_LabelPoint1 = new Point(rect.X + 6, rect.Y + 4);
            m_LabelPoint2 = new Point(rect.X + 6, rect.Y + 25);
            for (int i = 0; i < 4; i++)
            {
                m_Draught.Add(new RectText(new Rectangle(Common.m_InitialPosX + Common.m_FirstTrainRect.X + 73 * (i + 1), rect.Y + 2, Common.m_FirstTrainRect.Width, rect.Height / 2 - 4), "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Black, 1, true));
                m_Brake.Add(new RectText(new Rectangle(Common.m_InitialPosX + Common.m_FirstTrainRect.X + 73 * (i + 1), rect.Y + rect.Height / 2, Common.m_FirstTrainRect.Width, rect.Height / 2 - 2), "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Black, 1, true));
            }
        }

        public void OnDraw(Graphics g)
        {
            m_Rect.OnDraw(g);
            g.DrawString(m_Label1, Common.m_16Font, Common.m_BlueBrush, m_LabelPoint1);
            g.DrawString(m_Label2, Common.m_16Font, Common.m_BlueBrush, m_LabelPoint2);

            foreach (var v in m_Draught)
            {
                v.OnDraw(g);
            }

            foreach (var v in m_Brake)
            {
                v.OnDraw(g);
            }
        }
    }

  
}
