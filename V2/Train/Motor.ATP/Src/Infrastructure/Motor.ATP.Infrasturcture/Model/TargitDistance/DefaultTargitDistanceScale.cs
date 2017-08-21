using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface.TargetDistance;

namespace Motor.ATP.Infrasturcture.Model.TargitDistance
{
    public class DefaultTargitDistanceScale : ITargitDistanceScale
    {
        private static readonly double LogDegreeStart = Math.Log10(LineMaxValue);
        private static readonly double LogDegreeLength = Math.Log10(LogMaxValue) - LogDegreeStart;
        private double m_LineScale;

        public const int MaxValue = LogMaxValue;

        private const int LogMaxValue = 1000;
        private const int LineMaxValue = 100;

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
            var items = Enumerable.Range(0, 11).Select(s => new TargitDistanceScaleItem(s * 100, GetLocatoin(s * 100), pen, 0.7)).ToList();
            foreach (var it in items.Where(w => (int)w.Value % 500 == 0))
            {
                it.DegreeLength = 1;
            }
            TargitDistanceScaleItems = items.Cast<ITargitDistanceScaleItem>().ToList().AsReadOnly();
        }

        public ReadOnlyCollection<ITargitDistanceScaleItem> TargitDistanceScaleItems { get; private set; }

        public DefaultTargitDistanceScale()
        {
            LineScale = 0.7;
        }

        public double GetLocatoin(double value)
        {
            if (value <= LineMaxValue)
            {
                return (value / LineMaxValue) * LineScale;
            }

            if (value >= LogMaxValue)
            {
                return 1;
            }

            return ((Math.Log10(value) - LogDegreeStart) / LogDegreeLength) * (1 - LineScale) + LineScale;
        }

        public string GetDistanceText(double distance)
        {
            var d = (int) Math.Round(distance,0);

            if (d < LineMaxValue)
            {
                return Math.Max(0, d).ToString("0");
            }
            return (d / 10 * 10).ToString("0");
        }
    }
}