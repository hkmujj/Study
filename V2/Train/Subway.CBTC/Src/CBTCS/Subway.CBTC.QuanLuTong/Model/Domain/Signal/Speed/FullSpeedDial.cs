using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using CBTC.Infrasturcture.Model.Signal.Speed;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.CBTC.QuanLuTong.Model.Domain.Signal.Speed
{
    public class FullSpeedDial : NotificationObject, ISpeedDialPlate
    {
        public static readonly FullSpeedDial Instance = new FullSpeedDial();

        /// <summary>
        /// 最小值
        /// </summary>
        public SpeedDialPlateDegree MinDegree { get { return AllSpeedDegrees[0]; } }

        /// <summary>
        /// 最大值
        /// </summary>
        public SpeedDialPlateDegree MaxDegree { get { return AllSpeedDegrees.Last(); } }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<SpeedDialPlateDegree> AllSpeedDegrees { get; private set; }

        protected FullSpeedDial()
        {
            var allDegrees = new List<SpeedDialPlateDegree>();

            AllSpeedDegrees = new ReadOnlyCollection<SpeedDialPlateDegree>(allDegrees);

            var txts = GetTexts().GetEnumerator();
            foreach (var speedAngle in GetSpeedAngles())
            {
                var len = 10;
                var txt = string.Empty;
                if (speedAngle.Item1 % 10 == 0)
                {
                    txts.MoveNext();
                    txt = txts.Current;
                    len = 25;
                }
                allDegrees.Add(new SpeedDialPlateDegree(speedAngle.Item1, speedAngle.Item2, len, txt));
            }

            RaisePropertyChanged(() =>AllSpeedDegrees);
        }

        private IEnumerable<string> GetTexts()
        {
            for (int i = 0; i <= 90; i += 10)
            {
                yield return i.ToString(CultureInfo.InvariantCulture);
            }
        }

        private IEnumerable<Tuple<int, int>> GetSpeedAngles()
        {
            var startAng = -50;
            var count = 18;
            float step = ((float)180 + 50 - startAng) / count;

            for (int i = 0; i <= count; i++)
            {
                yield return new Tuple<int, int>(i * 5, (int)(startAng + step * i));
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