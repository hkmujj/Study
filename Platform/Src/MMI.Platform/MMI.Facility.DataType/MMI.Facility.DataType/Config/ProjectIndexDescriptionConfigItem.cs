using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.IndexDescription;

namespace MMI.Facility.DataType.Config
{
    [XmlType]
    public class ProjectIndexDescriptionConfigItem : NotificationObject, IExcelReaderConfig
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

        // ReSharper disable once UnusedMember.Local
        private static void Test()
        {
            var d = new ProjectIndexDescriptionConfigItem()
            {
                File = "a.xml",
                IndexType = AppIndexType.InBool,
                SheetNames = new List<string>() {"sheet1"},
                Coloumns = new List<ColoumnConfig>()
                {
                    new ColoumnConfig() {IsPrimaryKey = true, Name = "name"},
                    new ColoumnConfig()
                    {
                        IsPrimaryKey = false,
                        Name = "index",
                    }
                },
            };
            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }
    }
}