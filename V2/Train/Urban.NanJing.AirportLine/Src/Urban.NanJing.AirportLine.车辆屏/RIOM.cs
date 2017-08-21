using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class RIOM : baseClass
    {
        private TrainEquipment m_Train;

        private List<RectText> m_RectTextList;

        public override string GetInfo()
        {
            return "RIOM";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            m_Train = new TrainEquipment();
            m_RectTextList = new List<RectText>();
            m_RectTextList.Add(new RectText(new Rectangle(70, 260, 75, 50), "RT313", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));
            m_RectTextList.Add(new RectText(new Rectangle(70, 350, 75, 50), "RIOM1", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));
            m_RectTextList.Add(new RectText(new Rectangle(70, 440, 75, 50), "RIOM2", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));

            m_RectTextList.Add(new RectText(new Rectangle(70 + 95 * ++i, 260, 75, 50), "RIOM", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));

            m_RectTextList.Add(new RectText(new Rectangle(70 + 95 * ++i, 260, 75, 50), "RIOM", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));

            m_RectTextList.Add(new RectText(new Rectangle(70 + 95 * ++i, 260, 75, 50), "RIOM", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));

            m_RectTextList.Add(new RectText(new Rectangle(70 + 95 * ++i, 260, 75, 50), "RIOM", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));

            m_RectTextList.Add(new RectText(new Rectangle(70 + 95 * ++i, 260, 75, 50), "RT313", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));
            m_RectTextList.Add(new RectText(new Rectangle(70 + 95 * i, 350, 75, 50), "RIOM1", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));
            m_RectTextList.Add(new RectText(new Rectangle(70 + 95 * i, 440, 75, 50), "RIOM2", 12, 1, Color.Black, Color.FromArgb(146, 242, 30), Color.Black, 1, true, null, true));
            return true;
        }

        private Color[] m_ColorArray = { Color.FromArgb(146, 242, 30), Color.Red };
        
        public override void paint(Graphics g)
        {
            m_Train.OnDraw(g);

            for (int i = 0; i < m_RectTextList.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[1153 + 2 * i + j])
                    {
                        m_RectTextList[i].BackgroundColor = m_ColorArray[j];
                    }
                }
            }

            DrawLine(g);

            foreach (var v in m_RectTextList)
            {
                v.OnDraw(g);
            }
        }

        private void DrawLine(Graphics g)
        {
            int i = 0;
            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * i++ + 37, 210), new Point(70 + 95 * (i - 1) + 37, 310));
            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * (i - 1) + 37, 310), new Point(70 + 95 * (i - 1) + 37, 350));
            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * (i - 1) + 37, 400), new Point(70 + 95 * (i - 1) + 37, 440));

            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * i++ + 37, 210), new Point(70 + 95 * (i - 1) + 37, 310));

            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * i++ + 37, 210), new Point(70 + 95 * (i - 1) + 37, 310));

            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * i++ + 37, 210), new Point(70 + 95 * (i - 1) + 37, 310));

            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * i++ + 37, 210), new Point(70 + 95 * (i - 1) + 37, 310));

            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * i++ + 37, 210), new Point(70 + 95 * (i - 1) + 37, 310));
            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * (i - 1) + 37, 310), new Point(70 + 95 * (i - 1) + 37, 350));
            g.DrawLine(Common.m_Black2Pen, new Point(70 + 95 * (i - 1) + 37, 400), new Point(70 + 95 * (i - 1) + 37, 440));

        }
    }
}



