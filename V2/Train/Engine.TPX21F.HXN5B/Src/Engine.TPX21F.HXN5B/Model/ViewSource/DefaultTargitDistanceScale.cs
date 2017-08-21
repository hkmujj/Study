using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Engine.TPX21F.HXN5B.Model.Interface;

namespace Engine.TPX21F.HXN5B.Model.ViewSource
{
    public class DefaultTargitDistanceScale : ITargitDistanceScale
    {
        public static readonly DefaultTargitDistanceScale Instance = new DefaultTargitDistanceScale();

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
            var pen = new Pen(Brushes.White, 2);
            var items =
                Enumerable.Range(0, 13)
                    .Select(s => new TargitDistanceScaleItem(s*0.25, GetLocatoin(s* 0.25), pen, 0.5, GetDistanceText(s * 0.25)))
                    .ToList();
            foreach (var it in items.Where(w => (int)( w.Value * 10 ) % 5 == 0))
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
            return value/3d;
        }

        public string GetDistanceText(double distance)
        {
            // 0.5的整倍数 
            if (((int)(distance*10))%5 == 0)
            {
                if (Math.Abs(distance) < float.Epsilon)
                {
                    return "0";
                }
                return ((int) (distance*10)/5*0.5).ToString("0.0");
            }

            return string.Empty;
        }
    }
}