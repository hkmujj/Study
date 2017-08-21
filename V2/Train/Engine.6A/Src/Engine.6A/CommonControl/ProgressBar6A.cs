using System.Windows;
using System.Windows.Controls;

namespace Engine._6A.CommonControl
{
    public class ProgressBar6A : ProgressBar
    {
        private Border m_Col;
        private CornerRadius m_Radius;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            m_Col = this.Template.FindName("PART_Indicator", this) as Border;
            m_Radius = new CornerRadius(m_Col.CornerRadius.TopLeft, m_Col.CornerRadius.TopRight, m_Col.CornerRadius.BottomRight, m_Col.CornerRadius.BottomLeft);
        }
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            if (m_Col==null)
            {
                return;
            }
            if (newValue < Maximum)
            {
                if (m_Col.CornerRadius.BottomRight > float.Epsilon)
                {
                    m_Col.CornerRadius = new CornerRadius(m_Radius.TopLeft, 0, 0, m_Radius.BottomLeft);
                }
            }
            if (newValue== Maximum)
            {
                m_Col.CornerRadius = new CornerRadius(m_Radius.TopLeft, m_Radius.TopRight, m_Radius.BottomRight, m_Radius.BottomLeft);
            }

        }
    }
}