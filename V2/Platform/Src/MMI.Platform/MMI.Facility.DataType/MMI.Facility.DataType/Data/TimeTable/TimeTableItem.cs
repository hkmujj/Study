using System.Xml.Serialization;
using MMI.Facility.Interface.Data.TimeTable;

namespace MMI.Facility.DataType.Data.TimeTable
{
    /// <summary>
    /// ʱ�̱�
    /// </summary>
    public class TimeTableItem : ITimeTableItem
    {
        /// <summary>
        /// վ��ID
        /// </summary>
        [XmlAttribute("ID")]
        public string ID { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [XmlAttribute("��վʱ��")]
        public string DepartTime { get; set; }
        /// <summary>
        /// ��վʱ��
        /// </summary>
        [XmlAttribute("��վʱ��")]
        public string ArriveTime { get; set; }
    }
}