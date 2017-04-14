using System.Xml.Serialization;

namespace MMITool.Common.Model
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRectangleF
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        float X { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        float Y { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        float Width { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        float Height { set; get; }
    }
}
