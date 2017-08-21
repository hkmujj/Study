using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class PlanSectionCoordinateBase : IPlanSectionCoordinate
    {
        private List<ICoordinateAxis> m_CoordinateXAxises;
        private List<ICoordinateAxis> m_CoordinateYAxises;

        protected List<ICoordinateAxis> CoordinateXAxises
        {
            private set
            {
                m_CoordinateXAxises = value;
                XAxises = new ReadOnlyCollection<ICoordinateAxis>(CoordinateXAxises);
            }
            get { return m_CoordinateXAxises; }
        }

        protected List<ICoordinateAxis> CoordinateYAxises
        {
            private set
            {
                m_CoordinateYAxises = value;
                YAxises = new ReadOnlyCollection<ICoordinateAxis>(CoordinateYAxises);
            }
            get { return m_CoordinateYAxises; }
        }

        protected List<IValueScaleConverter> m_DistanceScaleConverters;
        protected List<IValueScaleConverter> m_SpeedScaleConverters;

        public ReadOnlyCollection<ICoordinateAxis> XAxises { get; private set; }

        public ReadOnlyCollection<ICoordinateAxis> YAxises { get; private set; }

        public float MaxSpeedValue
        {
            get { return YAxises.Max(m => m.Value); }
        }

        public float MaxDistanceValue
        {
            get { return XAxises.Max(m => m.Value); }
        }

        public bool CanZoomIn { get; protected set; }
        public bool CanZoomOut { get; protected set; }

        public PlanSectionCoordinateBase()
        {
            CoordinateYAxises = new List<ICoordinateAxis>();

            CoordinateXAxises = new List<ICoordinateAxis>();
        }

        public float GetDistanceScal(double distanceValue)
        {
            var convert = m_DistanceScaleConverters.Find(f => f.InRange(distanceValue));
            if (convert != null)
            {
                return (float) convert.ConvertToScale(distanceValue);
            }

            return 1;
        }

        public float GetSpeedScal(double speedValue)
        {
            return (float) m_SpeedScaleConverters.Find(f => f.InRange(speedValue)).ConvertToScale(speedValue);
        }

        public PointF GetPointF(DistanceSpeedPoint distanceSpeedPoint)
        {
            return
                new PointF(
                    GetDistanceScal(Math.Max(
                        Math.Min(distanceSpeedPoint.Distance, CoordinateXAxises.Max(m => m.Value)),
                        CoordinateXAxises.Min(m => m.Value))),
                    GetSpeedScal(Math.Max(Math.Min(distanceSpeedPoint.Speed, CoordinateYAxises.Max(m => m.Value)),
                        CoordinateYAxises.Min(m => m.Value))));
        }

        public DistanceSpeedPoint GetDistanceSpeedPoint(PointF point)
        {
            throw new NotImplementedException();
        }
    }
}