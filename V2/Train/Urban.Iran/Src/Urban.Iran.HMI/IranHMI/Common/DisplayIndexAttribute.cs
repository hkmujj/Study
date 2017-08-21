using System;
using System.Xml.Serialization;

namespace Urban.Iran.HMI.Common
{
    [XmlRoot]
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class DisplayLocationAttribute : Attribute
    {
        [XmlAttribute]
        public int Horizontal { set; get; }

        [XmlAttribute]
        public int Vertical { set; get; }
    }
}