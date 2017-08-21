using System.Xml.Serialization;

namespace Engine.TCMS.HXD3D.Config
{
    [XmlRoot]
    public class ProjectConfig
    {
        public const string FileName = "HXD3DProjectConfig.xml";

        [XmlElement]
        public HXD3DProject ProjectType { get; set; }

        public void Test()
        {

        }
    }
}