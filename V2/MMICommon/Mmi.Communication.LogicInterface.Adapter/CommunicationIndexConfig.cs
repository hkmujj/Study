using System.IO;
using System.Xml.Serialization;
using Excel.Interface;

namespace Mmi.Communication.Index.Adapter
{
    [XmlRoot]
    public class CommunicationIndexConfig : ExcelReaderConfig
    {
        [XmlAttribute]
        public CommunicationIndexType Type { set; get; }

        public void Correct(string rootConfigDirectory)
        {
            File = Path.Combine(rootConfigDirectory, File);
        }
    }
}