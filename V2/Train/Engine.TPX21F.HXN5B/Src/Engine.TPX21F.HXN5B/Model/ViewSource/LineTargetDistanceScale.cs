using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Engine.TPX21F.HXN5B.Model.Interface;

namespace Engine.TPX21F.HXN5B.Model.ViewSource
{
    public class LineTargetDistanceScale : ITargitDistanceScale
    {
        //public LineTargetDistanceScale()
        //{
        //    MaxValue = 1200;
        //    ValueCount = 1200/40;
        //    LongScalValueStep = 200;
        //    TextValueStep = 400;
        //    ValueStep = MaxValue/ValueCount;
        //    UpdateTargitDistanceScaleItems();
        //}

        //public static readonly LineTargetDistanceScale Instance = new LineTargetDistanceScale();

        public double MaxValue { get; set; }

        public int ValueCount { get; set; }

        public int LongScalValueStep { get; set; }

        public int TextValueStep { get; set; }
        
        public double ValueStep { get { return MaxValue / ValueCount; } }

        protected virtual void UpdateTargitDistanceScaleItems()
        {
            var pen = new Pen(Brushes.White, 2);
            var items =
                Enumerable.Range(0, ValueCount+1)
                    .Select(s => new TargitDistanceScaleItem(s * ValueStep, GetLocatoin(s * ValueStep), pen, 0.5, GetDistanceText(s * ValueStep)))
                    .ToList();
            foreach (var it in items.Where(w => (int) w.Value  % LongScalValueStep == 0))
            {
                it.DegreeLength = 1;
            }

            TargitDistanceScaleItems = items.Cast<ITargitDistanceScaleItem>().ToList().AsReadOnly();
        }

        public ReadOnlyCollection<ITargitDistanceScaleItem> TargitDistanceScaleItems { get; private set; }

        public virtual double GetLocatoin(double value)
        {
            return value / MaxValue;
        }

        public virtual string GetDistanceText(double distance)
        {
            // 0.5µÄÕû±¶Êý 
            if ((int)distance % TextValueStep == 0)
            {
                if (Math.Abs(distance) < float.Epsilon)
                {
                    return "0";
                }
                return distance.ToString("0");
            }

            return string.Empty;
        }
    }
}