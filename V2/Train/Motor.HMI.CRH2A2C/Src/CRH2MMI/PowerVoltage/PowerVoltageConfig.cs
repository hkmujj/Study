using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.PowerVoltage
{
    public class PowerVoltageConfig
    {
        public List<PowerVoltageModel> FirstPageModels { set; get; }

        public List<PowerVoltageModel> SecondPageModels { set; get; }

        /// <summary>
        /// 修改 \r\n为换行
        /// </summary>
        public void Correction()
        {
            FirstPageModels.ForEach(e => e.VoltageType = e.VoltageType.Replace(Line, "\r\n"));
            SecondPageModels.ForEach(e => e.VoltageType = e.VoltageType.Replace(Line, "\r\n"));
        }

        private const string Line = "\\r\\n";
    }

    public class PowerVoltageModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        [XmlAttribute]
        public string VoltageType { set; get; }

        /// <summary>
        ///  最大电压值
        /// </summary>
        [XmlAttribute]
        public int MaxVoltage { set; get; }

        [XmlAttribute]
        public int FirstAbsX { set; get; }

        [XmlAttribute]
        public int FirstAbsY { set; get; }

        [XmlAttribute]
        public int TextWidth { set; get; }

        [XmlAttribute]
        public int TextHeight { set; get; }

        [XmlElement("VoltageCar")]
        public List<VoltageConfig> VoltageConfigs { set; get; }


    }

    public class VoltageConfig : CRH2CommunicationPortModel
    {
        [XmlAttribute]
        public int CarName { set; get; }

        //[XmlAttribute]
        //public int FloatIndex { set; get; }

    }

    internal static class PowerVoltageConfigTest
    {
        public static void Test()
        {
            var data = new PowerVoltageConfig()
            {
                FirstPageModels = new List<PowerVoltageModel>()
            {
                new PowerVoltageModel()
                {
                    FirstAbsX = 0, FirstAbsY = 1,TextWidth = 20, TextHeight = 29,
                    VoltageType = "内",
                    MaxVoltage = 100,
                    VoltageConfigs = new List<VoltageConfig>()
                    {
                        //new VoltageConfig(){ CarName = 2, FloatIndex = 1},
                        //new VoltageConfig(){ CarName = 3, FloatIndex = 11},
                    }
                }
            }};

            //DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");

            var v = DataSerialization.DeserializeFromXmlFile<PowerVoltageConfig>("D:\\a.xml");


        }

    }

}

