using System.Xml.Serialization;

namespace Motor.ATP._300T.Config
{
    /// <summary>
    /// 速度比例配置
    /// </summary>
    [XmlRoot]
    public class SpeedProportionConfig
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public const string FileName = "SpeedProprrtionConfig.xml";
        /// <summary>
        /// 比例
        /// </summary>
        [XmlElement]
        public float Proportion { get; set; }
    }
}
