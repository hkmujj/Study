using System.Drawing;

namespace Engine.TCMS.HXD3C.Utils
{
    public class NeedChangeLength
    {
        /// <summary>
        /// 当前要画的高度值
        /// </summary>
        private float m_ViewValue = 0;

        /// <summary>
        /// 需要增加的高度量
        /// </summary>
        private float m_TmpNeedChangeLength = 0;

        /// <summary>
        /// 递增量
        /// </summary>
        private readonly float m_TmpStepLength = 5;

        /// <summary>
        /// 黄色画笔
        /// </summary>
        public SolidBrush m_BarBrush;

        /// <summary>
        /// 绘图起点
        /// </summary>
        PointF m_DrawPoint;

        /// <summary>
        /// 进度条宽度
        /// </summary>
        private readonly int m_RectWidth;

        /// <summary>
        /// 对应比例
        /// </summary>
        private readonly float m_RectScale;

        public NeedChangeLength(PointF point, int width, float dizeng, float scale, SolidBrush bar)
        {
            m_DrawPoint = point;
            m_RectWidth = width;
            m_TmpStepLength = dizeng;
            m_RectScale = scale;
            m_BarBrush = bar;
        }

        /// <summary>
        /// 绘制纵条1
        /// </summary>
        /// <param name="e"></param>
        /// <param name="currentValue"></param>
        /// <param name="drawType"></param>
        public void DrawRectangle(Graphics e, ref float currentValue, int drawType)
        {
            if (m_ViewValue > currentValue)
            {
                if (m_ViewValue - m_TmpStepLength >= currentValue)
                {
                    m_TmpNeedChangeLength = -m_TmpStepLength;
                }
                else
                {
                    m_TmpNeedChangeLength = currentValue - m_ViewValue;
                }
            }
            else if (m_ViewValue < currentValue)
            {
                if (m_ViewValue + m_TmpStepLength <= currentValue)
                {
                    m_TmpNeedChangeLength = m_TmpStepLength;
                }
                else
                {
                    m_TmpNeedChangeLength = currentValue - m_ViewValue;
                }
            }
            else
            {
                m_TmpNeedChangeLength = 0;
            }
            m_ViewValue += m_TmpNeedChangeLength;

            switch (drawType)
            {
                case 1://横左
                    e.FillRectangle(m_BarBrush, new RectangleF(new PointF(m_DrawPoint.X - m_ViewValue * m_RectScale, m_DrawPoint.Y), new SizeF(m_ViewValue * m_RectScale, m_RectWidth)));
                    break;
                case 2://横右
                    e.FillRectangle(m_BarBrush, new RectangleF(m_DrawPoint, new SizeF(m_ViewValue * m_RectScale, m_RectWidth)));
                    break;
                case 3://纵上
                    e.FillRectangle(m_BarBrush, new RectangleF(new PointF(m_DrawPoint.X, m_DrawPoint.Y - m_ViewValue * m_RectScale), new SizeF(m_RectWidth, m_ViewValue * m_RectScale)));
                    break;
                case 4://纵下
                    e.FillRectangle(m_BarBrush, new RectangleF(m_DrawPoint, new SizeF(m_RectWidth, m_ViewValue * m_RectScale)));
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 绘制纵条2
        /// </summary>
        /// <param name="e"></param>
        /// <param name="currentValue"></param>
        /// <param name="drawType"></param>
        public void DrawRectangle(Graphics e, ref float currentValue, int drawType, SolidBrush bar)
        {
            if (m_ViewValue > currentValue)
            {
                if (m_ViewValue - m_TmpStepLength >= currentValue)
                {
                    m_TmpNeedChangeLength = -m_TmpStepLength;
                }
                else
                {
                    m_TmpNeedChangeLength = currentValue - m_ViewValue;
                }
            }
            else if (m_ViewValue < currentValue)
            {
                if (m_ViewValue + m_TmpStepLength <= currentValue)
                {
                    m_TmpNeedChangeLength = m_TmpStepLength;
                }
                else
                {
                    m_TmpNeedChangeLength = currentValue - m_ViewValue;
                }
            }
            else
            {
                m_TmpNeedChangeLength = 0;
            }
            m_ViewValue += m_TmpNeedChangeLength;

            switch (drawType)
            {
                case 1://横左
                    e.FillRectangle(bar, new RectangleF(new PointF(m_DrawPoint.X - m_ViewValue * m_RectScale, m_DrawPoint.Y), new SizeF(m_ViewValue * m_RectScale, m_RectWidth)));
                    break;
                case 2://横右
                    e.FillRectangle(bar, new RectangleF(m_DrawPoint, new SizeF(m_ViewValue * m_RectScale, m_RectWidth)));
                    break;
                case 3://纵上
                    e.FillRectangle(bar, new RectangleF(new PointF(m_DrawPoint.X, m_DrawPoint.Y - m_ViewValue * m_RectScale), new SizeF(m_RectWidth, m_ViewValue * m_RectScale)));
                    break;
                case 4://纵下
                    e.FillRectangle(bar, new RectangleF(m_DrawPoint, new SizeF(m_RectWidth, m_ViewValue * m_RectScale)));
                    break;
                default:
                    break;
            }

        }
    }
}