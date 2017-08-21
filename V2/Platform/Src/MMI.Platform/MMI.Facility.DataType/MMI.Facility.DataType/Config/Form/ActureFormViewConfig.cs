using System.Xml.Serialization;
using MMI.Facility.Interface.Data.Config.Form;

namespace MMI.Facility.DataType.Config.Form
{
    [XmlRoot]
    public class ActureFormViewConfig : IActureFormViewConfig
    {
        [XmlIgnore]
        public const string FileName = "ActureFormViewConfig.xml";

        /// <summary>
        /// 启动视图
        /// </summary>
        public int StartViewIndex { get; set; }

        /// <summary>
        /// 课程开始时视图
        /// </summary>
        public int CourseStartViewIndex { get; set; }

        /// <summary>
        /// 课程结束时视图
        /// </summary>
        public int CourseStopViewIndex { get; set; }

        /// <summary>
        /// 刷新时长
        /// </summary>
        public int ViewRfreshInterval { get; set; }
    }
}