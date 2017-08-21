using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CommonUtil.Util
{  /// <summary>
    /// 可xml序列化的list, 序列化为 item1,item2, 写入 content
    /// </summary>
    public class XmlContentList<T> : List<T>, IXmlSerializable
    {
        static XmlContentList()
        {
            // TODO 
            XmlContentList<string>.Convertor = s => s;
            XmlContentList<int>.Convertor = Convert.ToInt32;
            XmlContentList<float>.Convertor = Convert.ToSingle;
            XmlContentList<double>.Convertor = Convert.ToDouble;
        }

        /// <summary>
        ///  string -> T 的转换器
        /// </summary>
        public static Func<string, T> Convertor { get; set; }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected string ReadNextHasValueElememt(XmlReader reader)
        {
            while (reader.NodeType == XmlNodeType.Element)
            {
                string name = reader.Name;
                reader.Read();
                if (reader.NodeType == XmlNodeType.Text)
                {
                    return name;
                }

                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected string ReadNextElememt(XmlReader reader)
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                string name = reader.Name;
                reader.Read();
                if (reader.NodeType == XmlNodeType.Text)
                {
                    return name;
                }

                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// 从对象的 XML 表示形式生成该对象。
        /// </summary>
        /// <param name="reader">对象从中进行反序列化的 <see cref="T:System.Xml.XmlReader"/> 流。</param>
        public virtual void ReadXml(XmlReader reader)
        {
            //while (ReadNextHasValueElememt(reader) != null)
            if (ReadNextElememt(reader) != null)
            {
                this.AddRange(GetListOfType(reader.Value));

                reader.Read();
                // 下一节点 
                reader.Read();

            }
        }

        /// <summary>
        /// 将对象转换为其 XML 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 <see cref="T:System.Xml.XmlWriter"/> 流。</param>
        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteString(string.Join(",", this.Select(s => s.ToString()).ToArray()));
        }

        private IEnumerable<T> GetListOfType(string values)
        {
            return !string.IsNullOrEmpty(values) ? values.Split(',').Select(Convertor).ToList() : new List<T>();
        }

        /// <summary>
        /// 此方法是保留方法，请不要使用。在实现 IXmlSerializable 接口时，应从此方法返回 null（在 Visual Basic 中为 Nothing），如果需要指定自定义架构，应向该类应用 <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/>。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Xml.Schema.XmlSchema"/>，描述由 <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> 方法产生并由 <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> 方法使用的对象的 XML 表示形式。
        /// </returns>
        public virtual XmlSchema GetSchema()
        {
            return null;
        }

    }

    static class XmlSerializationListTest
    {
        public static void Test()
        {
            var ls = new XmlContentList<int>() { 1, 2, 3 };

            //DataSerialization.SerializeToXmlFile(ls, "D:\\a.xml", "rot");

            var data = new XmlSerializationListModel()
            {
                Datas = new XmlContentList<int>() {1, 2, 3},
                
                };
            // need make XmlSerializationListModel public to Serialize
            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");


            var ds = DataSerialization.DeserializeFromXmlFile<XmlSerializationListModel>("D:\\a.xml");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("root")]
    public class XmlSerializationListModel 
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public XmlContentList<int> Datas { set; get; }

        //[XmlElement("ff")]
        //public XmlAttributeList<int> Datas2 { set; get; } 
    }
}