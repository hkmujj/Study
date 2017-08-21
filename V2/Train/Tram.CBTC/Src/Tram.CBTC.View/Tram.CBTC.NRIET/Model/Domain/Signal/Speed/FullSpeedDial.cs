using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Signal.Speed;
using Tram.CBTC.NRIET.Model.RegionSpeed;

namespace Tram.CBTC.NRIET.Model.Domain.Signal.Speed
{
    public class FullSpeedDial : NotificationObject, ISpeedDialPlate
    {
        public static readonly FullSpeedDial Instance = new FullSpeedDial();

        private const double AnglePerSpeed = 200d / 8d / 10d;

        private FullSpeedDial()
        {
            ItemCollection = new ObservableCollection<DegreeScaleItem>()
            {
            };

            //每个大刻度所占的角度值
            const int lStep = (100 - (-100)) / 8;

            AddDegree(-100, lStep, 0, 10, 8);

            //最后1个速度
            ItemCollection.Add(new DegreeScaleItem() { Angle = 100, Lenght = 25, Text = "80", Value = 80 });

            var order = ItemCollection.OrderBy(o => o.Value).ToList();
            ItemCollection.Clear();
            ItemCollection.AddRange(order);
            var startAngle = ItemCollection.First().Angle;
            foreach (var it in ItemCollection)
            {
                it.AngleIncrement = it.Angle - startAngle;
            }
        }

        public double ConvertToAngle(double speed)
        {
            return speed * AnglePerSpeed + -100;
        }

        private void AddDegree(int angle, int lStep, int startValue, int valueStep, int lCount)
        {
            int sStep = lStep / 5;
            for (int i = 0; i < lCount; i++, angle += lStep)
            {
                var value = startValue + i * valueStep;

                ItemCollection.Add(new DegreeScaleItem() { Angle = angle, Lenght = 25, Text = value.ToString(), Value = value });

                var sAngle = angle;

                for (int j = 1; j < 5; j++)
                {
                    sAngle += sStep;
                    var sValue = value + valueStep / 5 * j;
                    ItemCollection.Add(new DegreeScaleItem() { Angle = sAngle, Lenght = 15, Text = string.Empty, Value = sValue });
                }
            }
        }

        public ObservableCollection<DegreeScaleItem> ItemCollection { private set; get; }
        public SpeedDialPlateDegree MinDegree { get; private set; }
        public SpeedDialPlateDegree MaxDegree { get; private set; }
        public ReadOnlyCollection<SpeedDialPlateDegree> AllSpeedDegrees { get; private set; }
        public float ConvertSpeedToAngle(float speed)
        {
            return (float)ConvertToAngle(Math.Abs(speed) > 80 ? 80 : Math.Abs(speed));
        }

        public float ConvertSpeedToDrawArcAngle(float speed)
        {
            throw new System.NotImplementedException();
        }
    }
}