using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车CarPECU状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarPECUConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int CarIndex { get; private set; }

        [ExcelField("PECU1Index激活")]
        public string PECU1IndexActive { get; private set; }

        [ExcelField("PECU1Index通话中")]
        public string PECU1IndexUsing { get; private set; }

        [ExcelField("PECU2Index激活")]
        public string PECU2IndexActive { get; private set; }

        [ExcelField("PECU2Index通话中")]
        public string PECU2IndexUsing { get; private set; }

        [ExcelField("PECU3Index激活")]
        public string PECU3IndexActive { get; private set; }

        [ExcelField("PECU3Index通话中")]
        public string PECU3IndexUsing { get; private set; }

        [ExcelField("PECU4Index激活")]
        public string PECU4IndexActive { get; private set; }

        [ExcelField("PECU4Index通话中")]
        public string PECU4IndexUsing { get; private set; }

        [ExcelField("PECU5Index激活")]
        public string PECU5IndexActive { get; private set; }

        [ExcelField("PECU5Index通话中")]
        public string PECU5IndexUsing { get; private set; }

        [ExcelField("PECU6Index激活")]
        public string PECU6IndexActive { get; private set; }

        [ExcelField("PECU6Index通话中")]
        public string PECU6IndexUsing { get; private set; }

        [ExcelField("PECU7Index激活")]
        public string PECU7IndexActive { get; private set; }

        [ExcelField("PECU7Index通话中")]
        public string PECU7IndexUsing { get; private set; }

        [ExcelField("PECU8Index激活")]
        public string PECU8IndexActive { get; private set; }

        [ExcelField("PECU8Index通话中")]
        public string PECU8IndexUsing { get; private set; }

       

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
            
        }
    }
}