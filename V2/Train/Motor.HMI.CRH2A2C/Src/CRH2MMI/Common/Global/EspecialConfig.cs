using System.Xml;
using System.Xml.Serialization;

namespace CRH2MMI.Common.Global
{
    /// <summary>
    /// 特殊配置
    /// </summary>
    [XmlType]
    public class EspecialConfig
    {
        /// <summary>
        /// 速度缩放值
        /// </summary>
        [XmlElement]
        public float SpeedScale { get; set; }
    }
}