
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车CarDoorLock状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarDoorLockStateConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int CarIndex { get; private set; }


        [ExcelField("元素索引")]
        public int ItemIndex { get; private set; }

        [ExcelField("所有门关锁到位")]
        public string LockAllIndex { get; private set; }

        [ExcelField("至少有一扇门未关锁到位")]
        public string AtLastOnUnlockIndex { get; private set; }

        [ExcelField("门环路未知")]
        public string UnkonwIndex { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}