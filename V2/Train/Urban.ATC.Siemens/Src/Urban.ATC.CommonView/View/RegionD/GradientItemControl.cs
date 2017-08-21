using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionD
{
    internal partial class GradientItemControl : UserControl
    {
        private IGradientInfomationItem m_GradientInfomationItem;
        private GradientType m_GradientType;

        private Font m_TxtFont;

        private readonly Graphics m_Graphics;
        private float m_SlopeValue;
        private float m_ParentWidth;

        private const float MaxFontSize = 33;

        private string m_SclopeTypeLogo;
        private readonly Action m_UpdateInfomationsAction;
        private readonly Action m_UpdateRegionAction;

        public float ParentWidth
        {
            set
            {
                m_ParentWidth = value;
                UpdateRegion();
                Invalidate();
            }
            get { return m_ParentWidth; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IGradientInfomationItem GradientInfomationItem
        {
            get { return m_GradientInfomationItem; }
            set
            {
                if (Equals(value, m_GradientInfomationItem))
                {
                    return;
                }
                if (m_GradientInfomationItem != null)
                {
                    m_GradientInfomationItem.PropertyChanged -= SlopeInfomationItemOnPropertyChanged;
                }
                m_GradientInfomationItem = value;
                if (GradientInfomationItem != null)
                {
                    UpdateInfomations();
                    GradientInfomationItem.PropertyChanged += SlopeInfomationItemOnPropertyChanged;
                }
            }
        }

        public GradientType GradientType
        {
            set
            {
                m_GradientType = value;
                switch (value)
                {
                    case GradientType.None:
                        BackColor = Color.White;
                        m_SclopeTypeLogo = "+";
                        break;
                    case GradientType.Upslope:
                        BackColor = Color.White;
                        m_SclopeTypeLogo = "+";
                        break;
                    case GradientType.Downslope:
                        BackColor = Color.Gray;
                        m_SclopeTypeLogo = "-";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
                Invalidate();
            }
            get { return m_GradientType; }
        }

        public float SlopeValue
        {
            set { m_SlopeValue = value; Invalidate(); }
            get { return m_SlopeValue; }
        }


        private void SlopeInfomationItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateInfomations();
        }

        private void UpdateInfomations()
        {
            this.InvokeIfNeed(m_UpdateInfomationsAction);
        }

        private void UpdateRegion()
        {
            this.InvokeIfNeed(m_UpdateRegionAction);
        }

        public float GetDistanceScal(float distance)
        {
            if (GradientInfomationItem.Parent != null)
            {
                return
                    GradientInfomationItem.Parent.Parent.SpeedMonitoringSection.PlanSectionCoordinate.GetDistanceScal(distance);
            }
            return 0;
        }

        public GradientItemControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);

            m_UpdateRegionAction = new Action(() =>
            {
                if (GradientInfomationItem != null)
                {
                    Left = (int)(GetDistanceScal(GradientInfomationItem.StartDistance) * ParentWidth);
                    Width = (int)(GetDistanceScal(GradientInfomationItem.EndDistance) * ParentWidth - Left);
                }
            });
            m_UpdateInfomationsAction = new Action(() =>
            {
                UpdateRegion();
                SlopeValue = GradientInfomationItem.SlopeValue;
                GradientType = GradientInfomationItem.Type;
            });

            m_Graphics = CreateGraphics();
            this.SizeChanged += OnSizeChanged;


        }

        private void OnSizeChanged(object sender, EventArgs eventArgs)
        {
            Font font = null;
            for (int i = 8; i < MaxFontSize; i++)
            {
                font = new Font(this.Font.FontFamily, i);
                var size = m_Graphics.MeasureString("10", font);
                font.Dispose();
                if (size.Height > Height)
                {
                    font = new Font(Font.FontFamily, i - 1 == 0 ? 1 : i - 1);
                    break;
                }
            }

            if (m_TxtFont != null)
            {
                m_TxtFont.Dispose();
            }
            m_TxtFont = font;

            UpdateRegion();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            var slopeValue = this.SlopeValue.ToString("0");

            var slopeX = Width/2 - g.MeasureString(slopeValue, m_TxtFont, Width).Width/2;

            var brush = GetTextBrush();
            
            g.DrawString(slopeValue, m_TxtFont, brush, slopeX, 0);

            const int interval = 2;

            g.DrawString(m_SclopeTypeLogo, m_TxtFont, brush, interval, 0);

            g.DrawString(m_SclopeTypeLogo, m_TxtFont, brush,
                Width - interval - g.MeasureString(m_SclopeTypeLogo, m_TxtFont).Width, 0);
        }

        private Brush GetTextBrush()
        {
            return GradientType == GradientType.Upslope ? Brushes.Black : Brushes.White;
        }
    }
}
