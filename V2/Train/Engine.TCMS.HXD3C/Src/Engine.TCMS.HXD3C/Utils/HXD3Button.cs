using System.Drawing;

namespace Engine.TCMS.HXD3C.Utils
{
    public class HXD3Button
    {
        /// <summary>
        /// 外矩形
        /// </summary>
        readonly Rectangle m_RectOut;
        public Rectangle OutRectangle { get { return m_RectOut; } }
        /// <summary>
        /// 外矩形框的所有点
        /// </summary>
        readonly Point[] m_AllPointsOut = new Point[4];

        /// <summary>
        /// 内矩形
        /// </summary>
        readonly Rectangle m_RectIn;

        /// <summary>
        /// 内矩形框的所有点
        /// </summary>
        readonly Point[] m_AllPointsIn = new Point[4];

        /// <summary>
        /// 矩形左上角需要填充的坐标点集合
        /// </summary>
        readonly Point[] m_UpPoints = new Point[6];

        /// <summary>
        /// 多边形左上角需要填充的坐标点合集
        /// </summary>
        readonly Point[] m_UpPointsP = new Point[6];

        /// <summary>
        /// 矩形右下角需要填充的坐标点集合
        /// </summary>
        readonly Point[] m_DownPoints = new Point[6];

        /// <summary>
        /// 多边形右下角需要填充的坐标点合集
        /// </summary>
        readonly Point[] m_DownPointsP = new Point[8];

        /// <summary>
        /// 外多边形的点
        /// </summary>
        readonly Point[] m_PointsOut;

        /// <summary>
        /// 内多边形的点
        /// </summary>
        readonly Point[] m_PointsIn;

        /// <summary>
        /// 是否有按下和弹起的状态
        /// </summary>
        bool m_UpAndDown;

        #region :::::::::::::::::::::::::::::::::::: 矩形按钮 ::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 矩形框
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="fillColor"></param>
        /// <param name="upAndDown"></param>
        public HXD3Button(Rectangle rectOut, Rectangle rectIn)
        {
            m_RectOut = rectOut;
            m_RectIn = rectIn;

            m_UpPoints[0] = new Point(rectOut.X, rectOut.Y);
            m_UpPoints[1] = new Point(rectOut.X + rectOut.Width, rectOut.Y);
            m_UpPoints[2] = new Point(rectIn.X + rectIn.Width, rectIn.Y);
            m_UpPoints[3] = new Point(rectIn.X, rectIn.Y);
            m_UpPoints[4] = new Point(rectIn.X, rectIn.Y + rectIn.Height);
            m_UpPoints[5] = new Point(rectOut.X, rectOut.Y + rectOut.Height);

            m_DownPoints[0] = new Point(rectOut.X, rectOut.Y + rectOut.Height);
            m_DownPoints[1] = new Point(rectOut.X + rectOut.Width, rectOut.Y + rectOut.Height);
            m_DownPoints[2] = new Point(rectOut.X + rectOut.Width, rectOut.Y);
            m_DownPoints[3] = new Point(rectIn.X + rectIn.Width, rectIn.Y);
            m_DownPoints[4] = new Point(rectIn.X + rectIn.Width, rectIn.Y + rectIn.Height);
            m_DownPoints[5] = new Point(rectIn.X, rectIn.Y + rectIn.Height);

            m_AllPointsOut[0] = new Point(rectOut.X, rectOut.Y);
            m_AllPointsOut[1] = new Point(rectOut.X + rectOut.Width, rectOut.Y);
            m_AllPointsOut[2] = new Point(rectOut.X, rectOut.Y + rectOut.Height);
            m_AllPointsOut[3] = new Point(rectOut.X + rectOut.Width, rectOut.Y + rectOut.Height);

            m_AllPointsIn[0] = new Point(rectIn.X, rectIn.Y);
            m_AllPointsIn[1] = new Point(rectIn.X + rectIn.Width, rectIn.Y);
            m_AllPointsIn[2] = new Point(rectIn.X, rectIn.Y + rectIn.Height);
            m_AllPointsIn[3] = new Point(rectIn.X + rectIn.Width, rectIn.Y + rectIn.Height);
        }

