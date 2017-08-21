using System.Collections.ObjectModel;
using System.Diagnostics;
using Excel.Interface;

namespace Engine.TPX21F.HXN5B.Model.ConfigModel
{

    public class AccumulativeDataConfig
    {
        [DebuggerStepThrough]
        public AccumulativeDataConfig(ReadOnlyCollection<AccumulativePowerConfig> powerCollection, ReadOnlyCollection<AccumulativeTimeConfig> timeCollection, ReadOnlyCollection<AccumulativeTowTimeConfig> towCollection, ReadOnlyCollection<AccumulativeBrakeTimeConfig> brakeCollection)
        {
            PowerCollection = powerCollection;
            TimeCollection = timeCollection;
            TowCollection = towCollection;
            BrakeCollection = brakeCollection;
        }

        public ReadOnlyCollection<AccumulativePowerConfig> PowerCollection { private set; get; }
        public ReadOnlyCollection<AccumulativeTimeConfig> TimeCollection { private set; get; }
        public ReadOnlyCollection<AccumulativeTowTimeConfig> TowCollection { private set; get; }
        public ReadOnlyCollection<AccumulativeBrakeTimeConfig> BrakeCollection { private set; get; }
    }

    [ExcelLocation("Engine.TPX21F.HXN5B其它-累计数据.xls","能量累计")]
    public class AccumulativePowerConfig : AccumulativeDataItemConfig
    {
    }

    [ExcelLocation("Engine.TPX21F.HXN5B其它-累计数据.xls", "时间累计")]
    public class AccumulativeTimeConfig : AccumulativeDataItemConfig
    {
    }
    [ExcelLocation("Engine.TPX21F.HXN5B其它-累计数据.xls", "牵引")]
    public class AccumulativeTowTimeConfig : AccumulativeDataItemConfig
    {
    }
    [ExcelLocation("Engine.TPX21F.HXN5B其它-累计数据.xls", "制动")]
    public class AccumulativeBrakeTimeConfig : AccumulativeDataItemConfig
    {
    }

    [DebuggerDisplay("ShowContent={ShowContent}, LogicIndex={LogicIndex}")]
    public class AccumulativeDataItemConfig : ISetValueProvider
    {

        [ExcelField("显示内容")]
        public string ShowContent { get; set; }

        [ExcelField("逻辑索引")]
        public string LogicIndex { get; protected set; }



        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}