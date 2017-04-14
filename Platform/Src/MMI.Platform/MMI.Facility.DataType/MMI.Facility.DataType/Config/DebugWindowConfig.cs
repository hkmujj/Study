using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using CommonUtil.Model;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Attributes;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.DataType.Config
{
    /// <summary>
    /// 用于的调试窗口
    /// </summary>
    public class UserDebugWindownConfig : XmlRectangle, IUserDebugWindownConfig
    {
        [XmlAttribute]
        public string TypeName { set; get; }
    }

    [XmlType]
    [ConfigureDescription("调试窗口配置", FileName)]
    public class DebugWindowConfig : NotificationObject, IDebugWindowConfig
    {
        public const string FileName = "DebugWindowConfig.xml";

        public const string RunningFileName = "DebugWindowConfig.Running.xml";

        [XmlElement("DebugWindow")]
        public ObservableCollection<UserDebugWindownConfig> UserDebugWindownConfigCollection { set; get; }

        [XmlIgnore]
        IEnumerable<IUserDebugWindownConfig> IDebugWindowConfig.UserDebugWindownConfigCollection
        {
            get { return UserDebugWindownConfigCollection; }
        }
    }
}