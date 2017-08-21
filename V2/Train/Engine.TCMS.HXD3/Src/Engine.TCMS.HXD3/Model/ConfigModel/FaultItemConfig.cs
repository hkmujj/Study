using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [ExcelLocation("HXD3TCMS故障接口.xls", "Sheet1")]
    [DebuggerDisplay("LogicIndex={LogicIndex} NameAndContent={NameAndContent}")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class FaultItemConfig : ISetValueProvider
    {
        [DebuggerStepThrough]
        public FaultItemConfig(int identifier, string nameAndContent, int handleId)
        {
            Identifier = identifier;
            NameAndContent = nameAndContent;
            HandleId = handleId;
        }

        public FaultItemConfig()
        {
            
        }

        [ExcelField("LogicIndex", true)]
        public int LogicIndex { private set; get; }

        [ExcelField("Identifier")]
        public int Identifier { private set; get; }

        [ExcelField("NameAndContent")]
        public string NameAndContent { private set; get; }

        [ExcelField("HandleId")]
        public int HandleId { private set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}