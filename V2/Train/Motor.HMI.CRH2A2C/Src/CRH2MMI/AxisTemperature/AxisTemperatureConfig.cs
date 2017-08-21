using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.BreakLocked;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.AxisTemperature
{
    [XmlRoot]
    public class AxisTemperatureConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlElement]
        public List<GridConfig> Grid { set; get; }

        [XmlArray]
        [XmlArrayItem("SendModel")]
        public List<AxisTemperatureSendModel> AxisTemperatureSendModels { set; get; }

        private static void  Test()
        {
            var s=new AxisTemperatureConfig();
            s.AxisTemperatureSendModels.Add(new AxisTemperatureSendModel()
            {
                CarNo = 10,
                AxisTemperaturedLocation = 10,
            });
            DataSerialization.SerializeToXmlFile(s,@"d:\x.xml");
        }

    }


    [DebuggerDisplay("CarNo={CarNo} Type={Type} OutBoolColoumNames={OutBoolXmlNames}")]
    public class AxisTemperatureSendModel : CRH2CommunicationPortModel
    {
        [XmlAttribute]
        public int CarNo { set; get; }

        [XmlAttribute]
        public int AxisTemperaturedLocation { set; get; }

        [XmlAttribute]
        public CtrolType Type { set; get; }

    }


}
