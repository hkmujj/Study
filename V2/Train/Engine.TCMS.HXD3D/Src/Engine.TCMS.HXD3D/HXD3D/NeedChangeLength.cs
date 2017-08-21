using System.Drawing;
using Engine.TCMS.HXD3D.底层共用;

namespace Engine.TCMS.HXD3D.HXD3D
{
    public class NeedChangeLength
    {
        // Fields
        private SolidBrush m_TheBrush;

        private readonly float m_TheMaxValue;
        private RectangleF m_TheRectangleF;
        private readonly RectRiseDirection m_TheRectDirection;
        private float m_TheScale;
        private float m_TmpStrpLength = 5f;
        private readonly bool m_IsNeedIncrease;
        private float m_TmpNeedChangeLength = 0f;
        private float m_ViewValue = 0f;

        // Methods
        public NeedChangeLength(RectangleF rect, float maxValue, RectRiseDirection theDirection, bool needIncrease)
        {
            m_TheRectangleF = rect;
            m_TheMaxValue = maxValue;
            m_TheRectDirection = theDirection;
            m_IsNeedIncrease = needIncrease;
        }

        public void DrawRectangle(Graphics gs, ref float currentValue, SolidBrush brush)
        {
            float num;
            m_TheBrush = brush;
            switch (m_TheRectDirection)
            {
                case RectRiseDirection.上:
                    m_TheScale = m_TheRectangleF.Height / m_TheMaxValue;
                    num = m_IsNeedIncrease ? ReturnTheVariation(ref currentValue) * m_TheScale : currentValue * m_TheScale;
                    gs.FillRectangle(m_TheBrush, new RectangleF(m_TheRectangleF.X, m_TheRectangleF.Y + m_TheRectangleF.Height - num, m_TheRectangleF.Width, num));
                    break;

                case RectRiseDirection.下:
                    m_TheScale = m_TheRectangleF.Height / m_TheMaxValue;
                    num = m_IsNeedIncrease ? ReturnTheVariation(ref currentValue) * m_TheScale : currentValue * m_TheScale;
                    gs.FillRectangle(m_TheBrush, new RectangleF(m_TheRectangleF.X, m_TheRectangleF.Y, m_TheRectangleF.Width, num));
                    break;

                case RectRiseDirection.左:
                    m_TheScale = m_TheRectangleF.Width / m_TheMaxValue;
                    num = m_IsNeedIncrease ? ReturnTheVariation(ref currentValue) * m_TheScale : currentValue * m_TheScale;
                    gs.FillRectangle(m_TheBrush, new RectangleF(m_TheRectangleF.X + m_TheRectangleF.Width - num, m_TheRectangleF.Y, num, m_TheRectangleF.Height));
                    break;

                case RectRiseDirection.右:
                    m_TheScale = m_TheRectangleF.Width / m_TheMaxValue;
                    num = m_IsNeedIncrease ? ReturnTheVariation(ref currentValue) * m_TheScale : currentValue * m_TheScale;
                    gs.FillRectangle(m_TheBrush, new RectangleF(m_TheRectangleF.X, m_TheRectangleF.Y, num, m_TheRectangleF.Height));
                    break;
            }
        }

        private float ReturnTheVariation(ref float theValue)
        {
            if (m_ViewValue > theValue)
            {
                if (m_ViewValue - m_TmpStrpLength >= theValue)
                {
                    m_TmpNeedChangeLength = -TmpStrpLength;
                }
                else
                {
                    m_TmpNeedChangeLength = theValue - m_ViewValue;
                }
            }
            else if (m_ViewValue < theValue)
            {
                if (m_ViewValue + m_TmpStrpLength <= theValue)
                {
                    m_TmpNeedChangeLength = m_TmpStrpLength;
                }
                else
                {
                    m_TmpNeedChangeLength = theValue - m_ViewValue;
                }
            }
            else
            {
                m_TmpNeedChangeLength = 0f;
            }
            m_ViewValue += m_TmpNeedChangeLength;
            return m_ViewValue;
        }

        // Properties
        public float TmpStrpLength
        {
            get
            {
                return m_TmpStrpLength;
            }

            set
            {
                m_TmpStrpLength = value;
            }
        }
    }
}