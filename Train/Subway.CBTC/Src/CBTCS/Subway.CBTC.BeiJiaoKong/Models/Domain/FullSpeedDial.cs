using System;
using Microsoft.Practices.Prism;
using Subway.CBTC.BeiJiaoKong.Models.RegionB;
using System.Collections.ObjectModel;
using System.Linq;
using CBTC.Infrasturcture.Model.Signal.Speed;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.CBTC.BeiJiaoKong.Models.Domain
{
    public class FullSpeedDial : NotificationObject, ISpeedDialPlate
    {
        public static readonly FullSpeedDial Instance = new FullSpeedDial();

        private const double AnglePerSpeed = 28d / 10d;

        private FullSpeedDial()
        {
            ItemCollection = new ObservableCollection<DegreeScaleItem>()
            {
            };

            const int lStep = 28;

            AddDegree(-154, lStep, 0, 10, 11);

            ItemCollection.Add(new DegreeScaleItem() { Angle = 154, Lenght = 25, Text = "110", Value = 110 });

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
            return speed * AnglePerSpeed + -154;
        }

        private void AddDegree(int angle, int lStep, int startValue, int valueStep, int lCount)
        {
            int sStep = lStep / 2;
            for (int i = 0; i < lCount; i++, angle += lStep)
            {
                var value = startValue + i * valueStep;

                ItemCollection.Add(new DegreeScaleItem() { Angle = angle, Lenght = 25, Text = value.ToString(), Value = value });

                var sAngle = angle;

                for (int j = 1; j < 2; j++)
                {
                    sAngle += sStep;
                    var sValue = value + valueStep / 2 * j;
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
            return (float)ConvertToAngle(Math.Abs(speed) > 110 ? 110 : Math.Abs(speed));
        }

        public float ConvertSpeedToDrawArcAngle(float speed)
        {
            throw new System.NotImplementedException();
        }
    }
}