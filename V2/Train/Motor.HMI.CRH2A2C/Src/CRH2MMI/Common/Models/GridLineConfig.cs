using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;
using CommonUtil.Controls.Grid;


namespace CRH2MMI.Common.Models
{
    /// <summary>
    /// GridLineConfig
    /// </summary>
    [XmlType("Grid")]
    public class GridConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Name { set; get; }

        [XmlAttribute]
        public GridType GridType { set; get; }

        /// <summary>
        /// X
        /// </summary>
        [XmlAttribute]
        public int AbsX { set; get; }


        [XmlAttribute]
        public int AbsY { set; get; }
        /// <summary>
        /// Width
        /// </summary>
        [XmlAttribute]
        public int Width { set; get; }

        /// <summary>
        /// Height
        /// </summary>
        [XmlAttribute]
        public int Height { set; get; }

        /// <summary>
        /// 行数
        /// </summary>
        [XmlAttribute]
        public int RowCount { set; get; }

        /// <summary>
        /// 列数
        /// </summary>
        [XmlAttribute]
        public int ColumnCount { set; get; }


        /// <summary>
        /// 文本的高度
        /// </summary>
        [XmlAttribute]
        public int TextWidth { set; get; }

        [XmlAttribute]
        public int TextHeight { set; get; }

        /// <summary>
        /// 圆的半径
        /// </summary>
        [XmlAttribute]
        public int RoundnessR { set; get; }

        /// <summary>
        /// 网格是否可见
        /// </summary>
        [XmlAttribute]
        public bool LineVisible { set; get; }

        [XmlArray]
        public List<GridRowConfig> Rows { get; set; }

        /// <summary>
        /// 刷新关系 
        /// </summary>
        public void RefreshRelation()
        {
            foreach (var row in Rows)
            {
                row.Parent = this;
                row.RefreshParentOfChildren();
            }
        }

        /// <summary>
        /// 替换特殊字符
        /// </summary>
        public void RelaceSpecialString()
        {
            if (!Name.IsNullOrWhiteSpace())
            {
                Name = Name.Replace("\\r\\n", "\r\n");
            }
            Rows.ForEach(e =>
            {
                if (!e.Name.IsNullOrWhiteSpace())
                {
                    e.Name = e.Name.Replace("\\r\\n", "\r\n");
                }
            });
        }

