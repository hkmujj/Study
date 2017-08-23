using System;
using System.ComponentModel;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Model.Fault
{
    [ExcelLocation("故障列表.xls", "Sheet1")]
    public class FaultInfo : ISetValueProvider
    {
        [ExcelField("逻辑号")]
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ExcelField("车")]
        public string Car { get; set; }
        [ExcelField("代码")]
        public string Code { get; set; }
        [ExcelField("等级")]
        public FaultLevel Level { get; set; }
        [ExcelField("内容")]
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ExcelField("提示")]
        public string Trips { get; set; }

        public DateTime HapendTime { get; set; }

        public DateTime ConfirmTime { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Level":
                    Level = (FaultLevel)int.Parse(value);
                    break;

            }
        }

        public FaultInfo Clone()
        {
            return MemberwiseClone() as FaultInfo;
        }
    }

    public enum FaultLevel
    {
        [Description("i")]
        Slight = 1,
        [Description("2")]
        Medium,
        [Description("3")]
        Grave,
    }
}
