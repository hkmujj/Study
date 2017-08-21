
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou空调新风阀.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarAirConditionOutsideDamperStatusConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }
        
        [ExcelField("新风阀全关Index1")]
        public string OutsideDamper1Closed { get; private set; }

        [ExcelField("新风阀50Index1")]
        public string OutsideDamper1Half { get; private set; }

        [ExcelField("新风阀75Index1")]
        public string OutsideDamper1SeventyFive { get; private set; }

        [ExcelField("新风阀100Index1")]
        public string OutsideDamper1Totally { get; private set; }

        [ExcelField("新风阀未知Index1")]
        public string OutsideDamper1Unknow { get; private set; }

        [ExcelField("新风阀全关Index2")]
        public string OutsideDamper2Closed { get; private set; }

        [ExcelField("新风阀50Index2")]
        public string OutsideDamper2Half { get; private set; }

        [ExcelField("新风阀75Index2")]
        public string OutsideDamper2SeventyFive { get; private set; }

        [ExcelField("新风阀100Index2")]
        public string OutsideDamper2Totally { get; private set; }

        [ExcelField("新风阀未知Index2")]
        public string OutsideDamper2Unknow { get; private set; }
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
