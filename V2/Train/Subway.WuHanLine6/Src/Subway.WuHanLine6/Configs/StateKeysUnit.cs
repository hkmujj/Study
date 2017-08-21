using Excel.Interface;

namespace Subway.WuHanLine6.Configs
{
    /// <summary>
    ///
    /// </summary>
    [ExcelLocation("武汉地铁6号线按钮响应.xls", "Sheet1")]
    public class StateInterfaceUnit : ISetValueProvider
    {
        /// <summary>
        ///
        /// </summary>
        [ExcelField("Key")]
        public string Key { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("标题")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("内容控件名")]
        public string Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第1个按键内容")]
        public string Btn01Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第1个按键处理")]
        public string Btn01ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第2个按键内容")]
        public string Btn02Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第2个按键处理")]
        public string Btn02ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第3个按键内容")]
        public string Btn03Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第3个按键处理")]
        public string Btn03ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第4个按键内容")]
        public string Btn04Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第4个按键处理")]
        public string Btn04ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第5个按键内容")]
        public string Btn05Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第5个按键处理")]
        public string Btn05ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第6个按键内容")]
        public string Btn06Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第6个按键处理")]
        public string Btn06ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第7个按键内容")]
        public string Btn07Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第7个按键处理")]
        public string Btn07ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第8个按键内容")]
        public string Btn08Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第8个按键处理")]
        public string Btn08ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第9个按键内容")]
        public string Btn09Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第9个按键处理")]
        public string Btn09ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第10个按键内容")]
        public string Btn10Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第10个按键处理")]
        public string Btn10ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第11个按键内容")]
        public string Btn11Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第11个按键处理")]
        public string Btn11ActionKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第12个按键内容")]
        public string Btn12Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [ExcelField("第12个按键处理")]
        public string Btn12ActionKey { get; set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}