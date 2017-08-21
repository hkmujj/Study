using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Addin.MMIDataDebugger.Config.Model
{
    [XmlRoot]
    public class MMIDataDebuggerConfig
    {
        public const string File = "MMIDataDebuggerConfig.xml";

        public DataBufferConfig DataBufferConfig { get; set; }

        [XmlArray]
        [XmlArrayItem("Item")]
        public List<ValueDescriptionConfigItem> ValueDescriptionConfigItems { get; set; }

        // ReSharper disable once UnusedMember.Local
        private void Test()
        {
            var d = DataSerialization.DeserializeFromXmlFile<MMIDataDebuggerConfig>("D:\\a.xml");
        }
    }

    public class DataBufferConfig
    {
        public BufferSizeConfig InData { get; set; }

        public BufferSizeConfig OutData { get; set; }
    }

    public class BufferSizeConfig
    {
        [XmlAttribute]
        public int BoolStart { get; set; }

        [XmlAttribute]
        public int BoolEnd { get; set; }

        [XmlAttribute]
        public int FloatStart { get; set; }

        [XmlAttribute]
        public int FloatEnd { get; set; }
    }

    public class ValueDescriptionConfigItem : NotificationObject, IExcelReaderConfig
    {
        private AppIndexType m_IndexType;
        private string m_File;
        private List<string> m_SheetNames;
        private List<ColoumnConfig> m_Coloumns;

        public AppIndexType IndexType
        {
            set
            {
                if (value == m_IndexType)
                {
                    return;
                }
                m_IndexType = value;
                RaisePropertyChanged(() => IndexType);
            }
            get { return m_IndexType; }
        }

        public string File
        {
            set
            {
                if (value == m_File)
                {
                    return;
                }
                m_File = value;
                RaisePropertyChanged(() => File);
            }
            get { return m_File; }
        }

        [XmlArray]
        [XmlArrayItem("Sheet")]
        public List<string> SheetNames
        {
            get { return m_SheetNames; }
            set
            {
                if (Equals(value, m_SheetNames))
                {
                    return;
                }
                m_SheetNames = value;
                RaisePropertyChanged(() => SheetNames);
            }
        }

        [XmlArray("ReadColoumns")]
        [XmlArrayItem("Coloumn")]
        public List<ColoumnConfig> Coloumns
        {
            get { return m_Coloumns; }
            set
            {
                if (Equals(value, m_Coloumns))
                {
                    return;
                }
                m_Coloumns = value;
                RaisePropertyChanged(() => Coloumns);
            }
        }
        
    }
}