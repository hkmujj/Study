using System.Collections.Generic;
using System.Xml.Serialization;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Config
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
        [XmlArrayItem("AirConditionView")]
        public List<Priority<AirConditionState, int>> AirConditionPriorities { get; set; }
        
        /// <summary>
        /// 紧急对讲
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Emergency")]
        public List<Priority<EmergencyTalkState, int>> EmergencyTalkPriorities { get; set; }

        /// <summary>
        /// 制动单元
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Brake")]
        public List<Priority<BrakeState, int>> BrakePriorities { get; set; }

        /// <summary>
        /// 牵引单元
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Traction")]
        public List<Priority<TractionState, int>> TractionPriorities { get; set; }

        /// <summary>
        /// 电池单元
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Battery")]
        public List<Priority<BatteryState, int>> BatteryPriorities { get; set; }
    }
}