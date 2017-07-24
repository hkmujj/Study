using System;
using System.Diagnostics;

namespace MMI.Facility.Interface.Event
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateStationCollectionEventArgs : EventArgs
    {
        [DebuggerStepThrough]
        public UpdateStationCollectionEventArgs(string stations)
        {
            Stations = stations;
        }


        /// <summary>
        /// 车站集合字符 格式： 索引，车站;索引，车站;
        /// </summary>
        public string Stations { private set; get; }
    }

    /// <summary>
    /// 时间列表事件参数
    /// </summary>
    public class TimeTableEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="date"></param>
        public TimeTableEventArgs(byte[] date)
        {
            Date = date;
        }
        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Date { get; private set; }
    }
}