using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CommonUtil.Util;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Config
{
    [XmlRoot]
    public class DifferenceConfig
    {

        public List<TrainConfig> AllTrainConfig { get; set; }
        [XmlIgnore]
        public static string File { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\TrainDiffConfig.xml"); } }
        private static void Test()
        {
            var a = new DifferenceConfig();
            a.AllTrainConfig = new List<TrainConfig>();
            a.AllTrainConfig.Add(new TrainConfig
            {
                IsChar = false,
                DoorUseDisplay = false,
                Type = ProjectType.CRH380B
            });
            a.AllTrainConfig.Add(new TrainConfig
            {
                IsChar = false,
                DoorUseDisplay = false,
                Type = ProjectType.CRH3C
            });
            a.AllTrainConfig.Add(new TrainConfig
            {
                IsChar = false,
                DoorUseDisplay = true,
                Type = ProjectType.CRH380BL
            });
            DataSerialization.SerializeToXmlFile(a, @"d:\a.xml");
        }
    }

    public class TrainConfig
    {
        /// <summary>
        /// 车型
        /// </summary>
        [XmlAttribute]
        public ProjectType Type { get; set; }
        /// <summary>
        /// 门禁用显示
        /// </summary>
        [XmlAttribute]
        public bool DoorUseDisplay { get; set; }
        /// <summary>
        /// 是不是字母
        /// </summary>
        [XmlAttribute]
        public bool IsChar { get; set; }
    }
}