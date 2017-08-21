using System;
using System.Collections.Generic;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class SpeedProfile :  ISpeedProfile
    {
        public List<Tuple<double, double>> PointCollection { get; set; }
    }
}
