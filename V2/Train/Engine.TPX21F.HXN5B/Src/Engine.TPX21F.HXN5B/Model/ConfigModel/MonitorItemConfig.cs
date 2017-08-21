using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Engine.TPX21F.HXN5B.Model.ConfigModel
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class MonitorItemConfig : ISetValueProvider
    {
        private string m_Name;
        public const string InvalidateName = "NULL";

        public MonitorItemConfig()
        {

        }

        [DebuggerStepThrough]
        public MonitorItemConfig(string name)
        {
            Name = name;
        }

        [ExcelField("显示名称")]
        public string Name
        {
            protected set { m_Name = value; }
            get { return m_Name == InvalidateName ? string.Empty : m_Name; }
        }

        [ExcelField("索引类型")]
        public string IndexTypeString { protected set; get; }

        [ExcelField("数据类型")]
        public string ValueTypeString { protected set; get; }

        [ExcelField("数据格式")]
        public string ValueFormat { protected set; get; }

        [ExcelField("逻辑索引1")]
        public string Index1 { protected set; get; }

        [ExcelField("逻辑索引2")]
        public string Index2 { protected set; get; }

        [ExcelField("逻辑索引3")]
        public string Index3 { protected set; get; }

        [ExcelField("逻辑索引4")]
        public string Index4 { protected set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public virtual void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    [ExcelLocation("Engine.TPX21F.HXN5B监控器-辅变.xls", "Sheet1")]
    public class MonitorAssistMachineItemConfig : MonitorItemConfig
    {
        public MonitorAssistMachineItemConfig(string name) : base(name)
        {
        }

        public MonitorAssistMachineItemConfig()
        {
            
        }

        [ExcelField("COMP1")]
        public string COMP1 { set; get; }

        [ExcelField("COMP2")]
        public string COMP2 { set; get; }

        [ExcelField("RFC1")]
        public string RFC1 { set; get; }

        [ExcelField("RFC2")]
        public string RFC2 { set; get; }

        [ExcelField("TBC")]
        public string TBC { set; get; }
    }

    [ExcelLocation("Engine.TPX21F.HXN5B监控器-自负荷.xls", "Sheet1")]
    public class MonitorSelfChargingItemConfig : MonitorItemConfig
    {
    }

    [ExcelLocation("Engine.TPX21F.HXN5B监控器-柴油机启动.xls", "Sheet1")]
    public class MonitorOilEnginStartItemConfig : MonitorItemConfig
    {
    }

    [ExcelLocation("Engine.TPX21F.HXN5B监控器-DCU.xls", "Sheet1")]
    public class MonitorDCUItemConfig : MonitorItemConfig
    {
    }

    [ExcelLocation("Engine.TPX21F.HXN5B监控器-ECU.xls", "Sheet1")]
    public class MonitorECUItemConfig : MonitorItemConfig
    {
    }

    [ExcelLocation("Engine.TPX21F.HXN5B监控器-相控.xls", "Sheet1")]
    public class MonitorPhaseControlItemConfig : MonitorItemConfig
    {
    }



    [ExcelLocation("Engine.TPX21F.HXN5B监控器-机车.xls", "Sheet1")]
    public class MonitorEngineItemConfig : MonitorItemConfig
    {
    }

    [ExcelLocation("Engine.TPX21F.HXN5B监控器-牵引.xls", "Sheet1")]
    public class MonitorTowItemConfig : MonitorItemConfig
    {
    }
}