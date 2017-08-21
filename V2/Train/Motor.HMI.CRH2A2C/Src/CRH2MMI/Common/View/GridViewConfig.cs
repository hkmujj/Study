using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CRH2MMI.Common.Models;
using CommonUtil.Controls.Grid;


namespace CRH2MMI.Common.View
{
    /// <summary>
    /// 矩阵视图配置
    /// </summary>
    [Serializable]
    public class MatrixViewConfig
    {
        /// <summary>
        /// 名字
        /// </summary>
        [XmlAttribute]
        public string Name { set; get; }

        /// <summary>
        /// X 坐标
        /// </summary>
        [XmlAttribute]
        public int RowIndex { set; get; }

        /// <summary>
        /// Y 坐标
        /// </summary>
        [XmlAttribute]
        public int ColumnIndex { set; get; }

        /// <summary>
        /// 区域信息
        /// </summary>
        [XmlAttribute]
        public int Region { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [XmlAttribute]
        public GridItemType ItemType { set; get; }


        /// <summary>
        /// 矩形框的内容 
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Item")]
        public List<MatrixViewConfigItem> Items { set; get; }

    }

}
