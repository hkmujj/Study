using System.Collections.Generic;
using System.Xml.Serialization;
using LightRail.HMI.GZYGDC.Model.Units;

namespace LightRail.HMI.GZYGDC.Config
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
        /// 受电弓单元
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Pantograph")]
        public List<Priority<PantographState, int>> PantographPriorities { get; set; }

        /// <summary>
        /// HSCB高速断路器单元
        /// </summary>
        [XmlArray]
        [XmlArrayItem("HSCB")]
        public List<Priority<HSCBState, int>> HSCBPriorities { get; set; }


        /// <summary>
        /// 电池单元
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Battery")]
        public List<Priority<BatteryState, int>> BatteryPriorities { get; set; }


        /// <summary>
        /// 弹簧单元
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Spring")]
        public List<Priority<SpringState, int>> SpringPriorities { get; set; }


        /// <summary>
        ///　网络拓扑单元
        /// </summary>
        [XmlArray]
        [XmlArrayItem("NetTopology")]
        public List<Priority<NetTopologyState, int>> NetTopologyPriorities { get; set; }
    }
}