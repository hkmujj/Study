using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.Roller
{
    public class TemperatureStateAdpt
    {
        public static TemperatureState GeTemperatureState(float[] temperatures, float currenTem)
        {
            if (temperatures == null || temperatures.Length < 9)
            {
                return TemperatureState.Normal;

            }
            if (temperatures[8] > 30)//环境温度高于30
            {
                if (currenTem > 90)
                {
                    return TemperatureState.High;
                }
                return TemperatureState.Normal;
            }
            if (temperatures[8] < -10)
            {
                if (currenTem > 50)
                {
                    return TemperatureState.High;
                }
                return TemperatureState.Normal;
            }
            if (currenTem - temperatures[8] > 60)
            {
                return TemperatureState.High;
            }
            return TemperatureState.Normal;
        }
    }
}
