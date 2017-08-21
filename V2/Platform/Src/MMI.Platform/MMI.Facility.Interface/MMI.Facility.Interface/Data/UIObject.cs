using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using ES.Facility.PublicModule.Util;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 图元对象
    /// </summary>
    [DebuggerDisplay("DllName={DllName} ClassName={ClassName} ObjectKey={ObjectKey}")]
    [Serializable]
    public class UIObject : IUIObject
    {

        /// <summary>
        /// 
        /// </summary>
        public UIObject()
        {
            ParaList = new List<string>();
            OutFloatList = new List<int>();
            InFloatList = new List<int>();
            OutBoolList = new List<int>();
            InBoolList = new List<int>();
            Info = new baseObjectInfo();
            ViewList = new List<int>();
        }

        #region ::::::::::::::::::::::: attrible ::::::::::::::::::::::::::

        private string m_ObjectKey;

        /// <summary>
        /// 
        /// </summary>
        public string ObjectKey
        {
            get
            {
                if (string.IsNullOrEmpty(m_ObjectKey))
                {
                    m_ObjectKey = GetObjectKey(ClassName, MainIndex);
                }
                return m_ObjectKey;
            }
        }

        private static string GetObjectKey(string className, int mainIndex)
        {
            return string.Format("{0}_{1}", className, mainIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="mainIndex"></param>
        /// <returns></returns>
        public static string GetObjectKey(string className, string mainIndex)
        {
            return string.Format("{0}_{1}", className, mainIndex);
        }

        /// <summary>
        /// 文件名
        /// </summary>
        [XmlIgnore]
        public string DllName { set; get; }

        /// <summary>
        /// 类名
        /// </summary>
        [XmlAttribute]
        public string ClassName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int MainIndex { set; get; }

        /// <summary>
        /// 原始信息
        /// </summary>
        [XmlIgnore]
        public baseObjectInfo Info { get; set; }

        /// <summary>
        /// 视图列表
        /// </summary>
        [XmlIgnore]
        public List<int> ViewList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ViewList")]
        public XmlSerializationList<int> XmlViewList
        {
            set { ViewList = value; }
            get
            {
                return ViewList as XmlSerializationList<int>;
            }
        }

        /// <summary>
        /// 输入bool列表
        /// </summary>
        [XmlIgnore]
        public List<int> InBoolList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("InBoolList")]
        public XmlSerializationList<int> XmlInBoolList
        {
            set { InBoolList = value; }
            get
            {
                return InBoolList as XmlSerializationList<int>;
            }
        }

        /// <summary>
        /// 输出bool列表
        /// </summary>
        [XmlIgnore]
        public List<int> OutBoolList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("OutBoolList")]
        public XmlSerializationList<int> XmlOutBoolList
        {
            set { OutBoolList = value; }
            get
            {
                return OutBoolList as XmlSerializationList<int>;
            }
        }

        /// <summary>
        /// 输入float列表
        /// </summary>
        [XmlIgnore]
        public List<int> InFloatList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("InFloatList")]
        public XmlSerializationList<int> XmlInFloatList
        {
            set { InFloatList = value; }
            get
            {
                return InFloatList as XmlSerializationList<int>;
            }
        }

        /// <summary>
        /// 输出float列表
        /// </summary>
        [XmlIgnore]
        public List<int> OutFloatList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("OutFloatList")]
        public XmlSerializationList<int> XmlOutFloatList
        {
            set { OutFloatList = value; }
            get
            {
                return OutFloatList as XmlSerializationList<int>;
            }
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        [XmlIgnore]
        public List<string> ParaList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ParaList")]
        public XmlSerializationList<string> XmlParaList
        {
            set { ParaList = value; }
            get
            {
                return ParaList as XmlSerializationList<string>;
            }
        }

        #endregion

        /// <summary>
        /// 创建作为当前实例副本的新对象。
        /// </summary>
        /// <returns>
        /// 作为此实例副本的新对象。
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            return new UIObject()
                   {
                       ClassName = ClassName,
                       DllName = DllName,
                       InBoolList = new List<int>(InBoolList),
                       InFloatList = new List<int>(InFloatList),
                       Info = Info,
                       MainIndex = MainIndex,
                       OutBoolList = new XmlSerializationList<int>(OutBoolList),
                       OutFloatList = new XmlSerializationList<int>(OutFloatList),
                       ViewList = new XmlSerializationList<int>(ViewList),
                       ParaList = new XmlSerializationList<string>(ParaList)
                   };
        }
    }

    class MyClass
    {
        [SuppressMessage("ReSharper", "UnusedVariable")]
        public static void Test()
        {
            var data = new List<UIObject>
            {
                new UIObject()
                {
                    ClassName = "c1",
                    DllName = "c1.dll",
                    XmlInBoolList = new XmlSerializationList<int>() {2, 3, 4},
                    XmlInFloatList = new XmlSerializationList<int>() {3, 6},
                    XmlOutBoolList = new XmlSerializationList<int>() {11, 2},
                    XmlOutFloatList = new XmlSerializationList<int>() {22, 3},
                    XmlParaList = new XmlSerializationList<string>(){"a.bmp, b.bmp"}
                },

                new UIObject()
                {
                    ClassName = "c2",
                    DllName = "c2.dll",
                    InBoolList = new XmlSerializationList<int>() {2, 3, 4},
                    InFloatList = new XmlSerializationList<int>() {3, 6},
                    OutBoolList = new XmlSerializationList<int>() {11, 2},
                    OutFloatList = new XmlSerializationList<int>() {22, 3},
                    XmlParaList = new XmlSerializationList<string>(){"a.bmp, b.bmp"}
                },
            };

            //DataSerialization.SerializeToXmlFile(data, "D:\\a1.xml", "root");

           var data1 = DataSerialization.DeserializeFromXmlFile<List<UIObject>>("D:\\a1.xml", "root") as List<UIObject>;
        }
    }
}
