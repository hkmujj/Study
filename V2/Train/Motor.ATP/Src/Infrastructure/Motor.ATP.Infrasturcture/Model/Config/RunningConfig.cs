using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace Motor.ATP.Infrasturcture.Model.Config
{
    [XmlRoot]
    public class RunningConfig
    {
        public const string FileName = "RunningConfig.xml";

        public string Version { get; set; }

        [XmlArray]
        [XmlArrayItem("Version")]
        public List<string> KnownVersions { get; set; }


        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void Test()
        {
            var d = new RunningConfig()
            {
                Version = new Version(1, 2, 1, 2).ToString(),
                KnownVersions = new List<string>()
                {
                    new Version(1, 2, 1, 2).ToString(),
                    new Version(1, 2, 1, 3).ToString(),
                }
            };
            var s = DataSerialization.SerializeToString(d);
        }
    }
}