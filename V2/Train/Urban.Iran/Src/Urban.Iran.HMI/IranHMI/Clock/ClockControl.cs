using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util.Extension;

namespace Urban.Iran.HMI.Clock
{
    public class ClockControl : CommonInnerControlBase
    {
        public Image DegreeImage { set; get; }

        public Image HourImage { set; get; }

        public Image MinuteImage { set; get; }

        public Brush ForceBrush { set; get; }

        private PointF m_CenterPoint;

        private int m_R;

        // ReSharper disable once PossibleLossOfFraction
        private const float AnglePerSecnde = 360/60;

        // ReSharper disable once PossibleLossOfFraction
        private const float AnglePerHour = 360/12;

        private List<RectangleF> m_SecondeRectangles;

        private Matrix m_MinuteMatrix;
        private Matrix m_HourMatrix;

        public ClockControl()
        {
            ForceBrush = Brushes.White;

            OutLineChanged += OnOutLineChanged;

            OnOutLineChanged(null, null);
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            m_CenterPoint = OutLineRectangle.GetCenterPoint();
            m_R = Math.Min(Size.Height, Size.Width) / 2;
            m_MinuteMatrix= new Matrix();
            m_HourMatrix = new Matrix();
        }

        public override void Init()
        {
            InitDegrees();

            InitDegreeImageIfNeed();

            InitHourImageIfNeed();

            InitMinitImageIfNeed();
        }

        private void InitDegrees()
        {
            var centers = Enumerable.Range(0, 60)
                .Select(
                    s =>
                        new PointF((float) (Math.Cos((s*AnglePerSecnde - 90)*Math.PI/180)*m_R) + m_CenterPoint.X,
                            (float) (Math.Sin((s*AnglePerSecnde - 90)*Math.PI/180)*m_R) + m_CenterPoint.Y));

            m_SecondeRectangles = centers.Select(s => RectangleF.Inflate(new RectangleF(s, Size.Empty), 3, 3)).ToList();
        }

        private void InitMinitImageIfNeed()
        {
            if (MinuteImage == null)
            {
                MinuteImage = new Bitmap(Size.Width, Size.Height);

                var center = new PointF(Size.Width/2, Size.Height/2);
                var g = Graphics.FromImage(MinuteImage);
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear(Color.Transparent);

                var points = new PointF[5];
                points[0] = center.Translate(0, 15);
                var angle = -40 * Math.PI / 180;
                var l1 = 10F;
                points[1] = center.Translate((float)(Math.Sin(angle) * l1), -3);
                points[2] = new PointF(center.X, 15);

                angle = -angle;
                points[3] = center.Translate((float)(Math.Sin(angle) * l1), -3);
                points[4] = points[0];

                g.FillPolygon(Brushes.White, points);

                g.Dispose();
            }
        }

        private void InitHourImageIfNeed()
        {
            if (HourImage == null)
            {
                HourImage = new Bitmap(Size.Width, Size.Height);

                var g = Graphics.FromImage(HourImage);
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear(Color.Transparent);

                var center = new PointF(Size.Width / 2, Size.Height / 2);

                var points = new PointF[5];
                points[0] = center.Translate(0, 15);
                var angle = -40 * Math.PI / 180;
                var l1 = 10F;
                points[1] = center.Translate((float)(Math.Sin(angle) * l1), -3);
                points[2] = new PointF(center.X, 40);

                angle = -angle;
                points[3] = center.Translate((float)(Math.Sin(angle) * l1), -3);
                points[4] = points[0]; g.FillPolygon(Brushes.White, points);

                g.Dispose();
            }
        }

        private void InitDegreeImageIfNeed()
        {
            if (DegreeImage == null)
            {
                DegreeImage = new Bitmap(Size.Width, Size.Height);
                var g = Graphics.FromImage(DegreeImage);

                g.Clear(Color.Transparent);

                g.FillEllipse(ForceBrush, 0, 0, Size.Width, Size.Height);

                g.Dispose();
            }
        }

        public override void OnDraw(Graphics g)
        {
            var now = DateTime.Now;

            foreach (var e in m_SecondeRectangles.Take(now.Second))
            {
                g.DrawImage(DegreeImage, e);
            }

            UpdateMatrixes(now);

            var old = g.Transform;
            g.Transform = m_HourMatrix;
            g.DrawImage(HourImage, OutLineRectangle);

            g.Transform = m_MinuteMatrix;
            g.DrawImage(MinuteImage, OutLineRectangle);

            g.Transform = old;
        }

        private void UpdateMatrixes(DateTime now)
        {
            m_HourMatrix.Reset();
            m_HourMatrix.RotateAt(now.Hour * AnglePerHour + now.Minute / 60f * AnglePerHour, m_CenterPoint);

            m_MinuteMatrix.Reset();
            m_MinuteMatrix.RotateAt(now.Minute * AnglePerSecnde, m_CenterPoint);
        }
    }
}