using System.Diagnostics;
using Engine.TPX21F.HXN5B.Model.Interface;
using Excel.Interface;

namespace Engine.TPX21F.HXN5B.Model.ConfigModel
{
    [ExcelLocation("Engine.TPX21F.HXN5B测试-自测试条件.xls", "Sheet1")]
    public class TestSelfConditionItemConfig : TestItemConfig { }

    [ExcelLocation("Engine.TPX21F.HXN5B测试-警惕条件.xls", "Sheet1")]
    public class TestVigilantConditionItemConfig : TestItemConfig { }

    [ExcelLocation("Engine.TPX21F.HXN5B测试-速度条件.xls", "Sheet1")]
    public class TestSpeedConditionItemConfig : TestItemConfig { }


    [DebuggerDisplay("Content={Content}, Index={IndexBool}")]
    public class TestItemConfig :ISetValueProvider, IPassableItemConfig
    {
        [ExcelField("显示名称")]
        public string Content { protected set; get; }

        [ExcelField("逻辑索引Bool")]
        public string IndexBool { protected set; get; }

        [ExcelField("逻辑索引Float")]
        public string IndexFloat { protected set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
            
        }
    }
}