using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    /// <summary>
    /// 动车组界面
    /// </summary>
    public class TrainGroupView : CommonInnerControlBase
    {
        private readonly List<CommonInnerControlBase> m_ConstControlCollection;

        public Point LeftEdgeLocation { private set; get; }

        public Point RightEdgeLocation { private set; get; }

        public const int DefaultWidth = 95 + 25 + 25;

        public TrainGroupView(Point location)
        {
            const int length = 25;

            var point1 = new Point(location.X + length, location.Y);
            var point2 = new Point(point1.X + 95, point1.Y);
            var point3 = new Point(point1.X - length, point1.Y + length);
            var point4 = new Point(point2.X + length, point2.Y + length);
            var point5 = new Point(point3.X, point3.Y + length);
            var point6 = new Point(point4.X, point4.Y + length);
            m_ConstControlCollection = new List<CommonInnerControlBase>
            {
                new Line(point1, point3),
                new Line(point3, point5),
                new Line(point5, point6),
                new Line(point6, point4),
                new Line(point4, point2),
                new Line(point1, point2)
            };

            LeftEdgeLocation = point3;
            RightEdgeLocation = point4;

            m_OutLineRectangle = new Rectangle(location, new Size(95 + length*2, length*2));
        }

        public override void OnDraw(Graphics g)
        {
            if (!Visible)
            {
                return;
            }

            m_ConstControlCollection.ForEach(e => e.OnDraw(g));
        }
    }
}