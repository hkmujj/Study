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
    public class TrainEquipment
    {
        private List<TrainCab> m_TrainCabs;
        private TrainHead m_TrainHeadLeft;
        private TrainHead m_TrainHeadRight;
        private TrainRect1 m_TrainRectLeft1;
        private TrainRect1 m_TrainRectRight1;

        private TrainRect1 m_TrainRectLeft2;
        private TrainRect1 m_TrainRectRight2;

        private TrainTriangle m_TrainTriangleLeft;
        private TrainTriangle m_TrainTriangleRight;

        private TrainDoor m_TrainLeftUp;
        private TrainDoor m_TrainLeftDown;
        private TrainDoor m_TrainRightUp;
        private TrainDoor m_TrainRightDown;


        private TrainPolygon m_TrainPolygonLeftTop;
        private TrainPolygon m_TrainPolygonLeftDown;
        private TrainPolygon m_TrainPolygonRightTop;
        private TrainPolygon m_TrainPolygonRightDown;

        public TrainEquipment()
        {
            m_TrainCabs = new List<TrainCab>();
            int i = 0;
            m_TrainCabs.Add(new TrainCab("1", new Point(70 + 95 * i++, 160), new List<int>() { 772, 773 }));
            m_TrainCabs.Add(new TrainCab("2", new Point(70 + 95 * i++, 160), new List<int>() { 774, 775 }));
            m_TrainCabs.Add(new TrainCab("3", new Point(70 + 95 * i++, 160), new List<int>() { 776, 777 }));
            m_TrainCabs.Add(new TrainCab("4", new Point(70 + 95 * i++, 160), new List<int>() { 778, 779 }));
            m_TrainCabs.Add(new TrainCab("5", new Point(70 + 95 * i++, 160), new List<int>() { 780, 781 }));
            m_TrainCabs.Add(new TrainCab("6", new Point(70 + 95 * i++, 160), new List<int>() { 782, 783 }));


            m_TrainHeadLeft = new TrainHead(new Point(70, 160), new List<int>() { 333, 334, 335 }, 1);
            m_TrainHeadRight = new TrainHead(new Point(620, 160), new List<int>() { 336, 337, 338 }, -1);

            m_TrainRectLeft1 = new TrainRect1(new Point(63, 170), new List<int>() { 1165, 1166, 1167 });
            m_TrainRectRight1 = new TrainRect1(new Point(612, 170), new List<int>() { 1168, 1169, 1170 });

            m_TrainRectLeft2 = new TrainRect1(new Point(40, 170), new List<int>() { 345, 346, 347 });
            m_TrainRectRight2 = new TrainRect1(new Point(635, 170), new List<int>() { 348, 349, 350 });

            m_TrainTriangleLeft = new TrainTriangle(new Point(35, 170), new List<int>() { 339, 340, 341 }, 1);
            m_TrainTriangleRight = new TrainTriangle(new Point(655, 170), new List<int>() { 342, 343, 344 }, -1);


            m_TrainLeftUp = new TrainDoor(new Point(70, 148), null, 1, 1);
            m_TrainLeftDown = new TrainDoor(new Point(70, 222), null, 1, -1);
            m_TrainRightUp = new TrainDoor(new Point(620, 148), null, -1, 1);
            m_TrainRightDown = new TrainDoor(new Point(620, 222), null, -1, -1);

            m_TrainPolygonLeftTop = new TrainPolygon(new Point(), new List<int>() { 1, 2, 3 }, 1, 1);
        }

        public void OnDraw(Graphics g)
        {
            foreach (var v in m_TrainCabs)
            {
                v.OnDraw(g);
            }
            m_TrainHeadLeft.OnDraw(g);
            m_TrainHeadRight.OnDraw(g);

            m_TrainRectLeft1.OnDraw(g);
            m_TrainRectRight1.OnDraw(g);

            m_TrainRectLeft2.OnDraw(g);
            m_TrainRectRight2.OnDraw(g);

            m_TrainTriangleLeft.OnDraw(g);
            m_TrainTriangleRight.OnDraw(g);


            m_TrainLeftUp.OnDraw(g);
            m_TrainLeftDown.OnDraw(g);
            m_TrainRightUp.OnDraw(g);
            m_TrainRightDown.OnDraw(g);


            m_TrainPolygonLeftTop = new TrainPolygon(new Point(), new List<int>() { 1, 2, 3 }, 1, 1);
            m_TrainPolygonLeftTop.OnDraw(g);
        }
    }

    public class TrainCab
    {
        private string m_Label;
        private Point m_Point;
        private List<int> m_ReceiveCommand;

        private RectText m_Cab;
        public TrainCab(string label, Point point, List<int> receiveCommand)
        {
            m_Label = label;
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_Cab = new RectText(new Rectangle(m_Point.X, m_Point.Y, 75, 50), m_Label, 14, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true);
        }

        private Color[] m_ColorArray = { Common.m_BackGroundColor, Color.Yellow };

        public void OnDraw(Graphics g)
        {
            if (m_ReceiveCommand != null)
            {
                for (int i = 0; i < m_ReceiveCommand.Count; i++)
                {
                    if (BasicClass.m_Boolsortedlist[m_ReceiveCommand[i]])
                    {
                        m_Cab.BackgroundColor = m_ColorArray[i];
                    }
                }
            }
            m_Cab.OnDraw(g);
        }
    }

    public class TrainDoor
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;
        private Point[] m_PointsArray;


        public TrainDoor(Point point, List<int> receiveCommand, int leftDirection, int upDirection)
        {
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_PointsArray = new Point[4] { m_Point, new Point(m_Point.X - 25 * leftDirection, m_Point.Y + 10 * upDirection), new Point(m_Point.X - 25 * leftDirection, m_Point.Y + 20 * upDirection), new Point(m_Point.X, m_Point.Y + 10 * upDirection) };
        }

        public void OnDraw(Graphics g)
        {
            g.FillPolygon(Common.m_BlackBrush, m_PointsArray);

        }
    }



    public class TrainRect1
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;

        private Rectangle m_Rect;

        private SolidBrush m_Brush = new SolidBrush(Color.Gray);
        private Color[] m_ColorArray = { Color.Gray, Color.Red, Color.Black };

        public TrainRect1(Point point, List<int> receiveCommand)
        {
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_Rect = new Rectangle(m_Point.X, m_Point.Y, 10, 26);
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

    public class TrainRect2
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;

        private Rectangle m_Rect;

        public TrainRect2(Point point, List<int> receiveCommand)
        {
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_Rect = new Rectangle(m_Point.X, m_Point.Y, 15, 30);
        }


        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common.m_BlackBrush, m_Rect);
        }
    }

    public class TrainTriangle
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;
        private Point[] m_PointsArray;

        private SolidBrush m_Brush = new SolidBrush(Color.Black);
        private Color[] m_ColorArray = { Color.Black, Color.Black, Color.Gray };
        private int m_Direction;

        public TrainTriangle(Point point, List<int> receiveCommand, int direction)
        {
            m_Direction = direction;
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_PointsArray = new Point[3] { m_Point, new Point(m_Point.X - 10 * m_Direction, m_Point.Y + 15), new Point(m_Point.X, m_Point.Y + 30) };
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
                            m_PointsArray = new Point[3] { m_Point, new Point(m_Point.X - 10 * m_Direction, m_Point.Y + 15), new Point(m_Point.X, m_Point.Y + 30) };
                        }
                        else if (i == 1)
                        {
                            m_PointsArray = new Point[3] { new Point(m_Point.X - 10 * m_Direction, m_Point.Y), new Point(m_Point.X, m_Point.Y + 15), new Point(m_Point.X - 10 * m_Direction, m_Point.Y + 30) };
                        }

                        g.FillPolygon(m_Brush, m_PointsArray);
                        g.DrawLines(Common.m_BlackPen, m_PointsArray);
                    }
                }
            }

        }

    }


    public class TrainPolygon
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;
        private Point[] m_PointsArray;

        private SolidBrush m_Brush = new SolidBrush(Color.Black);
        private Color[] m_ColorArray = { Color.Black, Color.Red, Color.Gray };
        private int m_Direction;

        public TrainPolygon(Point point, List<int> receiveCommand, int directionLeft, int directionDown)
        {
            m_Direction = directionLeft;
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_PointsArray = new Point[4] { m_Point, new Point(m_Point.X - 12 * m_Direction, m_Point.Y + 6 * directionDown), new Point(m_Point.X - 12 * m_Direction, m_Point.Y + 12 * directionDown), new Point(m_Point.X, m_Point.Y + 6 * directionDown) };
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
                        //if (i == 0 || i == 2)
                        //{
                        //    _pointsArray = new Point[3] { _point, new Point(_point.X - 10 * _direction, _point.Y + 15), new Point(_point.X, _point.Y + 30) };
                        //}
                        //else if (i == 1)
                        //{
                        //    _pointsArray = new Point[3] { new Point(_point.X - 10 * _direction, _point.Y), new Point(_point.X, _point.Y + 15), new Point(_point.X - 10 * _direction, _point.Y + 30) };
                        //}
                        g.FillPolygon(m_Brush, m_PointsArray);
                        g.DrawLines(Common.m_BlackPen, m_PointsArray);
                    }
                }
            }

        }
    }

    public class TrainHead
    {
        private Point m_Point;
        private List<int> m_ReceiveCommand;
        private Point[] m_PointsArray;
        private SolidBrush m_Brush = new SolidBrush(Color.Blue);
        private Color[] m_ColorArray = { Color.Blue, Color.Green, Color.Gray };

        public TrainHead(Point point, List<int> receiveCommand, int direction)
        {
            m_Point = point;
            m_ReceiveCommand = receiveCommand;
            m_PointsArray = new Point[4] { m_Point, new Point(m_Point.X - 20 * direction, m_Point.Y + 10), new Point(m_Point.X - 20 * direction, m_Point.Y + 40), new Point(m_Point.X, m_Point.Y + 50) };
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
}
