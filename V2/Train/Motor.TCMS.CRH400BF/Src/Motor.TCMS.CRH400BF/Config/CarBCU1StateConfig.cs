using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Motor.TCMS.CRH400BF.Config
{
    [ExcelLocation("Motor.TCMS.CRH400BF列车CarBCU1状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarBcu1StateConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("未知")]
        public string Unknow { get; private set; }

        [ExcelField("工作")]
        public string Run { get; private set; }

        [ExcelField("未工作")]
        public string NotRun { get; private set; }

        [ExcelField("异常")]
        public string Unusual { get; private set; }

        [ExcelField("无效")]
        public string UnEffect { get; private set; }


        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}
