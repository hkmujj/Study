using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Course;

namespace MMI.Facility.DataType.Course
{
    [XmlRoot("protocol")]
    public class CourseStartParameter : NotificationObject, ICourseStartParameter
    {
        private string m_Description;
        private List<string> m_SelectedScreens;
        private string m_SignalFlag;
        private string m_LocomotiveNumber;
        private string m_CourseID;
        private string m_Version;
        private List<Station> m_AllStation;

        [XmlAttribute("Desc")]
        public string Description
        {
            get { return m_Description; }
            set
            {
                if (value == m_Description)
                {
                    return;
                }

                m_Description = value;
                RaisePropertyChanged(() => Description);
            }
        }


        [XmlAttribute("version")]
        public string Version
        {
            get { return m_Version; }
            set
            {
                if (value == m_Version)
                {
                    return;
                }

                m_Version = value;
                RaisePropertyChanged(() => Version);
            }
        }


        [XmlElement("课程编号")]
        public string CourseID
        {
            get { return m_CourseID; }
            set
            {
                if (value == m_CourseID)
                {
                    return;
                }

                m_CourseID = value;
                RaisePropertyChanged(() => CourseID);
            }
        }


        [XmlElement("机车号")]
        public string LocomotiveNumber
        {
            get { return m_LocomotiveNumber; }
            set
            {
                if (value == m_LocomotiveNumber)
                {
                    return;
                }

                m_LocomotiveNumber = value;
                RaisePropertyChanged(() => LocomotiveNumber);
            }
        }
        [XmlArray("站点")]
        [XmlArrayItem("车站")]
        public List<Station> AllStation
        {
            get { return m_AllStation; }
            set
            {
                if (Equals(value, m_AllStation))
                    return;

                m_AllStation = value;
                RaisePropertyChanged(() => AllStation);
            }
        }

        /// <summary>
        /// 信号标识  2.0  > Version 有效
        /// </summary>
        [XmlElement("信号模式")]
        public string SignalFlag
        {
            get { return m_SignalFlag; }
            set
            {
                if (value == m_SignalFlag)
                {
                    return;
                }

                m_SignalFlag = value;
                RaisePropertyChanged(() => SignalFlag);
            }
        }

        /// <summary>
        /// 屏的标志, Version > 2.0 有效
        /// </summary>
        [XmlArray("SelectedScreens")]
        [XmlArrayItem("Screen")]
        public List<string> SelectedScreens
        {
            set
            {
                if (Equals(value, m_SelectedScreens))
                {
                    return;
                }

                m_SelectedScreens = value;
                RaisePropertyChanged(() => SelectedScreens);
            }
            get { return m_SelectedScreens; }
        }

        /// <summary>
        /// 根据版本修改配置。
        /// </summary>
        public void AdjustByVersion()
        {
            if (string.Compare(Version, "2.0", StringComparison.Ordinal) > 0)
            {
                // not need in 2.0, let it = null
                SignalFlag = null;
            }
        }

        // ReSharper disable once UnusedMember.Local
        public static void Test()
        {
            var data = new CourseStartParameter()
            {
                SelectedScreens = new List<string>() { "ATP300S", "CRH380AL" },
                Description = "",
                Version = "2.0",
                CourseID = "118",
                AllStation = new List<Station>() { new Station() { ID = "nidhsi", ArriveTime = "nidsaddadassd", DeapartTime = "Nisdjaisd" }, new Station() { ID = "nidhsi", ArriveTime = "nidsaddadassd", DeapartTime = "Nisdjaisd" } }

            };
            DataSerialization.SerializeToXmlFile(data, "c:\\tools\\a.xml");
        }
    }

    public class Station
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }
        [XmlAttribute("到站时间")]
        public string ArriveTime { get; set; }
        [XmlAttribute("离站时间")]
        public string DeapartTime { get; set; }
    }
}