using System.Collections.Generic;
using System.Xml.Serialization;

namespace Excel.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExcelReaderConfig
    {
        /// <summary>
        /// 文件
        /// </summary>
        string File { get; }

        /// <summary>
        ///  sheet
        /// </summary>
        List<string> SheetNames {  get; }

        /// <summary>
        /// 
        /// </summary>
        List<ColoumnConfig> Coloumns { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRoot]
    public class ExcelReaderConfig : IExcelReaderConfig
    {
        /// <summary>
        /// 文件
        /// </summary>
        [XmlElement]
        public string File { set; get; }

        /// <summary>
        ///  sheet
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Sheet")]
        public List<string> SheetNames { set; get; } 

        /// <summary>
        /// 
        /// </summary>
        [XmlArray("ReadColoumns")]
        [XmlArrayItem("Coloumn")]
        public List<ColoumnConfig> Coloumns { set; get; }


    }

    /// <summary>
    /// 列配置
    /// </summary>
    public class ColoumnConfig
    {
        /// <summary>
        /// 是否为主键
        /// </summary>
        [XmlAttribute]
        public bool IsPrimaryKey { set; get; }

        /// <summary>
        /// 列名
        /// </summary>
        [XmlAttribute]
        public string Name { set; get; }
    }

}