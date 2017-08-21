using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Excel.Interface;

namespace Motor.HMI.CRH5A.Config.ConfigModel
{
    public class CRH5AFileConfig : ICorrectable
    {
        [XmlArray]
        public List<CRH5ACommunicationConfig> CRH5ACommunicationConfigCollection { set; get; }

        public void Correct(string dirctory)
        {
            CRH5ACommunicationConfigCollection.ForEach(e => e.Correct(dirctory));
        }
    }

    public class CRH5ACommunicationConfig : ExcelReaderConfig, ICorrectable
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