using System;
using System.Collections.Generic;

namespace Motor.ATP._300D.Station
{
    public class ByteStationInfoInterpreter : StationInfoInterpreterBase
    {
        /// <summary>
        /// 解释为资源索引
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override int InterpretedAsResourceIndex(float value)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(value), 0);

        }
    }
}