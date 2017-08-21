using System;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.XiaMenLine1.Interface;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    [ExcelLocation("厦门地铁1号线事件表.xls", "Sheet1")]
    public class EventInfo : NotificationObject, ISetValueProvider, IEventInfo
    {
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "LogicId":
                    if (value != "")
                    {
                        LogicId = Convert.ToInt32(value);
                    }
                    else
                    {
                        LogicId = Convert.ToInt32(2000);
                    }
                    break;
                case "Number":
                    if (value != "")
                    {
                        Number = Convert.ToInt32(value);
                    }
                    else
                    {
                        Number = Convert.ToInt32(0);
                    }
                    break;
                case "FaultCode":
                    FaultCode = value;
                    break;
                case "Level":
                    if (value != "")
                    {
                        Level = (EventLevel)Convert.ToInt32(value);
                    }
                    else
                    {
                        Level = (EventLevel)Convert.ToInt32(0);
                    }
                    break;
                case "Description":
                    Description = value;
                    break;
                case "System":
                    System = value;
                    break;
                case "CarNumber":
                    CarNumber = value;
                    break;
                case "IsCofirm":
                    break;
                case "HappenDateTime":
                    break;
                case "CofirmDateTime":
                    break;
                case "Handbook":
                    Handbook = value;
                    break;
                case "Guideline":
                    Guideline = value;
                    break;

            }
        }

        public int CompareTo(IEventInfo other)
        {
            return this.HappenDateTime.CompareTo(other.HappenDateTime);
        }

        [ExcelField("逻辑号")]
        public int LogicId { get; private set; }

        //[ExcelField("")]
        public int Number { get; set; }

        [ExcelField("故障代码")]
        public string FaultCode { get; private set; }

        [ExcelField("等级")]
        public EventLevel Level { get; private set; }

        [ExcelField("故障描述")]
        public string Description { get; private set; }

        [ExcelField("所属系统")]
        public string System { get; private set; }

        [ExcelField("车厢号")]
        public string CarNumber { get; private set; }

        //[ExcelField("")]
        public bool IsCofirm { get; set; }

        //[ExcelField("")]
        public DateTime HappenDateTime { get; set; }

        //[ExcelField("")]
        public DateTime CofirmDateTime { get; set; }

        [ExcelField("故障应急处理指南")]
        public string Handbook { get; private set; }

        [ExcelField("检修指引")]
        public string Guideline { get; private set; }

        public object Clone()
        {
            var tmp = new EventInfo();

            tmp.CarNumber = CarNumber;
            tmp.LogicId = LogicId;
            tmp.FaultCode = FaultCode;
            tmp.Level = Level;
            tmp.Description = Description;
            tmp.System = System;
            tmp.Handbook = Handbook;
            tmp.Guideline = Guideline;
            return tmp;

        }
    }
}