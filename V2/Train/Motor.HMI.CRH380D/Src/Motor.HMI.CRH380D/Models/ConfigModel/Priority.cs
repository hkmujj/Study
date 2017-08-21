using System.Collections.Generic;
using System.Xml.Serialization;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.ConfigModel
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
        /// 外门
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Door")]
        public List<Priority<DoorState, int>> DoorPriorities { get; set; }

        /// <summary>
        /// 受电弓
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Pantograph")]
        public List<Priority<PantographState, int>> PantographPriorities { get; set; }

        /// <summary>
        /// LCB
        /// </summary>
        [XmlArray]
        [XmlArrayItem("LCB")]
        public List<Priority<LCBState, int>> LCBPriorities { get; set; }

        /// <summary>
        /// 高速断路器
        /// </summary>
        [XmlArray]
        [XmlArrayItem("QuickBreak")]
        public List<Priority<QuickBreakState, int>> QuickBreakPriorities { get; set; }

        /// <summary>
        /// 接地开关
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Grounding")]
        public List<Priority<GroundingState, int>> GroundingPriorities { get; set; }

        /// <summary>
        /// ACM
        /// </summary>
        [XmlArray]
        [XmlArrayItem("ACM")]
        public List<Priority<ACMState, int>> ACMPriorities { get; set; }

        /// <summary>
        /// LCM
        /// </summary>
        [XmlArray]
        [XmlArrayItem("LCM")]
        public List<Priority<LCMState, int>> LCMPriorities { get; set; }

        /// <summary>
        /// MCM
        /// </summary>
        [XmlArray]
        [XmlArrayItem("MCM")]
        public List<Priority<MCMState, int>> MCMPriorities { get; set; }

        /// <summary>
        /// BCU
        /// </summary>
        [XmlArray]
        [XmlArrayItem("BCU")]
        public List<Priority<BCUState, int>> BCUPriorities { get; set; }

        /// <summary>
        /// EmergencyBreak
        /// </summary>
        [XmlArray]
        [XmlArrayItem("EmergencyBreak")]
        public List<Priority<EmergencyBreakState, int>> EmergencyBreakPriorities { get; set; }

        /// <summary>
        /// ParkBreak
        /// </summary>
        [XmlArray]
        [XmlArrayItem("ParkBreak")]
        public List<Priority<ParkBreakState, int>> ParkBreakPriorities { get; set; }

        /// <summary>
        /// CarComfort
        /// </summary>
        [XmlArray]
        [XmlArrayItem("CarComfort")]
        public List<Priority<CarComfortState, int>> CarComfortPriorities { get; set; }

        /// <summary>
        /// PrimaryCompressor
        /// </summary>
        [XmlArray]
        [XmlArrayItem("PrimaryCompressor")]
        public List<Priority<PrimaryCompressorState, int>> PrimaryCompressorPriorities { get; set; }

        /// <summary>
        /// PressureSensor
        /// </summary>
        [XmlArray]
        [XmlArrayItem("PressureSensor")]
        public List<Priority<PressureSensorState, int>> PressureSensorPriorities { get; set; }

        /// <summary>
        /// SubCompressor
        /// </summary>
        [XmlArray]
        [XmlArrayItem("SubCompressor")]
        public List<Priority<SubCompressorState, int>> SubCompressorPriorities { get; set; }

        /// <summary>
        /// 门
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Station")]
        public List<Priority<StationState, int>> StationPriorities { get; set; }

        /// <summary>
        /// 直流供电设备1
        /// </summary>
        [XmlArray]
        [XmlArrayItem("DCDevice1")]
        public List<Priority<DCDevice1State, int>> DCDevice1Priorities { get; set; }

        /// <summary>
        /// 直流供电设备2
        /// </summary>
        [XmlArray]
        [XmlArrayItem("DCDevice2")]
        public List<Priority<DCDevice2State, int>> DCDevice2Priorities { get; set; }

        /// <summary>
        /// 直流供电设备3
        /// </summary>
        [XmlArray]
        [XmlArrayItem("DCDevice3")]
        public List<Priority<DCDevice3State, int>> DCDevice3Priorities { get; set; }
    }
}