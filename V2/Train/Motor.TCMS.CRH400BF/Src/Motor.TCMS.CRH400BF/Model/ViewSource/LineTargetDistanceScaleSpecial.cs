using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Motor.TCMS.CRH400BF.Model.Interface;

namespace Motor.TCMS.CRH400BF.Model.ViewSource
{
    public class LineTargetDistanceScaleSpecial : ITargitDistanceScale
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

        public int MaxValue { get; set; }

        public int ValueCount { get; set; }

        public int LongScalValueStep { get; set; }

        public int MinValue { get; set; }//自己

        public int TextValueStep { get; set; }

        public int ValueStep { get { return (MaxValue-MinValue) / ValueCount; } }

        protected virtual void UpdateTargitDistanceScaleItems()
        {
            var pen = new Pen(Brushes.White, 1);
            var items =
                Enumerable.Range(0, ValueCount + 1)
                    .Select(s => new TargitDistanceScaleItem(s * ValueStep, GetLocatoin(s * ValueStep), pen, 0.5, GetDistanceText(s * ValueStep)))
                    .ToList();
            foreach (var it in items.Where(w => ((int)w.Value+ MinValue) % LongScalValueStep == 0))
            {
                
                it.DegreeLength = 1;
            }

            TargitDistanceScaleItems = items.Cast<ITargitDistanceScaleItem>().ToList().AsReadOnly();
           
        }

        public ReadOnlyCollection<ITargitDistanceScaleItem> TargitDistanceScaleItems { get; private set; }

        public virtual double GetLocatoin(double value)
        {
            return value / (MaxValue-MinValue);
        }

        public virtual string GetDistanceText(double distance)
        {
            // 0.5的整倍数 
            if (((int)distance+ MinValue ) % TextValueStep == 0)
            {
                if (Math.Abs(distance+ MinValue) < float.Epsilon)
                {
                    return "0";
                }
                return (distance+18).ToString("0");
            }

            return string.Empty;
        }
    }
}
