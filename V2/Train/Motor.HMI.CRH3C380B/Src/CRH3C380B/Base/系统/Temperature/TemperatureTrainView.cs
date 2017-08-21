using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;
using Motor.HMI.CRH3C380B.Base.底层共用;

namespace Motor.HMI.CRH3C380B.Base.系统.Temperature
{


    public class TemperatureTrainView
    {
        private bool m_IsReverse;
        private Point m_StartPoint;

        private readonly Pen m_VirtualLinePen;
        private readonly Pen m_LinePen;

        private readonly List<CommonInnerControlBase> m_ControlCollection;

        public TemperatureTrainView(bool bPara, Point pPara, List<CommonInnerControlBase> cPara)
        {
            m_IsReverse = bPara;
            m_StartPoint = pPara;
            m_ControlCollection = cPara;
            m_VirtualLinePen = new Pen(Color.White, 2f)
                        {
                            DashStyle = DashStyle.Custom,
                            DashPattern = new float[] { 3, 3 }
                        };
            m_LinePen = new Pen(Color.White, 2f);
            var x1 = pPara.X;
            var y1 = pPara.Y;
            const int length1 = 15;
            const int length2 = 30;
            const int length3 = 200;
            const int length4 = 2;
            const int length5 = 75;

            var line1 = new Line(new Point(x1, y1), new Point(x1, y1 + length1))
                        {
                            Color = Color.White,
                            Tag = 1,
                            LinePen = m_LinePen
                        };
            var line2 = new Line(new Point(x1, y1), new Point(x1 + (bPara ? -length2 : length2), y1 - (int)(length1 * 0.67)))
            {
                Color = Color.White,
                Tag = 2,
                LinePen = m_LinePen
            };
            var line3 = new Line(line2.EndPoint, new Point(line2.EndPoint.X + (bPara ? -length3 : length3), line2.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 3,
                LinePen = m_LinePen
            };
            var line4 = new Line(line1.EndPoint, new Point(line1.EndPoint.X + (bPara ? -length3 - length2 : length3 + length2), line1.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 4,
                LinePen = m_LinePen
            };
            var line5 = new Line(line3.EndPoint, line4.EndPoint)
            {
                Color = Color.White,
                Tag = 5,
                LinePen = m_LinePen
            };
            var line6 = new Line(new Point(line3.EndPoint.X + (bPara ? -length4 : length4), line3.EndPoint.Y), new Point(line4.EndPoint.X + (bPara ? -length4 : length4), line4.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_LinePen
            };
            var line7 = new Line(line6.StartPoint, new Point(line6.StartPoint.X + (bPara ? -length5 : length5), line6.StartPoint.Y))
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_VirtualLinePen
            };
            var line8 = new Line(line6.EndPoint, new Point(line6.EndPoint.X + (bPara ? -length5 : length5), line6.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_VirtualLinePen
            };
            var line9 = new Line(line7.EndPoint, line8.EndPoint)
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_LinePen
            };
            var line10 = new Line(new Point(line9.StartPoint.X + (bPara ? -length4 : length4), line9.StartPoint.Y), new Point(line9.EndPoint.X + (bPara ? -length4 : length4), line9.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_LinePen
            };
            var line11 = new Line(line10.StartPoint, new Point(line10.StartPoint.X + (bPara ? -length3 : length3), line10.StartPoint.Y))
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_LinePen
            };
            var line12 = new Line(line10.EndPoint, new Point(line10.EndPoint.X + (bPara ? -length3 : length3), line10.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_LinePen
            };
            var line13 = new Line(line11.EndPoint, line12.EndPoint)
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_LinePen
            };
            var line14 = new Line(new Point(line13.StartPoint.X + (bPara ? -length4 : length4), line13.StartPoint.Y), new Point(line13.EndPoint.X + (bPara ? -length4 : length4), line13.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_LinePen
            };

            var line15 = new Line(line14.StartPoint, new Point(line14.StartPoint.X + (bPara ? -length5 / 3 : length5 / 3), line14.StartPoint.Y))
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_VirtualLinePen
            };
            var line16 = new Line(line14.EndPoint, new Point(line14.EndPoint.X + (bPara ? -length5 / 3 : length5 / 3), line14.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 6,
                LinePen = m_VirtualLinePen
            };

            var textSzie = new Size(line4.StartPoint.Y - line3.StartPoint.Y + 5, line4.StartPoint.Y - line3.StartPoint.Y);

            const int textLength1 = 10;
            const int textLength2 = 20;
            const int textLength3 = 35;
            StringFormat draFormat=new StringFormat
            {
                                       Alignment = bPara ? StringAlignment.Far : StringAlignment.Near,
                                       LineAlignment = StringAlignment.Center 
                                   };
            var text1 = new GDIRectText
            {
                            Text = "M1",
                            NeedDarwOutline = false,
                            OutLineRectangle = new Rectangle(line1.StartPoint.X + (bPara ? -textLength2 - textSzie.Width : textLength2), line3.StartPoint.Y, textSzie.Width, textSzie.Height),
                            DrawFont = FontsItems.FontC11,
                            TextFormat = draFormat
                        };
            var text2 = new GDIRectText
            {
                Text = "M2",
                NeedDarwOutline = false,
                OutLineRectangle = new Rectangle(text1.OutLineRectangle.X + (bPara ? -textLength2 - textSzie.Width : textLength2 + textSzie.Width), line3.StartPoint.Y, textSzie.Width, textSzie.Height),
                DrawFont = FontsItems.FontC11,
                TextFormat = draFormat
            };
            var text3 = new GDIRectText
            {
                Text = "M3",
                NeedDarwOutline = false,
                OutLineRectangle = new Rectangle(text2.OutLineRectangle.X + (bPara ? -textLength3 - textSzie.Width : textLength3 + textSzie.Width), line3.StartPoint.Y, textSzie.Width, textSzie.Height),
                DrawFont = FontsItems.FontC11,
                TextFormat = draFormat
            };
            var text4 = new GDIRectText
            {
                Text = "M4",
                NeedDarwOutline = false,
                OutLineRectangle = new Rectangle(text3.OutLineRectangle.X + (bPara ? -textLength2 - textSzie.Width : textLength2 + textSzie.Width), line3.StartPoint.Y, textSzie.Width, textSzie.Height),
                DrawFont = FontsItems.FontC11,
                TextFormat = draFormat
            };

            var text5 = new GDIRectText
            {
                Text = "M1",
                NeedDarwOutline = false,
                OutLineRectangle = new Rectangle(line10.StartPoint.X + (bPara ? -textLength1 - textSzie.Width : textLength1), line3.StartPoint.Y, textSzie.Width, textSzie.Height),
                DrawFont = FontsItems.FontC11,
                TextFormat = draFormat
            };
            var text6 = new GDIRectText
            {
                Text = "M2",
                NeedDarwOutline = false,
                OutLineRectangle = new Rectangle(text5.OutLineRectangle.X + (bPara ? -textLength2 - textSzie.Width : textLength2 + textSzie.Width), line3.StartPoint.Y, textSzie.Width, textSzie.Height),
                DrawFont = FontsItems.FontC11,
                TextFormat = draFormat
            };
            var text7 = new GDIRectText
            {
                Text = "M3",
                NeedDarwOutline = false,
                OutLineRectangle = new Rectangle(text6.OutLineRectangle.X + (bPara ? -textLength3 - textSzie.Width : textLength3 + textSzie.Width), line3.StartPoint.Y, textSzie.Width, textSzie.Height),
                DrawFont = FontsItems.FontC11,
                TextFormat = draFormat
            };
            var text8 = new GDIRectText
            {
                Text = "M4",
                NeedDarwOutline = false,
                OutLineRectangle = new Rectangle(text7.OutLineRectangle.X + (bPara ? -textLength2 - textSzie.Width : textLength2 + textSzie.Width), line3.StartPoint.Y, textSzie.Width, textSzie.Height),
                DrawFont = FontsItems.FontC11,
                TextFormat = draFormat
            };

            m_ControlCollection.Add(text1);
            m_ControlCollection.Add(text2);
            m_ControlCollection.Add(text3);
            m_ControlCollection.Add(text4);
            m_ControlCollection.Add(text5);
            m_ControlCollection.Add(text6);
            m_ControlCollection.Add(text7);
            m_ControlCollection.Add(text8);

            m_ControlCollection.Add(line1);
            m_ControlCollection.Add(line2);
            m_ControlCollection.Add(line3);
            m_ControlCollection.Add(line4);
            m_ControlCollection.Add(line5);
            m_ControlCollection.Add(line6);
            m_ControlCollection.Add(line7);
            m_ControlCollection.Add(line8);
            m_ControlCollection.Add(line9);
            m_ControlCollection.Add(line10);
            m_ControlCollection.Add(line11);
            m_ControlCollection.Add(line12);
            m_ControlCollection.Add(line13);
            m_ControlCollection.Add(line14);
            m_ControlCollection.Add(line15);
            m_ControlCollection.Add(line16);

        }



        public void OnDraw(Graphics g)
        {
            m_ControlCollection.ForEach(e => e.OnDraw(g));
        }
    }
}