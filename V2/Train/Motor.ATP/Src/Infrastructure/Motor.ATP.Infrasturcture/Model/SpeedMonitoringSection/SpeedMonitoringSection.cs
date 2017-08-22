using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection
{
    public class SpeedMonitoringSection : ATPPartialBase, ISpeedMonitoringSection
    {
        private double m_BrakingStartPoint;
        private IPlanSectionCoordinate m_PlanSectionCoordinate;
        private SpeedCurve m_SpeedCurve;
        private SpeedMonitoringSectionType m_Type;
        private bool m_Visible;
        private bool m_ZoomVisible;

        public SpeedMonitoringSection(ATPDomain parent) : base(parent)
        {
            Visible = true;
        }

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }
                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        public SpeedMonitoringSectionType Type
        {
            get { return m_Type; }
            set
            {
                if (value == m_Type)
                {
                    return;
                }
                m_Type = value;
                RaisePropertyChanged(() => Type);
            }
        }

        public bool ZoomVisible
        {
            get { return m_ZoomVisible; }
            set
            {
                if (value == m_ZoomVisible)
                {
                    return;
                }

                m_ZoomVisible = value;
                RaisePropertyChanged(() => ZoomVisible);
            }
        }

        public SpeedCurve SpeedCurve
        {
            set
            {
                m_SpeedCurve = value;
                RaisePropertyChanged(() => SpeedCurve);
            }
            get { return m_SpeedCurve; }
        }

        ISpeedCurve ISpeedMonitoringSection.SpeedCurve
        {
            get { return m_SpeedCurve; }
        }

        public double BrakingStartPoint
        {
            get { return m_BrakingStartPoint; }
            set
            {
                m_BrakingStartPoint = value;
                RaisePropertyChanged(() => BrakingStartPoint);
            }
        }


        public IPlanSectionCoordinate PlanSectionCoordinate
        {
            set
            {
                if (Equals(value, m_PlanSectionCoordinate))
                {
                    return;
                }
                m_PlanSectionCoordinate = value;
                RaisePropertyChanged(() => PlanSectionCoordinate);
            }
            get { return m_PlanSectionCoordinate; }
        }

    }
}
