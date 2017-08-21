using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou空调压缩机.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarAirConditionCompressorConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("压缩机运行1")]
        public string CompressorRunning1 { get; private set; }

        [ExcelField("压缩机未运行1")]
        public string CompressorStop1 { get; private set; }

        [ExcelField("压缩机运行2")]
        public string CompressorRunning2 { get; private set; }

        [ExcelField("压缩机未运行2")]
        public string CompressorStop2 { get; private set; }

        [ExcelField("压缩机运行3")]
        public string CompressorRunning3 { get; private set; }

        [ExcelField("压缩机未运行3")]
        public string CompressorStop3 { get; private set; }

        [ExcelField("压缩机运行4")]
        public string CompressorRunning4 { get; private set; }

        [ExcelField("压缩机未运行4")]
        public string CompressorStop4 { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
