using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Controls.Grid;
using CommonUtil.Util;
using CRH2MMI.Common.View;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.BPRescue
{
    [XmlType("BPRescueView")]
    public class BPConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlElement]
        public BPGridLine BPGridLine { set; get; }

    }

    public class BPGridLine
    {
        [XmlElement("BPViewItem")]
        public List<BPViewItem> ViewItems { set; get; }

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
        /// X
        /// </summary>
        [XmlAttribute]
        public int AbsX { set; get; }

        /// <summary>
        /// Y
        /// </summary>
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

    }

    [Serializable]
    public class BPViewItem : MatrixViewConfig
    {
        [XmlArray]
        [XmlArrayItem("Value")]
        public List<string> ItemValues { set; get; }
    }

    class BPConfigTest
    {
        static public void Test()
        {
            var data = new BPConfig()
            {
                BPGridLine = new BPGridLine
                {
                    ViewItems = new List<BPViewItem>()
                    {
                        new BPViewItem()
                        {
                            ItemValues = new List<string>() {"A"},
                            ItemType = GridItemType.Text,
                            RowIndex = 1,
                            Name = "fdas"
                        },
                        new BPViewItem()
                        {
                            ItemValues = new List<string>() {"A"},
                            ItemType = GridItemType.Text,
                            RowIndex = 1,
                            Name = "aaa"
                        },
                    },

                    AbsY = 12,
                }

            };
            data.BPGridLine.TextHeight = 1;

            DataSerialization.SerializeToXmlFile(data, "d:\\a.xml");
        }
    }
}
