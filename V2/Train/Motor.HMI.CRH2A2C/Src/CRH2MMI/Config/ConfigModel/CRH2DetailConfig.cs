using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.Config.ConfigModel
{
    [XmlRoot]
    public class CRH2DetailConfig
    {

        [XmlElement]
        public CRH2ConfigData Data { set; get; }
    }

    static class CRH2DetailConfigTest
    {

        public static void Test()
        {
            XmlContentList<CarType>.Convertor = s => (CarType)(Enum.Parse(typeof(CarType), s));
            const string file = @"D:\Work\Yunda\MMI\CRH2\CRH2MMI\Config\DetailConfig\CRH2AConfig.xml";

            var v = DataSerialization.DeserializeFromXmlFile<CRH2DetailConfig>(file);

            var info = v.Data.JackConfig;

            foreach (var car in info.JackOfCars)
            {
                car.Units.ForEach(e =>
                {
                    if (e.Name.Contains("转向架") || e.Name.Contains("不足") || e.Name.Contains("隔离") || e.Name.Contains("制动不缓解"))
                    {
                        e.TextBkColors = new List<Color>() { Color.White, Color.Red };

                    }
                    //e.InBoolColoumNames = new string[1];
                    //e.OutBoolColoumNames = new string[1];
                    //e.OutFloatColoumNames = new string[1];
                    //e.InFloatColoumNames = new string[1];
                    //e.InBoolList = new List<int>();
                });
            }

            DataSerialization.SerializeToXmlFile(v.Data.JackConfig, "D:\\a.xml");

        }
    }
}