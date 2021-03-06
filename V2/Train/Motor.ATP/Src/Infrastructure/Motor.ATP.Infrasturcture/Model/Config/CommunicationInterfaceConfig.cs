﻿using System.Xml.Serialization;
using Excel.Interface;

namespace Motor.ATP.Infrasturcture.Model.Config
{
    [XmlRoot]
    public class CommunicationInterfaceConfig
    {
        public ExcelReaderConfig InBoolConfig { set; get; }

        public ExcelReaderConfig OutBoolConfig { set; get; }

        public ExcelReaderConfig InFloatConfig { set; get; }

        public ExcelReaderConfig OutFloatConfig { set; get; }
    }
}