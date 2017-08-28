﻿using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Motor.TCMS.CRH400BF.Config
{
    [ExcelLocation("Motor.TCMS.CRH400BF列车Car基本配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarBaseConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("Car名字")]
        public string Name { get; private set; }

       
        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}