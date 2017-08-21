
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.Domain.Interface.TargetDistance;
using Urban.ATC.CommonView.Model;
using Urban.ATC.Domain.Interface;

namespace Urban.ATC.CommonView.View
{
    public partial class TargetView : UserControl
    {
        private double m_TargetDistance;
        private ITargitDistanceScale m_TargetDistanceScale;
        private IOther m_Other;

        private List<Brush> m_BarBrushes = new List<Brush>()
        {
            Brushes.LightGreen,
            Brushes.Yellow,
            Brushes.Red,
        };

        private TargetBarType m_TargetBarType;
        private double m_TargetSpeed;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IOther Other
        {
            set
            {
                m_Other = value;
                distanceLable.AddOpuaqueLayer(Other);
                distanceProgressBar1.AddOpuaqueLayer(Other);
                targetDistanceDegreeControl1.AddOpuaqueLayer(Other);
            }
            get { return m_Other; }
        }

        /// <summary>
        /// 目标距离
        /// </summary>
        public double TargetDistance
        {
            set
            {
                if (Math.Abs(m_TargetDistance - value) > float.Epsilon)
                {
                    m_TargetDistance = value;
                    UpdateByTargetDistance();
                    Invalidate();
                }
            }
            get { return m_TargetDistance; }
        }

        /// <summary>
        /// 目标距离
        /// </summary>
        public double TargetSpeed
        {
            get { return m_TargetSpeed; }
            set
            {
                if (Math.Abs(m_TargetSpeed - value) > float.Epsilon)
                {
                    m_TargetSpeed = value;
                    UpdateByTargetDistance();
                    Invalidate();
                }
            }
        }

        public TargetBarType TargetBarType
        {
            set
            {
                m_TargetBarType = value;
                switch (value)
                {
                    case TargetBarType.LightGreen:
                        distanceProgressBar1.ContentBrush = m_BarBrushes[0];
                        break;

                    case TargetBarType.Yellow:
                        distanceProgressBar1.ContentBrush = m_BarBrushes[1];
                        break;

                    case TargetBarType.Red:
                        distanceProgressBar1.ContentBrush = m_BarBrushes[2];
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("value", value, null);
                }
                Invalidate();
            }
            get { return m_TargetBarType; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ITargitDistanceScale TargetDistanceScale
        {
            set
            {
                Contract.Requires(value != null);

                m_TargetDistanceScale = value;
                targetDistanceDegreeControl1.TargitDistanceScale = value;
                Invalidate();
            }
            get { return m_TargetDistanceScale; }
        }

        public TargetView()
        {
            InitializeComponent();

            TargetDistanceScale = new ATCTargitDistanceScale();

            UpdateInnerControlForceColor();

            ForeColorChanged += (sender, args) => UpdateInnerControlForceColor();

            distanceLable.MakeAutoFont("9999");
            TargetBarType = TargetBarType.LightGreen;
            distanceLable.ForeColor = Color.LightGray;
        }

        private void UpdateInnerControlForceColor()
        {
            distanceProgressBar1.ForeColor = ForeColor;
            //distanceLable.ForeColor = ForeColor;
        }

        private void UpdateByTargetDistance()
        {
            if (TargetDistanceScale != null)
            {
                this.InvokeIfNeed(new Action(() =>
                {
                    distanceLable.Text = TargetDistanceScale.GetDistanceText(TargetSpeed);
                    distanceProgressBar1.ScaleValue = TargetDistanceScale.GetLocatoin(TargetDistance);
                }));
            }
        }
    }
}