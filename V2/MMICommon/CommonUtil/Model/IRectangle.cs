using System.Xml.Serialization;

namespace CommonUtil.Model
{
    /// <summary>
    /// IRectangle
    /// </summary>
    public interface IRectangle
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        int X { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        int Y { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        int Width { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        int Height { set; get; }
    }
}
