using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [DebuggerDisplay("LogicKey={LogicKey}, Contrnt={Content}")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    [ExcelLocation("HXD3TCMS主断下提示信息接口.xls", "Sheet1")]
    public class MainSwitchNotifyConfig: ISetValueProvider
    {
        [ExcelField("内容_CH")]
        public string ContentCh { get; private set; }

        [ExcelField("内容_EN")]
        public string ContentEn { get; private set; }

        [ExcelField("逻辑索引", true)]
        public int LogicKey { get; private set; }

        public string Content {get { return ContentCh; } }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}