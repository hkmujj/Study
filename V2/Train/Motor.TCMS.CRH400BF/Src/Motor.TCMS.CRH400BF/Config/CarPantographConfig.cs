using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Motor.TCMS.CRH400BF.Config
{
   
    [ExcelLocation("Motor.TCMS.CRH400BF列车Car受电弓状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarPantographConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("未知")]
        public string UnKnow { get; private set; }

        [ExcelField("升弓")]
        public string Up { get; private set; }

        [ExcelField("降弓")]
        public string Down { get; private set; }

        [ExcelField("切除")]
        public string CutOff { get; private set; }


        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}
