using System;
using System.IO;
using System.Xml.Serialization;

namespace Motor.HMI.CRH1A.Config.Editer.Model
{
    [XmlRoot]
    public class EditerConfig
    {
        public string RelativePath { set; get; }

        public string AbsolutePath{get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RelativePath); }}
    }
}