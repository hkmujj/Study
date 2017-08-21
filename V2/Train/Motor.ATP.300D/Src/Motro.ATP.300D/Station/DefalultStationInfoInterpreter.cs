using System;

namespace Motor.ATP._300D.Station
{
    public class DefalultStationInfoInterpreter : StationInfoInterpreterBase
    {
        /// <summary>
        /// 解释为资源索引
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override int InterpretedAsResourceIndex(float value)
        {
            return Convert.ToInt32(value);
        }
    }
}