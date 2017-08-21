using Excel.Interface;

namespace Engine.TPX21F.HXN5B.Model.ConfigModel
{
    [ExcelLocation("Engine.TPX21F.HXN5B机车设置-自动启停.xls", "Sheet1")]
    public class AutoStartEngineItemConfig : ISetValueProvider
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