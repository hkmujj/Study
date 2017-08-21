using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Model.Domain.Fault;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou故障和提示信息.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class FaultInfo : ISetValueProvider
    {
        [ExcelField("逻辑索引")]
        public int LogicIndex { get; private set; }
        [ExcelField("故障车号")]
        public int CarIndex { private set; get; }
        [ExcelField("故障代码")]
        public int Code { get; set; }
        [ExcelField("等级")]
        public TrainFaultLevel Level { get; set; }
        [ExcelField("故障描述")]
        public string Description { get; set; }
        [ExcelField("故障位置")]
        public string Location { get; set; }
        [ExcelField("司机操作提示")]
        public string DriverOperateNotice { get; set; }
        [ExcelField("检修提示")]
        public string MaintenanceTips { get; set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    
    }
}