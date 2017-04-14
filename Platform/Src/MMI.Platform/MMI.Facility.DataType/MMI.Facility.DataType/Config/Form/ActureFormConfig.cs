using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Model;
using CommonUtil.Util;
using MMI.Facility.DataType.Attributes;
using MMI.Facility.Interface.Data.Config.Form;

namespace MMI.Facility.DataType.Config.Form
{
    [ConfigureDescription("屏的位置配置", FileName)]
    public class ActureFormConfig : XmlRectangle, IActureFormConfig
    {
        public ActureFormConfig()
        {
            DesignX = 0;
            DesignY = 0;
            DesignWidth = 800;
            DesignHeight = 600;
        }

        private bool m_TopMost;

        [XmlIgnore] public const string FileName = "ActureFormConfig.xml";

        private static void Test()
        {
            var d = new ActureFormConfig() {Rectangle = new Rectangle(0, 0, 800, 600)};
            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }

        public float DesignX { get; set; }

        public float DesignY { get; set; }

        public float DesignWidth { set; get; }

        public float DesignHeight { set; get; }

        /// <summary>
        /// 是否显示鼠标
        /// </summary>
        public bool IsCourseVisible { set; get; }

        /// <summary>
        /// 外部边框是否可见
        /// </summary>
        public bool IsOutterFrameVisible { set; get; }

        [XmlAttribute]
        public bool TopMost
        {
            get { return m_TopMost; }
            set
            {
                if (value.Equals(m_TopMost))
                {
                    return;
                }

                m_TopMost = value;
                OnPropertyChanged("TopMost");
            }
        }
    }
}