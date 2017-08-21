using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CBTC.Infrasturcture.Model.Signal.Warning;

namespace Subway.CBTC.Casco.Model.Domain.Signal.Warning
{
    public class TagetDistanceScale : ITargitDistanceScale
    {
        private List<double> Values;
        public TagetDistanceScale()
        {
            Values = GetValues().ToList();

            var tmp = Values.Select(s => new TargitDistanceScaleItem(s, GetScaleLocation(s), 0.6, GetDistanceText(s)));



            TargitDistanceScaleItems = tmp.ToList().AsReadOnly();
        }
        public static TagetDistanceScale Instance = new TagetDistanceScale();
        private IEnumerable<double> GetValues()
        {
            var list = new List<double>();
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(5);
            list.Add(10);
            list.Add(20);
            list.Add(50);
            list.Add(100);
            list.Add(200);
            list.Add(500);
            list.Add(1000);
            return list;
        }
        public ReadOnlyCollection<TargitDistanceScaleItem> TargitDistanceScaleItems { get; private set; }
        public double GetLocatoin(double value)
        {

            var doublVlaue = (double)value;
            for (int i = 1; i < Values.Count; ++i)
            {
                if (doublVlaue >= Values[i - 1] && doublVlaue < Values[i])
                {
                    return 0.1 * (i - 1) + (doublVlaue - Values[i - 1]) / (Values[i] - Values[i - 1]) * 0.1;
                }
            }
            return 1;

        }

        private double GetScaleLocation(double value)
        {
            return (double)Values.IndexOf(value) / (double)Values.Count;
        }

        public string GetDistanceText(double distance)
        {
            return distance.ToString("F0");
        }
    }
}
