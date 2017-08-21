using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using Motor.ATP.CommonView.Model;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.TargetDistance;

namespace Motor.ATP.CommonView.View.RegionA
{

    /// <summary>
    /// 目标距离监视
    /// </summary>
    public partial class TargetDistanceMonitorControl : UserControl
    {
        private double m_TargetDistance;
        private ITargitDistanceScale m_TargetDistanceScale;
        private IOther m_Other;
        private Action m_UpdateTargetDistanceAction;

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

        public TargetDistanceMonitorControl()
        {
            InitializeComponent();
            m_UpdateTargetDistanceAction = () =>
            {
                distanceLable.Text = TargetDistanceScale.GetDistanceText(TargetDistance);
                distanceProgressBar1.ScaleValue = TargetDistanceScale.GetLocatoin(TargetDistance);
            };
            TargetDistanceScale = new DefaultTargitDistanceScale();

            UpdateInnerControlForceColor();

            ForeColorChanged += (sender, args) => UpdateInnerControlForceColor();

            distanceLable.MakeAutoFont("9999");
        }

        private void UpdateInnerControlForceColor()
        {
            distanceProgressBar1.ForeColor = ForeColor;
            distanceLable.ForeColor = ForeColor;
        }


        private void UpdateByTargetDistance()
        {
            if (TargetDistanceScale != null)
            {
                this.InvokeIfNeed(m_UpdateTargetDistanceAction);
            }
        }
    }
}
