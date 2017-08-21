using LightRail.HMI.SZLHLF.Interface;
using System;
using System.Linq;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace LightRail.HMI.SZLHLF.Model.ViewSource
{
    public class MainMax100Scale : LineTargetDistanceScale
    {
        public MainMax100Scale()
        {
            MaxValue = 100;
            ValueCount = (int)(MaxValue / 12.5);
            LongScalValueStep = 25;
            TextValueStep = 25;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainMax100Scale Instance = new MainMax100Scale();
    }

    public class LineTargetDistanceScale : ITargitDistanceScale
    {

        public double MaxValue { get; set; }

        public int ValueCount { get; set; }

        public int LongScalValueStep { get; set; }

        public int TextValueStep { get; set; }

        public double ValueStep { get { return MaxValue / ValueCount; } }

        protected virtual void UpdateTargitDistanceScaleItems()
        {
            var pen = new Pen(Brushes.White, 2);
            var items =
                Enumerable.Range(0, ValueCount + 1)
                    .Select(s => new TargitDistanceScaleItem(s * ValueStep, GetLocatoin(s * ValueStep), pen, 0.5, GetDistanceText(s * ValueStep)))
                    .ToList();
            foreach (var it in items.Where(w => (int)w.Value % LongScalValueStep == 0))
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
            // 0.5的整倍数 
            if ((int)distance % TextValueStep == 0)
            {
                if (Math.Abs(distance) < float.Epsilon)
                {
                    return string.Empty;
                }
                return distance.ToString("0");
            }

            return string.Empty;
        }

        //public virtual string GetDistanceText1(double distance)
        //{
        //    // 0.5的整倍数 
        //    if ((int)distance % TextValueStep == 0)
        //    {
        //        if (Math.Abs(distance) < float.Epsilon)
        //        {
        //            return string.Empty;
        //        }
        //        return distance.ToString("0");
        //    }

        //    return string.Empty;
        //}
   }
}