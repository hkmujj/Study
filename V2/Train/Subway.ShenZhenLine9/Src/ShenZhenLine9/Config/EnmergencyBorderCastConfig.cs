using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subway.ShenZhenLine9.Config
{

    /// <summary>
    /// 
    /// </summary>
    public class EnmergencyBorderCastConfig
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public const string File = "BorderCast.xml";
        /// <summary>
        /// 所有紧急广播
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Item")]
        public List<BorderCastItem> BorderCastItems { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BorderCastItem : IComparer<BorderCastItem>, IComparable<BorderCastItem>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [XmlAttribute]
        public int Index { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// 比较两个对象并返回一个值，指示一个对象是小于、等于还是大于另一个对象。
        /// </summary>
        /// <returns>
        /// 一个带符号整数，它指示 <paramref name="x"/> 与 <paramref name="y"/> 的相对值，如下表所示。值含义小于零<paramref name="x"/> 小于 <paramref name="y"/>。零<paramref name="x"/> 等于 <paramref name="y"/>。大于零<paramref name="x"/> 大于 <paramref name="y"/>。
        /// </returns>
        /// <param name="x">要比较的第一个对象。</param><param name="y">要比较的第二个对象。</param>
        public int Compare(BorderCastItem x, BorderCastItem y)
        {
            if (x != null && y != null)
            {
                return x.Index - y.Index;
            }
            throw new ArgumentNullException($"{nameof(x)} Or {nameof(y)} is Null");
        }

        /// <summary>
        /// 比较当前对象和同一类型的另一对象。
        /// </summary>
        /// <returns>
        /// 一个值，指示要比较的对象的相对顺序。返回值的含义如下：值含义小于零此对象小于 <paramref name="other"/> 参数。零此对象等于 <paramref name="other"/>。大于零此对象大于 <paramref name="other"/>。
        /// </returns>
        /// <param name="other">与此对象进行比较的对象。</param>
        public int CompareTo(BorderCastItem other)
        {
            if (other == null)
            {
                throw new ArgumentNullException($"{nameof(other)} is  not null!");
            }
            return Index- other.Index;
        }
    }
}