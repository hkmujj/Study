using System.Collections.Generic;
using System.Xml.Serialization;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Configs
{
    /// <summary>
    /// 门优先级配置
    /// </summary>
    public class DoorPriorityConfig
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public const string File = "DoorPriorityConfig.xml";
        /// <summary>
        /// 所有状态优先级集合
        /// </summary>
        [XmlElement]
        public List<DoorPriority> DoorPriorities { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DoorPriority
    {
        /// <summary>
        /// 门状态
        /// </summary>
        [XmlAttribute]
        public DoorState State { get; set; }

        /// <summary>
        /// 优先级  值越小 优先级越高
        /// </summary>
        [XmlAttribute]
        public int Priority { get; set; }
    }
}