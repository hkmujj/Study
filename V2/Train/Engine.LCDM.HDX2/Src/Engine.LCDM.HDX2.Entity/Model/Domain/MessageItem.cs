using System;
using System.ComponentModel;
using System.Diagnostics;
using Excel.Interface;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    [ExcelLocation("HXD2制动屏消息表.xls", "Sheet1")]
    public class MessageItem : ISetValueProvider
    {
        public MessageItem(int logicIndex = Int32.MaxValue, string nameCH = null, string contentCH = null,
            string nameEN = null, string contentEN = null)
        {
            LogicIndex = logicIndex;
            NameCH = nameCH;
            ContentCH = contentCH;
            NameEN = nameEN;
            ContentEN = contentEN;
        }

        public MessageItem()
        {
            
        }

        [ExcelField("bool逻辑位", true)]
        public int LogicIndex { private set; get; }

        [ExcelField("消息名字_CH")]
        public string NameCH { private set; get; }

        [ExcelField("消息内容_CH")]
        public string ContentCH { private set; get; }

        [ExcelField("消息名字_EN")]
        public string NameEN { private set; get; }

        [ExcelField("消息内容_EN")]
        public string ContentEN { private set; get; }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "LogicIndex":
                    LogicIndex = Convert.ToInt32(value);
                    break;
                case "NameCH":
                    NameCH = value;
                    break;
                case "ContentCH":
                    ContentCH = value;
                    break;
                case "NameEN":
                    NameEN = value;
                    break;
                case "ContentEN":
                    ContentEN = value;
                    break;
            }
        }
    }
}