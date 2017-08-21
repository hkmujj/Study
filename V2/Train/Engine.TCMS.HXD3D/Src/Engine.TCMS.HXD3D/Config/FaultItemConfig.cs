using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Engine.TCMS.HXD3D.Config
{
    [ExcelLocation("HXD3D故障信息表.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class FaultItemConfig : ISetValueProvider
    {
        [ExcelField("LogicIndex")]
        public int LogicIndex { get; private set; }

        [ExcelField("SubsysName")]
        public string SubsysName { get; private set; }

        [ExcelField("MsgLevel")]
        public int MsgLevel { get; private set; }

        [ExcelField("Content")]
        public string Content { get; private set; }

        [ExcelField("Solution")]
        public string Solution { get; private set; }

        [ExcelField("SendIndex")]
        public int SendIndex { get; private set; }

        [ExcelField("ShowType")]
        public int ShowType { get; private set; }

        [ExcelField("TrainId")]
        public int TrainId { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}