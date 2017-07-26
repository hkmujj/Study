using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;
using Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection;

namespace Motor.ATP._300T.Subsys.Model
{
    public class ATP300TSpeedDialPlate : ISpeedDialPlate
    {
        public ReadOnlyCollection<ISpeedDialPlateDegree> AllSpeedDegrees { get; private set; }

        public ATP300TSpeedDialPlate()
        {
            var allDegrees = new List<ISpeedDialPlateDegree>();
            AllSpeedDegrees = new ReadOnlyCollection<ISpeedDialPlateDegree>(allDegrees);

            var txts = GetTexts().GetEnumerator();
            foreach (var speedAngle in GetSpeedAngles())
            {
                var len = 20;
                var txt = string.Empty;
                if (speedAngle.Item1 % 50 == 0)
                {
                    txts.MoveNext();
                    txt = txts.Current;
                    len = 30;
                }
                allDegrees.Add(new SpeedDialPlateDegree(speedAngle.Item1, speedAngle.Item2, len, txt));
            }
        }

        private IEnumerable<string> GetTexts()
        {
            for (int i = 0; i <= 150; i += 50)
            {
                yield return i.ToString(CultureInfo.InvariantCulture);
            }

            for (int i = 200; i <= 450; i += 50)
            {
                yield return i.ToString(CultureInfo.InvariantCulture);
            }
        }

        private IEnumerable<Tuple<int, int>> GetSpeedAngles()
        {
            var startAng = -50;
            var count = 15;
            float step = ((float)85 - startAng) / count;

            for (int i = 0; i <= count; i++)
            {
                yield return new Tuple<int, int>(i * 10, (int)(startAng + step * i));
            }

            startAng = 85;
            count = 30;
            step = ((float)180 + 50 - 85) / count;
            for (int i = 1; i <= count; i++)
            {
                yield return new Tuple<int, int>(150 + i * 10, (int)(startAng + step * i));
            }
        }


        public float ConvertSpeedToAngle(float speed)
        {
            // -1 减小误差
            return GetSpeedAngle(speed) - 0.5f;
        }

        private float GetSpeedAngle(float speed)
        {
            var max = AllSpeedDegrees.FirstOrDefault(f => speed <= f.Speed);
            if (max == null)
            {
                max = AllSpeedDegrees.Last();
                return max.Angle;
            }
            else if (max == AllSpeedDegrees.First())
            {
                return max.Angle;
            }
            var min = AllSpeedDegrees[AllSpeedDegrees.IndexOf(max) - 1];
            if (max == min)
            {
                return min.Angle;
            }
            var step = max.Speed - min.Speed;
            return ((speed - min.Speed) / step) * (max.Angle - min.Angle) + min.Angle;
        }

        public float ConvertSpeedToDrawArcAngle(float speed)
        {
            return ConvertSpeedToAngle(speed) - 90;
        }
    }

}