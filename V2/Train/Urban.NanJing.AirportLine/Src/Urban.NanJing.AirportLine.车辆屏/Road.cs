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
    public class Road : baseClass
    {
        private TrainHead2 m_TrainHeadLeft;
        private TrainHead2 m_TrainHeadRight;

        private TrainRect3 m_TrainRectLeft1;
        private TrainRect3 m_TrainRectRight1;

        private TrainRect3 m_TrainRectLeft2;
        private TrainRect3 m_TrainRectRight2;

        private TrainTriangle1 m_TrainTriangleLeft;
        private TrainTriangle1 m_TrainTriangleRight;

        private TrainPolygon m_TrainPolygonLeftTop;
        private TrainPolygon m_TrainPolygonLeftDown;
        private TrainPolygon m_TrainPolygonRightTop;
        private TrainPolygon m_TrainPolygonRightDown;

        private Image[] m_ImageArray;

        private List<IRoadInterface> m_RoadList = new List<IRoadInterface>();

        private List<string> m_StringList = new List<string>() { "VCBS", "DOBS", "LMRGBS", "ATCIS", "DDLS", "半车开关", "BBK", "KBS" };

        public override string GetInfo()
        {
            return "旁路";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            m_ImageArray = new Image[UIObj.ParaList.Count];
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }

            for (int i = 0; i < 8; i++)
            {

                if (i == 5)
                {
                    m_RoadList.Add(new RoadRect2(new Point(40, 200 + 40 * i), m_StringList[i], new List<int>() { 965, 966, 967, 968 }));
                }
                else
                {
                    if (i < 5)
                        m_RoadList.Add(new RoadRect1(new Point(40, 200 + 40 * i), m_StringList[i], new List<int>() { 925 + 8 * i, 926 + 8 * i, 927 + 8 * i, 928 + 8 * i, 929 + 8 * i, 930 + 8 * i, 931 + 8 * i, 932 + 8 * i }));
                    else
                        m_RoadList.Add(new RoadRect1(new Point(40, 200 + 40 * i), m_StringList[i], new List<int>() { 969 + 8 * (i - 6), 970 + 8 * (i - 6), 971 + 8 * (i - 6), 972 + 8 * (i - 6), 973 + 8 * (i - 6), 974 + 8 * (i - 6), 975 + 8 * (i - 6), 976 + 8 * (i - 6) }));
                }
            }


            m_TrainHeadLeft = new TrainHead2(new Point(110, 150), new List<int>() { 333, 334, 335 }, 1);
            m_TrainHeadRight = new TrainHead2(new Point(655, 150), new List<int>() { 336, 337, 338 }, -1);

            m_TrainRectLeft1 = new TrainRect3(new Point(105, 160), new List<int>() { 1165, 1166, 1167 });
            m_TrainRectRight1 = new TrainRect3(new Point(650, 160), new List<int>() { 1168, 1169, 1170 });

            m_TrainRectLeft2 = new TrainRect3(new Point(90, 160), new List<int>() { 345, 346, 347 });
            m_TrainRectRight2 = new TrainRect3(new Point(665, 160), new List<int>() { 348, 349, 350 });


            m_TrainTriangleLeft = new TrainTriangle1(new Point(88, 160), new List<int>() { 339, 340, 341 }, 1);
            m_TrainTriangleRight = new TrainTriangle1(new Point(677, 160), new List<int>() { 342, 343, 344 }, -1);

            m_TrainPolygonLeftTop = new TrainPolygon(new Point(110, 142), new List<int>() { 1153, 1154, 1155 }, 1, 1);
            m_TrainPolygonLeftDown = new TrainPolygon(new Point(110, 196), new List<int>() { 1156, 1157, 1158 }, 1, -1);
            m_TrainPolygonRightTop = new TrainPolygon(new Point(655, 142), new List<int>() { 1159, 1160, 1161 }, -1, 1);
            m_TrainPolygonRightDown = new TrainPolygon(new Point(655, 196), new List<int>() { 1162, 1163, 1164 }, -1, -1);


            return true;
        }

        public override void paint(Graphics g)
        {
            #region  画车
            g.DrawImage(m_ImageArray[0], new Point(110, 150));
            g.DrawImage(m_ImageArray[1], new Point(112 + m_ImageArray[0].Width, 150));
            g.DrawImage(m_ImageArray[2], new Point(114 + m_ImageArray[0].Width + m_ImageArray[1].Width, 150));
            g.DrawImage(m_ImageArray[3], new Point(116 + m_ImageArray[0].Width + m_ImageArray[1].Width + m_ImageArray[2].Width, 150));
            g.DrawImage(m_ImageArray[4], new Point(120 + m_ImageArray[0].Width + m_ImageArray[1].Width + m_ImageArray[2].Width + m_ImageArray[3].Width, 150));
            g.DrawImage(m_ImageArray[5], new Point(122 + m_ImageArray[0].Width + m_ImageArray[1].Width + m_ImageArray[2].Width + m_ImageArray[3].Width + m_ImageArray[4].Width, 150));
            #endregion


            m_TrainHeadLeft.OnDraw(g);
            m_TrainHeadRight.OnDraw(g);

            m_TrainRectLeft1.OnDraw(g);
            m_TrainRectRight1.OnDraw(g);


            m_TrainRectLeft2.OnDraw(g);
            m_TrainRectRight2.OnDraw(g);



            m_TrainTriangleLeft.OnDraw(g);
            m_TrainTriangleRight.OnDraw(g);




            m_TrainPolygonLeftTop.OnDraw(g);
            m_TrainPolygonLeftDown.OnDraw(g);
            m_TrainPolygonRightTop.OnDraw(g);
            m_TrainPolygonRightDown.OnDraw(g);

            foreach (var v in m_RoadList)
            {
                v.OnDraw(g);
            }
        }
    }

    public interface IRoadInterface
    {
        void OnDraw(Graphics g);
    }

    public class RoadRect1 : IRoadInterface
    {
        private List<string> m_StrinList = new List<string>() { "未知", "激活", "未激活", "激活" };
        private List<Color> m_ColorList = new List<Color>() { Color.Gray, Color.Yellow, Color.FromArgb(0, 255, 0), Color.Red };

        private Point m_Point;
        private string m_Label;
        private List<int> m_ReceiveCommand;

        private List<RectText> m_RectTextList = new List<RectText>();
        private Rectangle m_Rect;
        public RoadRect1(Point point, string label, List<int> receiveCommand)
        {
            m_Point = point;
            m_Label = label;
            m_ReceiveCommand = receiveCommand;

            m_Rect = new Rectangle(m_Point.X, m_Point.Y, 620, 40);
            m_RectTextList.Add(new RectText(new Rectangle(m_Point.X + 100, m_Point.Y + 2, 140, 36), "未激活", 12, 1, Color.Black, Color.FromArgb(0, 255, 0), Color.Black, 1, true, null, false));
            m_RectTextList.Add(new RectText(new Rectangle(m_Point.X + 460, m_Point.Y + 2, 140, 36), "未激活", 12, 1, Color.Black, Color.FromArgb(0, 255, 0), Color.Black, 1, true, null, false));
        }

        public void OnDraw(Graphics g)
        {
            if (m_ReceiveCommand != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (BasicClass.m_Boolsortedlist[m_ReceiveCommand[i * 4 + j]])
                        {
                            m_RectTextList[i].BackgroundColor = m_ColorList[j];
                            m_RectTextList[i].Text = m_StrinList[j];
                        }
                    }
                }
            }

            g.DrawRectangle(Common.m_BluePen, m_Rect);
            g.DrawString(m_Label, Common.m_12Font, Common.m_BlueBrush, new Point(m_Point.X + 2, m_Point.Y + 4));
            foreach (var v in m_RectTextList)
            {
                v.OnDraw(g);
            }
        }

    }

    public class RoadRect2 : IRoadInterface
    {

        private List<string> m_StrinList = new List<string>() { "6", "3" };
        private List<Color> m_ColorList = new List<Color>() { Color.FromArgb(0, 255, 0), Color.Red };

        private Point m_Point;
        private string m_Label;
        private List<int> m_ReceiveCommand;

        private List<RectText> m_RectTextList = new List<RectText>();
        private Rectangle m_Rect;
        public RoadRect2(Point point, string label, List<int> receiveCommand)
        {
            m_Point = point;
            m_Label = label;
            m_ReceiveCommand = receiveCommand;

            m_Rect = new Rectangle(m_Point.X, m_Point.Y, 620, 40);
            m_RectTextList.Add(new RectText(new Rectangle(m_Point.X + 260, m_Point.Y + 2, 50, 36), "6", 12, 1, Color.Black, Color.FromArgb(0, 255, 0), Color.Black, 1, true, null, true));
            m_RectTextList.Add(new RectText(new Rectangle(m_Point.X + 330, m_Point.Y + 2, 50, 36), "6", 12, 1, Color.Black, Color.FromArgb(0, 255, 0), Color.Black, 1, true, null, true));
        }

        public void OnDraw(Graphics g)
        {

            if (m_ReceiveCommand != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (BasicClass.m_Boolsortedlist[m_ReceiveCommand[i * 2 + j]])
                        {
                            m_RectTextList[i].BackgroundColor = m_ColorList[j];
                            m_RectTextList[i].Text = m_StrinList[j];
                        }
                    }
                }
            }

            g.DrawRectangle(Common.m_BluePen, m_Rect);
            g.DrawString(m_Label, Common.m_12Font, Common.m_BlueBrush, new Point(m_Point.X + 2, m_Point.Y + 4));
            foreach (var v in m_RectTextList)
            {
                v.OnDraw(g);
            }
        }

    }


    public class TrainHead2
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;
        private Point[] m_PointsArray;
        private SolidBrush m_Brush = new SolidBrush(Color.Blue);
        private Color[] m_ColorArray = { Color.Blue, Color.Green, Color.Gray };

        public TrainHead2(Point point, List<int> receiveCommand, int direction)
        {
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_PointsArray = new Point[4] { m_Point, new Point(m_Point.X - 10 * direction, m_Point.Y + 10), new Point(m_Point.X - 10 * direction, m_Point.Y + 30), new Point(m_Point.X, m_Point.Y + 40) };
        }

        public void OnDraw(Graphics g)
        {
            if (m_ReceiveCommand != null)
            {
                for (int i = 0; i < m_ReceiveCommand.Count; i++)
                {
                    if (BasicClass.m_Boolsortedlist[m_ReceiveCommand[i]])
                    {
                        m_Brush.Color = m_ColorArray[i];

                        g.FillPolygon(m_Brush, m_PointsArray);
                        g.DrawLines(Common.m_BlackPen, m_PointsArray);
                    }
                }
            }


        }
    }

    public class TrainRect3
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;

        private Rectangle m_Rect;

        private SolidBrush m_Brush = new SolidBrush(Color.Gray);
        private Color[] m_ColorArray = { Color.Gray, Color.Red, Color.Black };

        public TrainRect3(Point point, List<int> receiveCommand)
        {
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_Rect = new Rectangle(m_Point.X, m_Point.Y, 10, 20);
        }


        public void OnDraw(Graphics g)
        {
            if (m_ReceiveCommand != null)
            {
                for (int i = 0; i < m_ReceiveCommand.Count; i++)
                {
                    if (BasicClass.m_Boolsortedlist[m_ReceiveCommand[i]])
                    {
                        m_Brush.Color = m_ColorArray[i];


                        g.FillRectangle(m_Brush, m_Rect);
                        g.DrawRectangle(Common.m_BlackPen, m_Rect);
                    }
                }
            }


        }
    }


    public class TrainRect4
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;

        private Rectangle m_Rect;

        public TrainRect4(Point point, List<int> receiveCommand)
        {
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_Rect = new Rectangle(m_Point.X, m_Point.Y, 10, 20);
        }


        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common.m_BlackBrush, m_Rect);
        }
    }


    public class TrainTriangle1
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;
        private Point[] m_PointsArray;

        private SolidBrush m_Brush = new SolidBrush(Color.Black);
        private Color[] m_ColorArray = { Color.Black, Color.Black, Color.Gray };
        private int m_Direction;

        public TrainTriangle1(Point point, List<int> receiveCommand, int direction)
        {
            m_Direction = direction;
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_PointsArray = new Point[3] { m_Point, new Point(m_Point.X - 10 * m_Direction, m_Point.Y + 10), new Point(m_Point.X, m_Point.Y + 20) };
        }

        public void OnDraw(Graphics g)
        {

            if (m_ReceiveCommand != null)
            {
                for (int i = 0; i < m_ReceiveCommand.Count; i++)
                {
                    if (BasicClass.m_Boolsortedlist[m_ReceiveCommand[i]])
                    {
                        m_Brush.Color = m_ColorArray[i];
                        if (i == 0 || i == 2)
                        {

                            m_PointsArray = new Point[3] { m_Point, new Point(m_Point.X - 10 * m_Direction, m_Point.Y + 10), new Point(m_Point.X, m_Point.Y + 20) };

                        }
                        else if (i == 1)
                        {
                            // _pointsArray = new Point[3] { _point, new Point(_point.X - 10 * _direction, _point.Y + 10), new Point(_point.X, _point.Y + 20) };

                            //_pointsArray = new Point[3] { new Point(_point.X - 10 * _direction, _point.Y), new Point(_point.X, _point.Y + 15), new Point(_point.X - 10 * _direction, _point.Y + 30) };

                            m_PointsArray = new Point[3] { new Point(m_Point.X, m_Point.Y + 10), new Point(m_Point.X - 10 * m_Direction, m_Point.Y), new Point(m_Point.X - 10 * m_Direction, m_Point.Y + 20) };
                        }

                        g.FillPolygon(m_Brush, m_PointsArray);
                        g.DrawLines(Common.m_BlackPen, m_PointsArray);
                    }
                }
            }

        }
    }
}
