using System.Xml.Serialization;
using MMI.Facility.Interface.Data.TimeTable;

namespace MMI.Facility.DataType.Data.TimeTable
{
    /// <summary>
    /// 时刻表
    /// </summary>
    public class TimeTableItem : ITimeTableItem
    {
        /// <summary>
        /// 站点ID
        /// </summary>
        [XmlAttribute("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 发车时间
        /// </summary>
        [XmlAttribute("离站时间")]
        public string DepartTime { get; set; }
        /// <summary>
        /// 到站时间
        /// </summary>
        [XmlAttribute("到站时间")]
        public string ArriveTime { get; set; }
    }
}