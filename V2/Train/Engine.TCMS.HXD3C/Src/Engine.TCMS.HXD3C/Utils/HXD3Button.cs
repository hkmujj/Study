using System.Drawing;

namespace Engine.TCMS.HXD3C.Utils
{
    public class HXD3Button
    {
        /// <summary>
        /// �����
        /// </summary>
        readonly Rectangle m_RectOut;
        public Rectangle OutRectangle { get { return m_RectOut; } }
        /// <summary>
        /// ����ο�����е�
        /// </summary>
        readonly Point[] m_AllPointsOut = new Point[4];

        /// <summary>
        /// �ھ���
        /// </summary>
        readonly Rectangle m_RectIn;

        /// <summary>
        /// �ھ��ο�����е�
        /// </summary>
        readonly Point[] m_AllPointsIn = new Point[4];

        /// <summary>
        /// �������Ͻ���Ҫ��������㼯��
        /// </summary>
        readonly Point[] m_UpPoints = new Point[6];

        /// <summary>
        /// ��������Ͻ���Ҫ���������ϼ�
        /// </summary>
        readonly Point[] m_UpPointsP = new Point[6];

        /// <summary>
        /// �������½���Ҫ��������㼯��
        /// </summary>
        readonly Point[] m_DownPoints = new Point[6];

        /// <summary>
        /// ��������½���Ҫ���������ϼ�
        /// </summary>
        readonly Point[] m_DownPointsP = new Point[8];

        /// <summary>
        /// �����εĵ�
        /// </summary>
        readonly Point[] m_PointsOut;

        /// <summary>
        /// �ڶ���εĵ�
        /// </summary>
        readonly Point[] m_PointsIn;

        /// <summary>
        /// �Ƿ��а��º͵����״̬
        /// </summary>
        bool m_UpAndDown;

        #region :::::::::::::::::::::::::::::::::::: ���ΰ�ť ::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// ���ο�
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
        /// ���ΰ�ť
        /// �����ɫ
        /// ���а���״̬�仯
        /// </summary>
        public void DrawRectButtonFillAndState()
        {
            //��ʱ����
        }

        /// <summary>
        /// ���ΰ�ť
        /// �����ɫ
        /// ��û�а���״̬�仯
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
        /// ���ΰ�ť
        /// û�������ɫ
        /// ��û�а���״̬�仯
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

        #region :::::::::::::::::::::::::::::::::::: ����ΰ�ť :::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// �����(�����)
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
        /// ����ΰ�ť
        /// �����ɫ
        /// ���а���״̬�仯
        /// </summary>
        public void DrawPolygonButtonFillAndState()
        {
        }

        /// <summary>
        /// ����ΰ�ť
        /// �����ɫ
        /// ��û�а���״̬�仯
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
        /// ����ΰ�ť
        /// û�������ɫ
        /// ��û�а���״̬�仯
        /// </summary>
        public void DrawPolygonButtonNoFillAndNoState()
        {

        }
        #endregion

    }
}