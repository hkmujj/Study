using System.IO;
using System.Xml.Serialization;
using CommonUtil.Util;
using Microsoft.Practices.Prism.ViewModel;

namespace General.FlashLoader.ConfigEditer.Model
{
    [XmlType]
    public class FlashConfigRoot : NotificationObject
    {
        private string m_RootPath;
        private DataInterface m_DataInterface;
        private string m_SwfFile;

        public string RootPath
        {
            set
            {
                if (value == m_RootPath)
                {
                    return;
                }
                m_RootPath = value;
                RaisePropertyChanged(() => RootPath);
            }
            get { return m_RootPath; }
        }


        public string SwfFile
        {
            set
            {
                if (value == m_SwfFile)
                {
                    return;
                }
                m_SwfFile = value;
                RaisePropertyChanged(() => SwfFile);
            }
            get { return m_SwfFile; }
        }

        public DataInterface DataInterface
        {
            set
            {
                if (Equals(value, m_DataInterface))
                {
                    return;
                }
                m_DataInterface = value;
                RaisePropertyChanged(() => DataInterface);
            }
            get { return m_DataInterface; }
        }

        // ReSharper disable once UnusedMember.Local
        private static void Test()
        {
            var data = new FlashConfigRoot()
            {
                RootPath = "D:\\Tmp\\",
                SwfFile = "HXD2.swf",
                DataInterface = new DataInterface()
                {
                    InBool =
                        new ExcelConfig()
                        {
                            File = "Config\\HXD2车辆屏接口_InBool.xls",
                            Sheet = "Sheet1",
                            Name = "Name",
                            Index = "Index",
                        },
                    OutBool =
                        new ExcelConfig()
                        {
                            File = "Config\\HXD2车辆屏接口_OutBool.xls",
                            Sheet = "Sheet1",
                            Name = "Name",
                            Index = "Index",
                        },
                    InFloat =
                        new ExcelConfig()
                        {
                            File = "Config\\HXD2车辆屏接口_InFloat.xls",
                            Sheet = "Sheet1",
                            Name = "Name",
                            Index = "Index",
                        },
                    OutFloat =
                        new ExcelConfig()
                        {
                            File = "Config\\HXD2车辆屏接口_OutFloat.xls",
                            Sheet = "Sheet1",
                            Name = "Name",
                            Index = "Index",
                        },
                }
            };
            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");
        }

        // ReSharper disable once UnusedMember.Local
        private void TestD()
        {
            var file = "D:\\HXD2Config.xml";
            var data = DataSerialization.DeserializeFromXmlFile<FlashConfigRoot>(file);


            if (File.Exists(file))
            {
                var directory = Path.GetDirectoryName(file);

                var d3 = Path.GetFullPath(".");

                var d2 = Path.GetFullPath(data.RootPath);
                if (d2 == string.Empty)
                {
                    var dir = Path.Combine(directory, data.RootPath);
                }
            }
            else
            {
                //TODO log.
            }

            

        }
    }
}