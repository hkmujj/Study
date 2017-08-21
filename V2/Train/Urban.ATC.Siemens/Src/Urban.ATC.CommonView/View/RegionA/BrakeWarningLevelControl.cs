using System;
using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionA
{
    public partial class BrakeWarningLevelControl : UserControl
    {
        private BrakeWarningLevel m_BrakeWarningLevel;
        private RectangleF m_ContentRectangle;
        private readonly SolidBrush m_ContentBrush;

        public BrakeWarningLevel BrakeWarningLevel
        {
            set
            {
                if (m_BrakeWarningLevel != value)
                {
                    m_BrakeWarningLevel = value;
                    UpdateContentRegion();
                    Invalidate();
                }
                
            }
            get { return m_BrakeWarningLevel; }
        }


        public Color WarningColor
        {
            set {
                if (m_ContentBrush.Color != value)
                {
                    m_ContentBrush.Color = value;
                    Invalidate();
                }
            }
            get { return m_ContentBrush.Color; }
        }

        public BrakeWarningLevelControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            SizeChanged += (sender, args) => UpdateContentRegion();
            m_ContentBrush = new SolidBrush(Color.White);
            m_ContentRectangle = Rectangle.Empty;
        }
        private void UpdateContentRegion()
        {
            var region = this.GetActureRectangleF();
            switch (BrakeWarningLevel)
            {
                case BrakeWarningLevel.LevelUnkown:
                case BrakeWarningLevel.Level0:
                    m_ContentRectangle = RectangleF.Empty;
                    break;
                case BrakeWarningLevel.Level1:
                    m_ContentRectangle = RectangleF.Inflate(region, -region.Width / 2 / 4 * 3, -region.Height / 2 / 4 * 3);
                    break;
                case BrakeWarningLevel.Level2:
                    m_ContentRectangle = RectangleF.Inflate(region, -region.Width / 2 / 2, -region.Height / 2 / 2);
                    break;
                case BrakeWarningLevel.LevelFull:
                    m_ContentRectangle = region;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_ContentRectangle != RectangleF.Empty)
            {
                e.Graphics.FillRectangle(m_ContentBrush, m_ContentRectangle);
            }
        }
    }
}
