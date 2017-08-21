using System.Diagnostics;
using Excel.Interface;

namespace MMITool.Addin.MMIDataDebugger.Config.Model
{
    [DebuggerDisplay("Index={Index}, Name={Name}")]
    public class IndexDescriptionConfig : ISetValueProvider
    {
        [ExcelField("Name", true)]
        public string Name;

        [ExcelField("Index")]
        public int Index { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}