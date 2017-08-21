
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Motor.ATP.Domain.Interface.TargetDistance;

namespace Urban.ATC.CommonView.Model
{
    public class ATCTargitDistanceScale : ITargitDistanceScale
    {
        private static readonly double LogDegreeStart = Math.Log10(LineMaxValue);
        private static readonly double LogDegreeLength = Math.Log10(LogMaxValue) - LogDegreeStart;
        private double m_LineScale;

        public const int MaxValue = LogMaxValue;

        private const int LogMaxValue = 750;
        private const int LineMaxValue = 1;

        /// <summary>
        /// 下面线性部分所占百分比
        /// </summary>
        public double LineScale
        {
            set
            {
                m_LineScale = value;
                UpdateTargitDistanceScaleItems();
            }
            get { return m_LineScale; }
        }

        private void UpdateTargitDistanceScaleItems()
        {
            var pen = new Pen(Color.White, 2);

            var txts = new List<int>() { 1, 2, 5, 10, 20, 50, 100, 200, 500, 750 };

            var items =
                txts.Select(s => new TargitDistanceScaleItem(s, GetLocatoin(s), pen, 0.8, s.ToString()))
                    .ToList();
            items[0] = new TargitDistanceScaleItem(items[0].Value, items[0].DegreeLocation, items[0].DegreePen,
                items[0].DegreeLength, "m1");

            TargitDistanceScaleItems = items.Cast<ITargitDistanceScaleItem>().ToList().AsReadOnly();
        }

        public ReadOnlyCollection<ITargitDistanceScaleItem> TargitDistanceScaleItems { get; private set; }

        public ATCTargitDistanceScale()
        {
            LineScale = 0.05;
        }

        public double GetLocatoin(double value)
        {
            if (value <= LineMaxValue)
            {
                return 1 * LineScale;
                // return (value / LineMaxValue) * LineScale;
            }

            if (value >= LogMaxValue)
            {
                return 1;
            }

            return ((Math.Log10(value) - LogDegreeStart) / LogDegreeLength) * (1 - LineScale) + LineScale;
        }

        public string GetDistanceText(double distance)
        {
            return distance.ToString("0");
            //var d = (int)Math.Round(distance, 0);

            //if (d < LineMaxValue)
            //{
            //    return Math.Max(0, d).ToString("0");
            //}
            //return (d / 10 * 10).ToString("0");
        }
    }
}