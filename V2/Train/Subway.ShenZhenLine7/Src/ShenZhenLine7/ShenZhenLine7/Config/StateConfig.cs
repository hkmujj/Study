using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class StateConfig
    {
        /// <summary>
        /// 状态Key
        /// </summary>
        [XmlAttribute]
        public string Key { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn01 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn02 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn03 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn04 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn05 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn06 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn07 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn08 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn09 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        [XmlElement]
        public BtnItemConfig Btn10 { get; set; }
        /// <summary>
        /// 视图名称
        /// </summary>
        [XmlAttribute]
        public string View { get; set; }

        /// <summary>
        /// 标题名称
        /// </summary>
        [XmlAttribute]
        public string TitleName { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class BtnItemConfig
    {
        /// <summary>
        /// 按钮文字
        /// </summary>
        [XmlAttribute]
        public string Text { get; set; }

        /// <summary>
        /// 响应类
        /// </summary>
        [XmlIgnore]
        public IBtnResponse Response { get; private set; }


        /// <summary>
        /// 响应类名称
        /// </summary>
        [XmlAttribute]
        public string ResponseName
        {
            get { return m_ResponseName; }
            set
            {
                m_ResponseName = value;
                if (!string.IsNullOrEmpty(value))
                {
                    var fullName = $"Subway.ShenZhenLine7.Controller.BtnResponse.{value}";
                    var s1 = Type.GetType(fullName);
                    var s2 = s1.Assembly;
                    var s3 = s2.CreateInstance(s1.FullName) as IBtnResponse;
                    //var s = fullName.GetType()?.Assembly.CreateInstance(fullName) as IBtnResponse;
                    Response = s3;
                }

            }
        }
        private string m_ResponseName;
    }
    /// <summary>
    /// 
    /// </summary>
    public class BtnConfig
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public const string File = "StateKeyConfigs.xml";
        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        public List<StateConfig> AllSatteConfig { get; set; }
    }

}