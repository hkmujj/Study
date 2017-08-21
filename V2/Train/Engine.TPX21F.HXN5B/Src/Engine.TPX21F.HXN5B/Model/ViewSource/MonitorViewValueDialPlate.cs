using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Engine.TPX21F.HXN5B.Model.Interface;

namespace Engine.TPX21F.HXN5B.Model.ViewSource
{
    public class MonitorViewValueDialPlate : IValueDialPlate
    {
        public static readonly MonitorViewValueDialPlate Instance = new MonitorViewValueDialPlate();

        public ReadOnlyCollection<IValueDialPlateDegree> AllValueDegrees { get; private set; }

        private MonitorViewValueDialPlate()
        {
            var allDegrees = new List<IValueDialPlateDegree>();
            AllValueDegrees = new ReadOnlyCollection<IValueDialPlateDegree>(allDegrees);

            var txts = GetTexts().GetEnumerator();
            foreach (var speedAngle in GetSpeedAngles())
            {
                var len = 10;
                var txt = string.Empty;
                if (speedAngle.Item1 % 10 == 0)
                {
                    len = 20;
                }
                if (speedAngle.Item1 % 20 == 0)
                {
                    txts.MoveNext();
                    txt = txts.Current;
                }
                allDegrees.Add(new ValueDialPlateDegree(speedAngle.Item1, speedAngle.Item2, len, txt));
            }
        }

        private IEnumerable<string> GetTexts()
        {
            for (int i = 0; i <= 120; i += 20)
            {
                yield return i.ToString(CultureInfo.InvariantCulture);
            }
        }

        private IEnumerable<Tuple<int, int>> GetSpeedAngles()
        {
            var startAng = 0;
            var count = 24;
            float step = ( (float)180 - startAng ) / count;

            for (int i = 0; i <= count; i++)
            {
                yield return new Tuple<int, int>(i * 5, (int)( startAng + step * i ));
            }
        }


        public float ConvertValueToAngle(float value)
        {
            // -1 减小误差
            return GetSpeedAngle(value) - 0.5f;
        }

        private float GetSpeedAngle(float speed)
        {
            var max = AllValueDegrees.FirstOrDefault(f => speed <= f.Value);
            if (max == null)
            {
                max = AllValueDegrees.Last();
                return max.Angle;
            }
            else if (max == AllValueDegrees.First())
            {
                return max.Angle;
            }
            var min = AllValueDegrees[AllValueDegrees.IndexOf(max) - 1];
            if (max == min)
            {
                return min.Angle;
            }
            var step = max.Value - min.Value;
            return ( ( speed - min.Value ) / step ) * ( max.Angle - min.Angle ) + min.Angle;
        }

        public float ConvertValueToDrawArcAngle(float value)
        {
            return ConvertValueToAngle(value) - 180;
        }
    }
}