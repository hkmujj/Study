using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;
using Motor.ATP.Domain.Model.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionB
{
    public partial class SpeedArcPanel : Panel
    {
        private ISpeedDialPlate m_SpeedDialPlate;
        private readonly Pen m_ArcPen;
        private int m_ArcWidth;
        private Color m_ArcColor;
        private float m_StartSpeed;
        private float m_EndSpeed;

        public SpeedArcPanel()
        {
            InitializeComponent();
            m_ArcPen = new Pen(Color.White);
            m_SpeedDialPlate = new DefaultSpeedDialPlate();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        public ISpeedDialPlate SpeedDialPlate
        {
            set { m_SpeedDialPlate = value; Invalidate(); }
            get { return m_SpeedDialPlate; }
        }

        public int ArcWidth
        {
            set
            {
                m_ArcWidth = value;
                m_ArcPen.Width = value;
                Invalidate();
            }
            get { return m_ArcWidth; }
        }

        public Color ArcColor
        {
            set
            {
                m_ArcColor = value;
                m_ArcPen.Color = value;
                Invalidate();
            }
            get { return m_ArcColor; }
        }

        public float StartSpeed
        {
            set { m_StartSpeed = value; Invalidate(); }
            get { return m_StartSpeed; }
        }

        public float EndSpeed
        {
            set { m_EndSpeed = value; Invalidate(); }
            get { return m_EndSpeed; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (ArcColor != Color.Empty && Math.Abs(ArcWidth) > float.Epsilon && SpeedDialPlate != null)
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    var start = m_SpeedDialPlate.ConvertSpeedToDrawArcAngle(StartSpeed);
                    var end = m_SpeedDialPlate.ConvertSpeedToDrawArcAngle(EndSpeed);
                    g.DrawArc(m_ArcPen, this.GetActureRectangle(), start, end - start);
                }
            }
            catch (Exception ex )
            {
                LogMgr.Error("paint speed arc occ some errors. {0}", ex);
            }
            base.OnPaint(e);
        }
    }
}