        /// <summary>
        /// 刷新子结点的父结点
        /// </summary>
        public void RefreshParentOfChildren()
        {
            foreach (var row in Rows)
            {
                row.Parent = this;
            }
        }
    }

    /// <summary>
    /// gridline 行元素的配置
    /// </summary>
    [XmlType("Row")]
    [DebuggerDisplay("RowIndex = {RowIndex}, ItemType = {ItemType}, Name = {Name}")]
    public class GridRowConfig
    {
        public GridRowConfig()
        {
            Texts = new List<string>();
            Colors = new List<Color>();
            TextBkColors = new List<Color>();
        }

        /// <summary>
        /// 父结点, 整个GridLine
        /// </summary>
        [XmlIgnore]
        public GridConfig Parent { set; get; }

        /// <summary>
        /// 行名
        /// </summary>
        [XmlAttribute]
        public string Name { set; get; }

        /// <summary>
        /// 行
        /// </summary>
        [XmlAttribute]
        public int RowIndex { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [XmlAttribute]
        public GridItemType ItemType { set; get; }

        /// <summary>
        /// 如果 ItemType == GridItemType.Text, 此值表示 text的值列表
        /// </summary>
        [XmlIgnore]
        public List<string> Texts { set; get; }

        [XmlAttribute("Texts")]
        public string TextsAttr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    Texts = value.Split(',').ToList();
                }
            }
            get { return string.Join(",", Texts.ToArray()); }
        }

        /// <summary>
        /// 如果 ItemType == GridItemType.Text, 此值表示 Text 文本的颜色
        /// 如果 ItemType == GridItemType.Round, 此值表示 圆的颜色
        /// </summary>
        [XmlIgnore]
        public List<Color> Colors { set; get; }

        [XmlAttribute("Colors")]
        public string ColorsAttr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    Colors = value.Split(',').Select(ColorTranslator.FromHtml).ToList();
                }
            }
            get { return string.Join(",", Colors.Select(ColorTranslator.ToHtml).ToArray()); }
        }


        /// <summary>
        /// 如果 ItemType == GridItemType.Text, 此值表示 Text 背景 的颜色
        /// 如果 ItemType == GridItemType.Round, 此值 无意义 
        /// </summary>
        [XmlIgnore]
        public List<Color> TextBkColors { set; get; }

        [XmlAttribute("TextBkColors")]
        public string TextBkColorsAttr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    TextBkColors = value.Split(',').Select(ColorTranslator.FromHtml).ToList();
                }
            }
            get { return string.Join(",", TextBkColors.Select(ColorTranslator.ToHtml).ToArray()); }
        }

        [XmlElement("Columns")]
        public List<GridColumnConfig> ColumnConfigs { get; set; }

        /// <summary>
        /// 如果是 InFloatList,  指定的Tostring 格式.
        /// </summary>
        [XmlAttribute]
        public string FloatToStringFormat { set; get; }

        /// <summary>
        /// 刷新子结点的父结点
        /// </summary>
        public void RefreshParentOfChildren()
        {
            foreach (var columnConfig in ColumnConfigs)
            {
                columnConfig.Parent = this;
            }
        }

    }

    [XmlType("Column")]
    public class GridColumnConfig : CRH2CommunicationPortModel
    {
        public GridColumnConfig()
        {
            InBoolList = new List<int>();
            OutBoolList = new List<int>();
            InFloatList = new List<int>();
            OutFloatList = new List<int>();
            ColumSpan = 1;
        }

        /// <summary>
        /// 父结点 , 行
        /// </summary>
        [XmlIgnore]
        public GridRowConfig Parent { set; get; }

        /// <summary>
        /// 列
        /// </summary>
        [XmlAttribute]
        public int ColumIndex { set; get; }

        [XmlAttribute]
        public int ColumSpan { set; get; }

        [XmlIgnore]
        public List<int> InBoolList { set; get; }

        [XmlAttribute("InBoolList")]
        public string InBoolListAttr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    InBoolList = value.Split(',').Select(DynamicInvokeHelper.DynamicInvoke<int>).ToList();
                }
            }
            get { return string.Join(",", InBoolList.Select(s => s.ToString()).ToArray()); }
        }


        [XmlIgnore]
        public List<int> OutBoolList { set; get; }

        [XmlAttribute("OutBoolList")]
        public string OutBoolListAttr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    OutBoolList = value.Split(',').Select(DynamicInvokeHelper.DynamicInvoke<int>).ToList();
                }
            }
            get { return string.Join(",", OutBoolList.Select(s => s.ToString()).ToArray()); }
        }


        [XmlIgnore]
        public List<int> InFloatList { set; get; }

        [XmlAttribute("InFloatList")]
        public string InFloatListAttr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    InFloatList = value.Split(',').Select(DynamicInvokeHelper.DynamicInvoke<int>).ToList();
                }
            }
            get { return string.Join(",", InFloatList.Select(s => s.ToString()).ToArray()); }
        }


        [XmlIgnore]
        public List<int> OutFloatList { set; get; }

        [XmlAttribute("OutFloatList")]
        public string OutFloatListAttr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    OutFloatList = value.Split(',').Select(DynamicInvokeHelper.DynamicInvoke<int>).ToList();
                }
            }
            get { return string.Join(",", OutFloatList.Select(s => s.ToString()).ToArray()); }
        }

    }


    /// <summary>
    /// gridline元素的配置
    /// </summary>
    [XmlType("IntersectionItem")]
    [XmlInclude(typeof(GridLineItemData))]
    //[XmlInclude(typeof(BPGridLineItemData))]
    public class GridLineItemConifg
    {
        /// <summary>
        /// 行
        /// </summary>
        [XmlAttribute]
        public int RowIndex { set; get; }

        /// <summary>
        /// 列
        /// </summary>
        [XmlAttribute]
        public int ColumIndex { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [XmlAttribute]
        public GridItemType ItemType { set; get; }

    }

    public enum GridType
    {
        GridLine,
        GridCell,
    }
}