        /// <summary>
        /// 矩形按钮
        /// 填充颜色
        /// 且有按键状态变化
        /// </summary>
        public void DrawRectButtonFillAndState()
        {
            //暂时不做
        }

        /// <summary>
        /// 矩形按钮
        /// 填充颜色
        /// 且没有按键状态变化
        /// </summary>
        public void DrawRectButoonFillAndNoState(Graphics g)
        {
            g.FillPolygon(Common.WhiteBrush, m_UpPoints);
            g.FillPolygon(Common.BlueBrush, m_DownPoints);
            g.FillRectangle(Common.BlueBrush, m_RectIn);
            g.DrawRectangle(Common.WhitePen1, m_RectOut);
            g.DrawRectangle(Common.WhitePen1, m_RectIn);
            g.DrawLine(Common.WhitePen1, m_UpPoints[1], m_UpPoints[2]);
            g.DrawLine(Common.WhitePen1, m_DownPoints[0], m_DownPoints[1]);
        }

        /// <summary>
        /// 矩形按钮
        /// 没有填充颜色
        /// 且没有按键状态变化
        /// </summary>
        public void DrawRectButtonNoFillAndNoState(Graphics g)
        {
            g.DrawRectangle(Common.WhitePen1, m_RectOut);
            g.DrawRectangle(Common.WhitePen1, m_RectIn);

            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(Common.WhitePen1, m_AllPointsOut[i], m_AllPointsIn[i]);
            }
        }
        #endregion

        #region :::::::::::::::::::::::::::::::::::: 多边形按钮 :::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 多边形(五边形)
        /// </summary>
        /// <param name="points"></param>
        /// <param name="fillColor"></param>
        /// <param name="upAndDown"></param>
        public HXD3Button(Point[] pointsOut, Point[] pointsIn)
        {
            m_PointsOut = pointsOut;
            m_PointsIn = pointsIn;

            if (pointsOut.Length == 5 && pointsIn.Length == 5)
            {
                m_UpPointsP[0] = pointsOut[0];
                m_UpPointsP[1] = pointsOut[1];
                m_UpPointsP[2] = pointsOut[2];
                m_UpPointsP[3] = pointsIn[2];
                m_UpPointsP[4] = pointsIn[1];
                m_UpPointsP[5] = pointsIn[0];

                m_DownPointsP[0] = pointsOut[0];
                m_DownPointsP[1] = pointsOut[4];
                m_DownPointsP[2] = pointsOut[3];
                m_DownPointsP[3] = pointsOut[2];
                m_DownPointsP[4] = pointsIn[2];
                m_DownPointsP[5] = pointsIn[3];
                m_DownPointsP[6] = pointsIn[4];
                m_DownPointsP[7] = pointsIn[0];
            }

        }


        /// <summary>
        /// 多边形按钮
        /// 填充颜色
        /// 且有按键状态变化
        /// </summary>
        public void DrawPolygonButtonFillAndState()
        {
        }

        /// <summary>
        /// 多边形按钮
        /// 填充颜色
        /// 且没有按键状态变化
        /// </summary>
        public void DrawPolygonButoonFillAndNoState(Graphics g)
        {
            g.FillPolygon(Common.WhiteBrush, m_UpPointsP);
            g.FillPolygon(Common.BlueBrush, m_DownPointsP);
            g.FillPolygon(Common.BlueBrush, m_PointsIn);
            g.DrawLines(Common.WhitePen1, m_PointsOut);
            g.DrawLines(Common.WhitePen1, m_PointsIn);
            g.DrawLine(Common.WhitePen1, m_PointsOut[0], m_PointsIn[0]);
            g.DrawLine(Common.WhitePen1, m_PointsOut[2], m_PointsIn[2]);
            g.DrawLine(Common.WhitePen1, m_PointsIn[0], m_PointsIn[4]);
            g.DrawLine(Common.WhitePen1, m_PointsOut[3], m_PointsIn[3]);
            g.DrawLine(Common.WhitePen1, m_PointsOut[4], m_PointsIn[4]);
        }

        /// <summary>
        /// 多边形按钮
        /// 没有填充颜色
        /// 且没有按键状态变化
        /// </summary>
        public void DrawPolygonButtonNoFillAndNoState()
        {

        }
        #endregion

    }
}