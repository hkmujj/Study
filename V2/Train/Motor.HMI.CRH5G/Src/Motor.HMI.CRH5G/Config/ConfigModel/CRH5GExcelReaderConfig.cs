using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Excel.Interface;

namespace Motor.HMI.CRH5G.Config.ConfigModel
{
    public class CRH5GFileConfig : ICorrectable
    {
        [XmlArray]
        public List<CRH5GCommunicationConfig> CRH5GCommunicationConfigCollection { set; get; }

        public void Correct(string dirctory)
        {
            CRH5GCommunicationConfigCollection.ForEach(e => e.Correct(dirctory));
        }
    }

    public class CRH5GCommunicationConfig : ExcelReaderConfig, ICorrectable
    {
        [XmlAttribute]
        public CommunicationDataType DataType { set; get; }

        public void Correct(string dirctory)
        {
            File = Path.Combine(dirctory, File);
        }
    }

    public enum CommunicationDataType
    {
        InBool,

        InFloat,

        OutBool,

        OutFloat,

    }

}