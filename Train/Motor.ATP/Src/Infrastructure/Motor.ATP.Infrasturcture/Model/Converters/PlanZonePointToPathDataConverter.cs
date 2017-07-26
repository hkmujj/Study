using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public class PlanZonePointToPathDataConverter : IMultiValueConverter
    {
        private DistanceSpeedPoint[] m_PointsBuff;

        private readonly StringBuilder m_StringBuilder;

        private DistanceSpeedPoint m_ZeroDistanceSpeedPoint = new DistanceSpeedPoint();

        public PlanZonePointToPathDataConverter()
        {
            m_PointsBuff = new DistanceSpeedPoint[10];
            m_StringBuilder = new StringBuilder();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Any(a => a == DependencyProperty.UnsetValue) ||
                values.Length != 4
                || !(values[0] is IPlanSectionCoordinate)
                || !(values[1] is double)
                || !(values[2] is double)
                || !(values[3] is ObservableCollection<DistanceSpeedPoint>))
            {
                return DependencyProperty.UnsetValue;
            }

            var ord = (IPlanSectionCoordinate) values[0];
            var wid = (double) values[1];
            var hei = (double) values[2];

            
            var src = (ObservableCollection<DistanceSpeedPoint>) values[3];
            if (src.Count > m_PointsBuff.Length)
            {
                m_PointsBuff = new DistanceSpeedPoint[src.Count];
            }

            Array.Clear(m_PointsBuff, 0, m_PointsBuff.Length);

            src.CopyTo(m_PointsBuff, 0);
            var pts = m_PointsBuff;
            
            m_StringBuilder.Clear();
            if (pts.Any(a => a != null))
            {
                m_StringBuilder.AppendFormat("M {0},{1} ", GetX(ord, pts[0].Distance, wid), GetY(ord, pts[0].Speed, hei));
                foreach (var pt in pts.Where(w => w != null).Skip(1))
                {
                    var px = GetX(ord, pt.Distance, wid);
                    var py = GetY(ord, pt.Speed, hei);
                    m_StringBuilder.AppendFormat("L {0},{1} ", px, py);
                }

                var last = pts.Last(w => w != null);
                if (Math.Abs(last.Speed) > 0)
                {
                    m_StringBuilder.AppendFormat("L {0},{1} ", GetX(ord, last.Distance, wid), GetY(ord, 0, hei));
                }

                m_StringBuilder.AppendFormat("L {0},{1} ", GetX(ord, 0, wid), GetY(ord, 0, hei));

                m_StringBuilder.Append("Z");

                return m_StringBuilder.ToString();

            }

            return DependencyProperty.UnsetValue;
        }

        private static double GetY(IPlanSectionCoordinate ord, double speed, double hei)
        {
            return (1- ord.GetSpeedScal(speed))*hei;
        }

        private static double GetX(IPlanSectionCoordinate ord, double distance, double wid)
        {
            return ord.GetDistanceScal(distance) *wid;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
