using System.Collections.Generic;
using System.Collections.ObjectModel;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class DefaultSpeedDialPlate : ISpeedDialPlate
    {
        public ReadOnlyCollection<ISpeedDialPlateDegree> AllSpeedDegrees { get; private set; }

        public DefaultSpeedDialPlate()
        {
            var allDegrees = new List<ISpeedDialPlateDegree>();
            AllSpeedDegrees = new ReadOnlyCollection<ISpeedDialPlateDegree>(allDegrees);


            for (var i = 0; i < 300; i += 10)
            {
                allDegrees.Add(new SpeedDialPlateDegree(0, i, i % 20 == 0 ? 10 : 30, i % 20 == 0 ? null : i.ToString()));
            }
        }

        public float ConvertSpeedToAngle(float speed)
        {
            return speed;
        }

        public float ConvertSpeedToDrawArcAngle(float speed)
        {
            return ConvertSpeedToAngle(speed) - 180;
        }
    }
}