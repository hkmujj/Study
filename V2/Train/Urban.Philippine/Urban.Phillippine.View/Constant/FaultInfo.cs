using System;
using Excel.Interface;
using Urban.Phillippine.View.Extend;
using Urban.Phillippine.View.Interface;

namespace Urban.Phillippine.View.Constant
{
    [ExcelLocation("FaulrInfo.xls", "Sheet1")]
    public class FaultInfo : IFaultInfo, ISetValueProvider
    {
        [ExcelField("LogicName")]
        public int Logic { get; set; }

        [ExcelField("Name")]
        public string Name { get; set; }
        [ExcelField("Code", true)]
        public int Code { get; set; }
        [ExcelField("Description")]
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public object Clone()
        {
            IFaultInfo info = new FaultInfo();
            info.Name = this.Name;
            info.Code = this.Code;
            info.Description = this.Description;
            info.Logic = this.Logic;
            return info;
        }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Name":
                    Name = value;
                    break;
                case "Code":
                    Code = value.ConvertToInt();
                    break;
                case "Description":
                    Description = value;
                    break;
                case "Logic":
                    Logic = value.ConvertToInt();
                    break;
            }
        }
    }
}