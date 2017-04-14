using System.Collections.Generic;
using System.Xml.Serialization;

namespace MMITool.Common.Util.Excel
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot]
    public class ExcelReaderConfig
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
        /// 目标名
        /// </summary>
        [XmlAttribute]
        public string TargetName { set; get; }

        /// <summary>
        /// 列名
        /// </summary>
        [XmlAttribute]
        public string ColoumnName { set; get; }
    }

}