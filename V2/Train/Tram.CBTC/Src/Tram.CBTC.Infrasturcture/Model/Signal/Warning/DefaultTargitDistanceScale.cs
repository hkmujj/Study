using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tram.CBTC.Infrasturcture.Model.Signal.Warning
{
    public class DefaultTargitDistanceScale : ITargitDistanceScale
    {
        private static readonly double LogDegreeStart = Math.Log10(LineMaxValue);
        private static readonly double LogDegreeLength = Math.Log10(LogMaxValue) - LogDegreeStart;
        private double m_LineScale;

        public const int MaxValue = LogMaxValue;

        private const int LogMaxValue = 1000;
        private const int LineMaxValue = 100;


        public static readonly DefaultTargitDistanceScale Instance = new DefaultTargitDistanceScale();

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
            var items = Enumerable.Range(0, 11).Select(s => new TargitDistanceScaleItem(s * 100, GetLocatoin(s * 100), 0.7)).ToList();
            foreach (var it in items.Where(w => (int)w.Value % 500 == 0))
            {
                it.DegreeLength = 1;
            }

            TargitDistanceScaleItems = items.ToList().AsReadOnly();
        }

        public ReadOnlyCollection<TargitDistanceScaleItem> TargitDistanceScaleItems { get; private set; }

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
            var d = (int)Math.Round(distance, 0);

            if (d < LineMaxValue)
            {
                return Math.Max(0, d).ToString("0");
            }
            return (d / 10 * 10).ToString("0");
        }
    }
}