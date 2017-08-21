using Engine.TPX21F.HXN5B.Model.Interface;
using Excel.Interface;

namespace Engine.TPX21F.HXN5B.Model.ConfigModel
{
    [ExcelLocation("Engine.TPX21F.HXN5B维护-操纵台选择.xls", "Sheet1")]
    public class OperConsoleSelectItemConfig : ISetValueProvider, IPassableItemConfig
    {

        [ExcelField("显示名称")]
        public string Content { protected set; get; }

        [ExcelField("逻辑索引")]
        public string IndexBool { protected set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}