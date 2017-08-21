using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Station.Door
{
    class StationDoorLocation 
    {
        /// <summary>
        /// 是否在上面
        /// </summary>
        public bool IsUp { set; get; }

        /// <summary>
        /// 列车号 ， 从0 开始
        /// </summary>
        public uint CarNo { set; get; }

        public override int GetHashCode()
        {
            return (int) ( GlobalParam.CarCount * ( IsUp ? 0 : 1 ) + CarNo );
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", IsUp ? "Up" : "Down", CarNo + 1);
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }
    }
}
