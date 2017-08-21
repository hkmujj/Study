using System.Diagnostics;
using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [DebuggerDisplay("Name={Name}")]
    public class NameIndex : ISetValueProvider
    {
        public const string InvalidateValue = "NULL";

        public NameIndex(string name, string index)
        {
            Name = name;
            Index = index;
        }

        public NameIndex()
        {

        }

        [ExcelField("Name")]
        public string Name { protected set; get; }

        [ExcelField("Index")]
        public string Index { protected set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public virtual void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}