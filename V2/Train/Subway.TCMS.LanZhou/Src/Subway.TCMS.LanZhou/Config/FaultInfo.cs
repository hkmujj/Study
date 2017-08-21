using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Model.Domain.Fault;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou���Ϻ���ʾ��Ϣ.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class FaultInfo : ISetValueProvider
    {
        [ExcelField("�߼�����")]
        public int LogicIndex { get; private set; }
        [ExcelField("���ϳ���")]
        public int CarIndex { private set; get; }
        [ExcelField("���ϴ���")]
        public int Code { get; set; }
        [ExcelField("�ȼ�")]
        public TrainFaultLevel Level { get; set; }
        [ExcelField("��������")]
        public string Description { get; set; }
        [ExcelField("����λ��")]
        public string Location { get; set; }
        [ExcelField("˾��������ʾ")]
        public string DriverOperateNotice { get; set; }
        [ExcelField("������ʾ")]
        public string MaintenanceTips { get; set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    
    }
}