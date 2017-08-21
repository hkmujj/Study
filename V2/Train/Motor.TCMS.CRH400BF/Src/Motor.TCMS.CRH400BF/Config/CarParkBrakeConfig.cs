using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Motor.TCMS.CRH400BF.Config
{
    [ExcelLocation("Motor.TCMS.CRH400BF列车Car停放制动配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarParkBrakeConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("未知")]
        public string UnKnow { get; private set; }

        [ExcelField("施加")]
        public string Run { get; private set; }

        [ExcelField("隔离")]
        public string Isolation { get; private set; }

        [ExcelField("缓解")]
        public string Remission { get; private set; }

        [ExcelField("有效")]
        public string Effective { get; private set; }

        [ExcelField("无效")]
        public string UnEffective { get; private set; }


        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}
