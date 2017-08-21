using System.Collections.Generic;
using System.Xml.Serialization;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.Config
{
    /// <summary>
    /// 优先级
    /// </summary>
    public class Priority<TKey, TValue>
    {
        /// <summary>
        /// 优先级Key
        /// </summary>
        [XmlAttribute]
        public TKey Key { get; set; }
        /// <summary>
        /// 优先级 1 最高
        /// </summary>
        [XmlAttribute]
        public TValue Prioritys { get; set; }

        /// <summary>
        /// 克隆方法
        /// </summary>
        /// <returns></returns>
        public Priority<TKey, TValue> Clone()
        {
            var tmp = new Priority<TKey, TValue>
            {
                Key = Key,
                Prioritys = Prioritys
            };
            return tmp;
        }
    }

    /// <summary>
    /// 优先级配置
    /// </summary>
    public class PriorityConfig
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public const string File = "Priority.xml";
        /// <summary>
        /// 门
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Door")]
        public List<Priority<DoorState, int>> DoorPriorities { get; set; }
        /// <summary>
        /// 空调
        /// </summary>
        [XmlArray]
        [XmlArrayItem("AirCondition")]
        public List<Priority<AirConditionState, int>> AirConditionPriorities { get; set; }
        /// <summary>
        /// 辅助电源
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Assist")]
        public List<Priority<AssistState, int>> AssistPriorities { get; set; }
        /// <summary>
        /// 紧急对讲
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Emergency")]
        public List<Priority<EmergencyTalkState, int>> EmergencyTalkPriorities { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Brake")]
        public List<Priority<BrakeState, int>> BrakePriorities { get; set; }
    }
}