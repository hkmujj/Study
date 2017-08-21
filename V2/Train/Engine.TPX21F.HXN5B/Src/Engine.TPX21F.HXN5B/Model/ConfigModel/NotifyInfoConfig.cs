using System;
using System.Diagnostics;
using CommonUtil.Util;
using Excel.Interface;

namespace Engine.TPX21F.HXN5B.Model.ConfigModel
{
    [ExcelLocation("Engine.TPX21F.HXN5B故障和提示信息.xls", "Sheet1")]
    [DebuggerDisplay("InfoType={InfoType}, Index={Index}, Content={Content}")]
    public class NotifyInfoConfig : ISetValueProvider
    {
        public NotifyInfoConfig()
        {
            
        }

        public NotifyInfoConfig(string content, int index = -1, NotifyInfoType infoType = NotifyInfoType.Fault)
        {
            Content = content;
            Index = index;
            InfoType = infoType;
        }

        [ExcelField("内容")]
        public string Content { private set; get; }

        [ExcelField("逻辑索引")]
        public int Index { get; private set; }

        [ExcelField("类型")]
        public NotifyInfoType InfoType { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "InfoType":
                    try
                    {
                        InfoType = (NotifyInfoType) Enum.Parse(typeof(NotifyInfoType), value);
                    }
                    catch (Exception e)
                    {
                        AppLog.Error("Can not parser {0} to type {1}, {2}", value, typeof(NotifyInfoType), e);
                    }
                    break;
            }
        }

    }
}