using System;
using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP.Infrasturcture.Control.Service
{
    public class OpaqueLayerService : IOpaqueLayerService
    {
        public const int MaxAlpha = 220;

        // ReSharper disable once InconsistentNaming
        private readonly SolidBrush m_SolidBrush;
        private float m_LightPencent;
        private static Color m_BackColor;
        private static int m_Alpha;

        public Color BackColor
        {
            set
            {
                m_BackColor = value;
                UpdateColor();
            }
            get { return m_BackColor; }
        }

        public int Alpha
        {
            set
            {
                m_Alpha = value;
                UpdateColor();
            }
            get { return m_Alpha; }
        }

        public float LightPencent
        {
            get { return m_LightPencent; }
            set
            {
                m_LightPencent = Math.Max(0, Math.Min(1, value));
                Alpha = (int) (MaxAlpha - m_LightPencent*MaxAlpha);
            }
        }

        public OpaqueLayerService()
        {
            m_SolidBrush = new SolidBrush(Color.Transparent);
        }

        private void UpdateColor()
        {
            m_SolidBrush.Color = Color.FromArgb(Alpha, BackColor);
        }

        public void PaintOpaqueLayer(object sender, PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.FillRectangle(m_SolidBrush, paintEventArgs.ClipRectangle);
        }
    }
}