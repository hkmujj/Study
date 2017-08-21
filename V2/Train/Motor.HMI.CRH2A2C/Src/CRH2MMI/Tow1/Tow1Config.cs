using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.Tow1
{
    [XmlRoot]
    public class Tow1Config
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlArray("PageConfigs")]
        [XmlArrayItem("Page")]
        public List<Tow1PageConfig> Tow1PageConfigs { set; get; }

        /// <summary>
        /// 修改 \r\n为换行
        /// </summary>
        public void Correction()
        {
            Tow1PageConfigs.ForEach(e =>
            {
                e.Grid.Rows.ForEach(row => row.Name = row.Name.Replace(Line, "\r\n"));
                e.PageName = e.PageName.Replace(Line, "\r\n");
            });
        }

        private const string Line = "\\r\\n";
    }

    [DebuggerDisplay("PageName = {PageName}")]
    public class Tow1PageConfig
    {
        [XmlAttribute]
        public string PageName { set; get; }

        public GridConfig Grid { set; get; }
    }

    class BrakeInfoConfigTest
    {
        public static void Test()
        {
            var data = new Tow1Config()
            {
                Tow1PageConfigs = new List<Tow1PageConfig>()
                {
                    new Tow1PageConfig() {PageName = "制动", Grid = new GridConfig() {}}
                }
            };

            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");
        }
    }
}
