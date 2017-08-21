using System;
using System.Drawing;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    /// <summary>
    /// �仯������
    /// </summary>
    public class NeedChangeLength
    {
        /// <summary>
        /// ��ǰҪ���ĸ߶�ֵ
        /// </summary>
        private float m_ViewValue = 0;

        /// <summary>
        /// ��Ҫ���ӵĸ߶���
        /// </summary>
        private float m_TmpNeedChangeLength = 0;

        /// <summary>
        /// ������
        /// </summary>
        private float m_TmpStepLength = 5;

        /// <summary>
        /// ��ɫ����
        /// </summary>
        public static SolidBrush m_YellowBrush = new SolidBrush(Color.FromArgb(0, 204, 255));

        /// <summary>
        /// ��ͼ���
        /// </summary>
        PointF m_DrawPoint;

        /// <summary>
        /// ���������
        /// </summary>
        private int m_RectWidth;

        /// <summary>
        /// ��Ӧ����
        /// </summary>
        private float m_RectScale;

        private float[] m_Scales;
        private float[] m_Values;

        private Int32 m_Index = 0;
        private PointF m_CurrentPoint;
        private float m_CurrentHigh;
        private Pen m_P;

        public NeedChangeLength(PointF point, int width, float dizeng, float scale)
        {
            m_DrawPoint = point;
            m_RectWidth = width;
            m_TmpStepLength = dizeng;
            m_RectScale = scale;
        }

        public NeedChangeLength(PointF point, int width, float dizeng, float[] scale, float[] values)
        {
            m_DrawPoint = point;
            m_CurrentPoint = point;
            //currentHigh = point.Y;
            m_RectWidth = width;
            m_TmpStepLength = dizeng;
            m_Scales = scale;
            m_Values = values;

            m_P = new Pen(new SolidBrush(Color.FromArgb(255, 0, 255)), 5);
        }

        public float m_CurrentValue = 0;
        public void Draw(Graphics e, ref float currentValue)
        {
            if (m_ViewValue < currentValue)
            {
                if (m_ViewValue + m_Scales[m_Index] <= currentValue)
                {
                    m_ViewValue += m_Scales[m_Index];
                    //ViewValue = this.values[index+1];
                    m_Index++;
                    m_CurrentHigh += m_TmpStepLength;
                    e.FillRectangle(m_YellowBrush, new RectangleF(new PointF(m_DrawPoint.X, m_DrawPoint.Y - m_CurrentHigh), new SizeF(m_RectWidth, m_CurrentHigh)));
                }
                else
                {
                    m_TmpNeedChangeLength = (currentValue - m_ViewValue) * (m_TmpStepLength / m_Scales[m_Index]);
                    m_ViewValue = currentValue;
                }
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="e"></param>
        /// <param name="currentValue"></param>
        /// <param name="drawType"></param>
        public void DrawRectangle(Graphics e, ref float currentValue, int drawType)
        {
            if (currentValue > 1000)
                currentValue = 1000;

            if (currentValue < 0)
                return;

            while (m_ViewValue != currentValue)
            {
                if (m_ViewValue > currentValue)
                {
                    if (currentValue >= m_Values[m_Index])
                    {
                        m_TmpNeedChangeLength = -(m_ViewValue - currentValue) * (m_TmpStepLength / m_Scales[m_Index]);
                        m_ViewValue = currentValue;
                    }
                    else
                    {
                        m_TmpNeedChangeLength = -(m_ViewValue - m_Values[m_Index]) * (m_TmpStepLength / m_Scales[m_Index]);
                        m_ViewValue = m_Values[m_Index];
                        if (m_Index != 0)
                            m_Index--;
                    }
                }
                else if (m_ViewValue < currentValue)//����
                {
                    if (m_Index == m_Values.Length - 1) break;
                    if (currentValue < m_Values[m_Index + 1])
                    {
                        m_TmpNeedChangeLength = (currentValue - m_ViewValue) * (m_TmpStepLength / m_Scales[m_Index]);
                        m_ViewValue = currentValue;
                    }
                    else
                    {
                        m_TmpNeedChangeLength = (m_Values[m_Index + 1] - m_ViewValue) * (m_TmpStepLength / m_Scales[m_Index]); ;
                        m_ViewValue = m_Values[m_Index + 1];
                        if (m_Index != 9)
                            m_Index++;
                    }
                }
                else
                {
                    m_TmpNeedChangeLength = 0;
                }
                m_CurrentHigh += m_TmpNeedChangeLength;
            }

            switch (drawType)
            {
                case 1://����
                    e.FillRectangle(m_YellowBrush, new RectangleF(new PointF(m_DrawPoint.X - m_ViewValue * m_RectScale, m_DrawPoint.Y), new SizeF(m_ViewValue * m_RectScale, m_RectWidth)));
                    break;
                case 2://����
                    e.FillRectangle(m_YellowBrush, new RectangleF(m_DrawPoint, new SizeF(m_ViewValue * m_RectScale, m_RectWidth)));
                    break;
                case 3://����
                    e.FillRectangle(m_YellowBrush, new RectangleF(new PointF(m_DrawPoint.X, m_DrawPoint.Y - m_CurrentHigh), new SizeF(m_RectWidth, m_CurrentHigh)));
                    e.DrawLine(
                        m_P,
                        new PointF(m_DrawPoint.X - 6, m_DrawPoint.Y - m_CurrentHigh - 2),
                        new PointF(m_DrawPoint.X - 4 + 50, m_DrawPoint.Y - m_CurrentHigh - 2)
                        );
                    break;
                case 4://����
                    e.FillRectangle(m_YellowBrush, new RectangleF(m_DrawPoint, new SizeF(m_RectWidth, m_ViewValue * m_RectScale)));
                    break;
                default:
                    break;
            }
        }
    }
}