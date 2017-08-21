using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionD
{
    /// <summary>
    /// 起模点
    /// </summary>
    public partial class StartLocationPanel : Panel
    {
        private IPlanSectionCoordinate m_PlanSectionCoordinate;

        private Pen m_StartLocationPen;
        private double m_StartLocation;

        private readonly PointF[] m_StartLocationLine;
        private float m_XDeviation;
        private ISpeedMonitoringSection m_SpeedMonitoringSection;


        /// <summary>
        /// X轴的误差补偿
        /// </summary>
        public float XDeviation
        {
            set
            {
                m_XDeviation = value;
                UpdateStartLocationLine(); Invalidate();
            }
            get { return m_XDeviation; }
        }

        /// <summary>
        /// 速度监视区
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ISpeedMonitoringSection SpeedMonitoringSection
        {
            set
            {
                if (m_SpeedMonitoringSection != value)
                {
                    if (m_SpeedMonitoringSection != null)
                    {
                        m_SpeedMonitoringSection.PropertyChanged -= SpeedMonitoringSectionOnPropertyChanged;
                    }
                    m_SpeedMonitoringSection = value;
                    if (m_SpeedMonitoringSection != null)
                    {
                        StartLocation = m_SpeedMonitoringSection.BrakingStartPoint;
                        m_SpeedMonitoringSection.PropertyChanged += SpeedMonitoringSectionOnPropertyChanged;
                    }
                    Invalidate();
                }
                
            }
            get { return m_SpeedMonitoringSection; }
        }

       
        /// <summary>
        /// 起模点笔
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Pen StartLocationPen
        {
            set
            {
                m_StartLocationPen = value;
                Invalidate();
            }
            get { return m_StartLocationPen; }
        }


        /// <summary>
        /// 起模点距离值 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double StartLocation
        {
            set
            {
                m_StartLocation = value;
                UpdateStartLocationLine();
                Invalidate();
            }
            get { return m_StartLocation; }
        }

        /// <summary>
        /// 坐标系
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IPlanSectionCoordinate PlanSectionCoordinate
        {
            set
            {
                m_PlanSectionCoordinate = value;

                UpdateStartLocationLine();

                Invalidate();
            }
            get { return m_PlanSectionCoordinate; }
        }

        public StartLocationPanel()
        {
            m_StartLocationLine = new[] { new PointF(float.MaxValue, float.MaxValue), new PointF(float.MaxValue, float.MaxValue) };
            m_StartLocationPen = new Pen(Color.Yellow, 3);

            InitializeComponent();

            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            StartLocation = double.PositiveInfinity;

            SizeChanged += (sender, args) => UpdateStartLocationLine();

        }
        private void SpeedMonitoringSectionOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<ISpeedMonitoringSection,double>(s => s.BrakingStartPoint))
            {
                StartLocation = SpeedMonitoringSection.BrakingStartPoint;
            }
        }

        private void UpdateStartLocationLine()
        {
            if (double.IsNaN(StartLocation) || double.IsInfinity(StartLocation) || PlanSectionCoordinate == null)
            {
                return;
            }

            var act = this.GetActureRectangleF();

            UpdateStartLocationLineY(act);

            UpdateStartLocationLineX(act);
        }
        private void UpdateStartLocationLineX(RectangleF act)
        {
            var x = PlanSectionCoordinate.GetDistanceScal(StartLocation) * act.Width + act.Left + XDeviation;
            m_StartLocationLine[0].X = x;
            m_StartLocationLine[1].X = x;
        }

        private void UpdateStartLocationLineY(RectangleF rect)
        {
            m_StartLocationLine[0].Y = rect.Top;
            m_StartLocationLine[1].Y = rect.Bottom;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (PlanSectionCoordinate == null)
            {
                return;
            }
            // 起模点
            e.Graphics.DrawLine(StartLocationPen, m_StartLocationLine[0], m_StartLocationLine[1]);
        }
    }
}
