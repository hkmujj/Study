using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Model;
using Motor.HMI.CRH3C380B.Base.底层共用;

namespace Motor.HMI.CRH3C380B.Base.系统.WheelShaft
{
    public class WheelShaftCar
    {
        //车子长度
        public int Length { get; set; }
        //是否有头车
        public bool IsTc { get; set; }
        public int Interval { get; set; }
        public int TcLength { get; set; }
        public Point Location { get; set; }
        public Direction Direction { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsCRH380BL { get; set; }
        public WheelShaftCar()
        {

        }

        public List<CommonInnerControlBase> Init()
        {
            var lst = new List<CommonInnerControlBase>();
            var loction = Location;
            if (IsTc)
            {
                switch (Direction)
                {
                    case Direction.Left:
                        var line1 = new Line(new Point(loction.X, loction.Y + TcLength), new Point(loction.X, loction.Y + Height - TcLength));
                        var line2 = new Line(line1.StartPoint, new Point(loction.X + TcLength, loction.Y));
                        var line3 = new Line(line1.EndPoint, new Point(loction.X + TcLength, loction.Y + Height));
                        var line4 = new Line(line2.EndPoint, new Point(line2.EndPoint.X + Width, loction.Y));
                        var line5 = new Line(line3.EndPoint, new Point(line3.EndPoint.X + Width, line3.EndPoint.Y));
                        var line6 = new Line(line4.EndPoint, line5.EndPoint);
                        lst.Add(line1);
                        lst.Add(line2);
                        lst.Add(line3);
                        lst.Add(line4);
                        lst.Add(line5);
                        lst.Add(line6);
                        loction.X = loction.X + Width + Interval + TcLength;
                        for (int i = 0; i < 3; i++)
                        {
                            lst.Add(new GDIRectText
                            {
                                NeedDarwOutline = true,
                                BackColorVisible = false,
                                OutLineRectangle = new Rectangle(loction, new Size(Width, Height)),
                                OutLinePen = new Pen(Color.White, 2f)
                            });
                            loction.X = loction.X + Width + Interval;
                        }
                        break;
                    case Direction.Right:
                        for (int i = 0; i < 3; i++)
                        {
                            lst.Add(new GDIRectText
                            {
                                NeedDarwOutline = true,
                                BackColorVisible = false,
                                OutLineRectangle = new Rectangle(loction, new Size(Width, Height)),
                                OutLinePen = new Pen(Color.White, 2f)
                            });
                            loction.X = loction.X + Width + Interval;
                        }
                        var line7 = new Line(loction, new Point(loction.X, loction.Y + Height));
                        var line8 = new Line(line7.StartPoint, new Point(line7.StartPoint.X + Width, line7.StartPoint.Y));
                        var line9 = new Line(line7.EndPoint, new Point(line7.EndPoint.X + Width, line7.EndPoint.Y));
                        var line10 = new Line(line8.EndPoint, new Point(line8.EndPoint.X + TcLength, line8.EndPoint.Y + TcLength));
                        var line11 = new Line(line9.EndPoint, new Point(line9.EndPoint.X + TcLength, line9.EndPoint.Y - TcLength));
                        var line12 = new Line(line10.EndPoint, line11.EndPoint);
                        lst.Add(line7);
                        lst.Add(line8);
                        lst.Add(line9);
                        lst.Add(line10);
                        lst.Add(line11);
                        lst.Add(line12);
                        break;
                }
            }
            else
            {
                if (Direction == Direction.Left)
                {
                    loction.Offset(TcLength, 0);
                }
                for (int i = 0; i < 4; i++)
                {
                    lst.Add(new GDIRectText
                    {
                        NeedDarwOutline = true,
                        BackColorVisible = false,
                        OutLineRectangle = new Rectangle(loction, new Size(Width, Height)),
                        OutLinePen = new Pen(Color.White, 2f)
                    });
                    loction.X = loction.X + Width + Interval;
                }
            }
            InitNum(lst);
            InitLeftAndRight(lst);
            return lst;
        }

        private void InitNum(ICollection<CommonInnerControlBase> lst)
        {
            var size = new Size(Width / 5, Width / 5);
            var y = Location.Y + (Height - size.Height) / 2;

            var tmpLoction = Location;
            tmpLoction.X = tmpLoction.X + (IsTc && Direction == Direction.Left ? TcLength : 0);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    lst.Add(new GDIRectText
                    {
                        Text = (j + 1).ToString(),
                        NeedDarwOutline = false,
                        BackColorVisible = false,
                        TextFormat = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        },
                        OutLineRectangle = new Rectangle(tmpLoction.X, y, size.Width, size.Height)
                    });
                    tmpLoction.X += (size.Width) + (j == 1 ? Width / 5 : 0);
                }
                tmpLoction.X = (Location.X + (IsTc && Direction == Direction.Left ? TcLength : 0)) + (Width + Interval) * (i + 1);
            }
        }

        private void InitLeftAndRight(ICollection<CommonInnerControlBase> lst)
        {
            var size = new Size(Width / 5 * 2, 40);
            var location = Location;
            location.X = location.X + (IsTc && Direction == Direction.Left ? TcLength : 0);
            var topY = Location.Y + 2;
            var downY = Location.Y + Height - size.Height - 2;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    lst.Add(new GDIRectText
                    {
                        Text = i >= 2 ? "左" : "右",
                        NeedDarwOutline = false,
                        BackColorVisible = false,
                        TextFormat = FontsItems.TheAlignment(FontRelated.靠中上),
                        OutLineRectangle = new Rectangle(location.X, topY, size.Width, size.Height)
                    });
                    lst.Add(new GDIRectText
                    {
                        Text = i >= 2 ? "右" : "左",
                        NeedDarwOutline = false,
                        BackColorVisible = false,
                        TextFormat = FontsItems.TheAlignment(FontRelated.靠中下),
                        OutLineRectangle = new Rectangle(location.X, downY, size.Width, size.Height)
                    });
                    location.X += (size.Width + Width / 5);
                }
                location.X = (Location.X + (IsTc && Direction == Direction.Left ? TcLength : 0)) + (Width + Interval) * (i + 1);
            }

        }
    }
}