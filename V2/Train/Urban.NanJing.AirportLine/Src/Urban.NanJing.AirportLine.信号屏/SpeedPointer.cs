using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    /// <summary>
    /// 速度指针
    /// </summary>
    public class SpeedPointer
    {
        /// <summary>
        /// 需要画的最大角度
        /// </summary>
        private float m_DialAnglev;

        /// <summary>
        /// 指针初始角度
        /// </summary>
        private float m_InitalAnglev;

        /// <summary>
        /// 指针最大值
        /// </summary>
        private float m_MaxSpeed;

        /// <summary>
        /// 绘图顶点
        /// </summary>
        private PointF m_TopPoint;

        /// <summary>
        /// 绘图中心点
        /// </summary>
        private PointF m_CentralPoint;

        private Matrix m_Matrix;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float m_Anglev = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maxAnglev"></param>
        /// <param name="initAnglev"></param>
        /// <param name="maxValue"></param>
        /// <param name="apexPoint"></param>
        /// <param name="centrePoint"></param>
        public SpeedPointer(float maxAnglev, float initAnglev, float maxValue, PointF apexPoint, PointF centrePoint)
        {
            m_DialAnglev = maxAnglev;
            m_InitalAnglev = initAnglev;
            m_MaxSpeed = maxValue;
            m_TopPoint = apexPoint;
            m_CentralPoint = centrePoint;
        }

        /// <summary>
        /// 绘指针
        /// </summary>
        /// <param name="g"></param>
        /// <param name="speed"></param>
        public void PaintPointer(Graphics g, Image tmpPic, ref float speed)
        {
            try
            {
                if (speed <= m_MaxSpeed)
                {
                    m_Anglev = speed*m_DialAnglev/m_MaxSpeed + m_InitalAnglev;
                }
                m_Matrix = g.Transform;
                m_Matrix.RotateAt(m_Anglev, m_CentralPoint);
                g.Transform = m_Matrix;
                g.DrawImage(tmpPic, m_TopPoint);
                m_Matrix.Reset();
                g.Transform = m_Matrix;
            }
            catch (Exception e)
            {
            }
        }
    }
}