﻿using System.Collections.ObjectModel;
using System.Linq;

namespace Subway.CBTC.THALES.Model
{
    abstract class TargitDistanceScale : ITargitDistanceScale
    {
        protected double MaxtValue { get; set; }
        protected int TextStep { get; set; }
        protected double ValueStep { get { return MaxtValue / LineCount; } }
        public int LongLineStep { get; set; }
        protected int LineCount { get; set; }
        public ReadOnlyCollection<ITargitDistanceScaleItem> Items { get; private set; }

        public void InitItems()
        {
            var cont = Enumerable.Range(0, LineCount + 1).Select(s => new TargitDistanceScaleItem(s * ValueStep, GetDgreeLenth(s * ValueStep), GetLocation(s * ValueStep), GetDistanceText(s * ValueStep))).ToList();
            //foreach (var it in cont.Where(w => (int)w.Value % LongLineStep == 0))
            //{
            //    it.DegreeLength = 1;

            //}
            Items = cont.Cast<ITargitDistanceScaleItem>().ToList().AsReadOnly();
        }
        public double GetLocation(double value)
        {
            return value / MaxtValue;
        }

        public virtual string GetDistanceText(double distance)
        {
            if ((int)distance % TextStep == 0)
            {
                if (distance < float.Epsilon || distance == MaxtValue)
                {
                    return string.Empty;
                }
                return distance.ToString("0");
            }
            return string.Empty;
        }

        public abstract double GetDgreeLenth(double distance);

    }
}
