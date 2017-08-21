using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionD
{
    public partial class SpeedLimitedCurvePanel : Panel
    {
        private const int MaxCurvePointCount = 40;
        private IPlanSectionCoordinate m_PlanSectionCoordinate;
        private ISpeedCurve m_SpeedCurve;
        private readonly DistanceSpeedPoint[] m_DistanceSpeedPointBuffer = new DistanceSpeedPoint[MaxCurvePointCount];

        private readonly GraphicsPath m_CurveRegion;

        private SolidBrush m_CurveForceBrush;
        private SolidBrush m_CurveBackBrush;

        private RectangleF m_CurveBackRectangleF;

        private volatile bool m_CanInvalidate;

        private static readonly int UpdateDelay = 5;

        private int m_DelayFlag;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public SolidBrush CurveForceBrush
        {
            set
            {
                m_CurveForceBrush = value;
                Invalidate();
            }
            get { return m_CurveForceBrush; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public SolidBrush CurveBackBrush
        {
            set
            {
                m_CurveBackBrush = value;
                Invalidate();
            }
            get { return m_CurveBackBrush; }
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

                UpdateContentRegion();

                Invalidate();
            }
            get { return m_PlanSectionCoordinate; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ISpeedCurve SpeedCurve
        {
            get { return m_SpeedCurve; }
            set
            {
                if (m_SpeedCurve != value)
                {
                    if (m_SpeedCurve != null)
                    {
                        m_SpeedCurve.CurvePointCollectionChanged -= CurvePointCollectionOnCollectionChanged;
                    }
                    m_SpeedCurve = value;
                    if (m_SpeedCurve != null)
                    {
                        m_SpeedCurve.CurvePointCollectionChanged += CurvePointCollectionOnCollectionChanged;
                    }

                    UpdateContentForeRegion();

                    Invalidate();
                }
            }
        }

        public SpeedLimitedCurvePanel()
        {
            InitializeComponent();
            components = new Container();
            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer, true);

            SizeChanged += (sender, args) => UpdateContentRegion();

            m_CurveRegion = new GraphicsPath();

            var refreshTimer = new System.Windows.Forms.Timer() {Interval = 200};
            refreshTimer.Tick += (sender, args) => m_CanInvalidate = true;
            refreshTimer.Start();
            components.Add(refreshTimer);

        }

        private void CurvePointCollectionOnCollectionChanged(object sender,
            EventArgs notifyCollectionChangedEventArgs)
        {

            if (++m_DelayFlag <= UpdateDelay)
            {
                return;
            }

            m_DelayFlag = 0;

            UpdateContentRegion();

            Invalidate();
        }

        public void UpdateContentRegion()
        {
            UpdateContentBackRegion();

            this.InvokeIfNeed(new Action(UpdateContentForeRegion));
        }

        private void UpdateContentForeRegion()
        {
            Array.Clear(m_DistanceSpeedPointBuffer, 0, m_DistanceSpeedPointBuffer.Length);

            if (PlanSectionCoordinate == null || SpeedCurve == null)
            {
                return;
            }

            lock (SpeedCurve.CurvePointCollection)
            {
                SpeedCurve.CurvePointCollection.CopyTo(m_DistanceSpeedPointBuffer, 0);
            }

            CorrectCurver();

            m_CurveRegion.Reset();
            var act = this.GetActureRectangleF();
            var ps = GetContent(act).ToArray();
            m_CurveRegion.AddLines(ps);
            m_CurveRegion.CloseAllFigures();

        }

        private void CorrectCurver()
        {
            var maxDistance = PlanSectionCoordinate.XAxises.Last().Value;

            var validCount = m_DistanceSpeedPointBuffer.Count(c => c != null);

            for (int i = 1; i < validCount; i++)
            {
                var item = m_DistanceSpeedPointBuffer[i];
                if (item.Distance > maxDistance)
                {
                    var before = m_DistanceSpeedPointBuffer[i - 1];
                    var mid = new DistanceSpeedPoint(maxDistance,
                        (float)
                            ((before.Speed - item.Speed) / Math.Log(maxDistance, 2) *
                             (Math.Log(Math.Min(item.Distance, 32000), 2) - Math.Log(Math.Max(1000, before.Distance), 2))  ));
                    m_DistanceSpeedPointBuffer[i] = mid;
                    if (i + 1 < m_DistanceSpeedPointBuffer.Length)
                    {
                        m_DistanceSpeedPointBuffer[i + 1] = item;
                    }
                    else
                    {
                        // 可能会出现最后一个是8000米外的点。
                    }
                    for (int j = i + 2; j < validCount; j++)
                    {
                        m_DistanceSpeedPointBuffer[j] = null;
                    }
                    break;
                }
            }
        }

        private void UpdateContentBackRegion()
        {
            if (PlanSectionCoordinate == null)
            {
                return;
            }

            var act = this.GetActureRectangleF();
            m_CurveBackRectangleF = new RectangleF(act.Left, act.Top, act.Width,
                act.Height*(1 - PlanSectionCoordinate.GetSpeedScal(0)));
        }

        private IEnumerable<PointF> GetContent(RectangleF acture)
        {
            yield return
                new PointF(acture.Left, acture.Top + acture.Height - PlanSectionCoordinate.GetSpeedScal(0)*acture.Height)
                ;

            foreach (var p in m_DistanceSpeedPointBuffer.Where(w => w != null)
                .OrderBy(o => o.Distance)
                .Select(
                    s => new PointF(PlanSectionCoordinate.GetDistanceScal(s.Distance)*acture.Width + acture.Left,
                        acture.Height - PlanSectionCoordinate.GetSpeedScal(s.Speed)*acture.Height + acture.Top)))
            {
                yield return p;
            }

            yield return
                new PointF(acture.Left, acture.Top + acture.Height - PlanSectionCoordinate.GetSpeedScal(0)*acture.Height)
                ;

        }

        protected override void NotifyInvalidate(Rectangle invalidatedArea)
        {
            if (m_CanInvalidate)
            {
                m_CanInvalidate = false;
                base.NotifyInvalidate(invalidatedArea);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (CurveBackBrush != null)
            {
                e.Graphics.FillRectangle(CurveBackBrush, m_CurveBackRectangleF);
            }

            if (PlanSectionCoordinate == null || SpeedCurve == null || CurveForceBrush == null)
            {
                return;
            }

            // 计划区
            e.Graphics.FillPath(CurveForceBrush, m_CurveRegion);

        }
    }
}
