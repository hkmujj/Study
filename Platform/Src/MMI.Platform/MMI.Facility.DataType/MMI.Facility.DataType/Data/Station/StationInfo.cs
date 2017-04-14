using System.Collections.Generic;
using System.Diagnostics;
using MMI.Facility.Interface.Data.Station;

namespace MMI.Facility.DataType.Data.Station
{
    [DebuggerDisplay("Index={Index},Name={Name}")]
    public class StationInfo : IStationInfo, IEqualityComparer<StationInfo>
    {
        [DebuggerStepThrough]
        public StationInfo(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public static readonly StationInfo Comparer = new StationInfo(0, null);

        /// <summary>
        /// 车站索引
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// 车站名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>确定指定的对象是否相等。</summary>
        /// <returns>如果指定的对象相等，则为 true；否则为 false。</returns>
        /// <param name="x">要比较的第一个类型为 <paramref name="T" /> 的对象。</param>
        /// <param name="y">要比较的第二个类型为 <paramref name="T" /> 的对象。</param>
        public bool Equals(StationInfo x, StationInfo y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Index == y.Index;
        }

        /// <summary>返回指定对象的哈希代码。</summary>
        /// <returns>指定对象的哈希代码。</returns>
        /// <param name="obj">
        /// <see cref="T:System.Object" />，将为其返回哈希代码。</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="obj" /> 的类型为引用类型，<paramref name="obj" /> 为 null。</exception>
        public int GetHashCode(StationInfo obj)
        {
            return obj.Index.GetHashCode();
        }
    }
}