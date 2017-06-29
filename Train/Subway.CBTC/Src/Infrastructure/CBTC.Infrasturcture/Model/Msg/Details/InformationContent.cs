using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using CBTC.Infrasturcture.Model.Constant;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;

namespace CBTC.Infrasturcture.Model.Msg.Details
{
    [ExcelLocation("CBTC信号通用消息表.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    [DebuggerDisplay("Id={Id}, Content={Content}")]
    public class InformationContent : NotificationObject, IInformationContent, ISetValueProvider
    {

        public InformationContent()
        {
        }

        [DebuggerStepThrough]
        public InformationContent(int id, string happenDataKey, string content, string sendDataKey = null)
        {
            InformationShowType = InformationShowType.YellowFrameFlick;
            Content = content;
            HappenDataKey = happenDataKey;
            SendDataKey = sendDataKey;
            Id = id;
        }

        [ExcelField("Id")]
        public int Id { private set; get; }

        [ExcelField("HappenDataKey")]
        public string HappenDataKey { private set; get; }

        [ExcelField("Content")]
        public string Content { private set; get; }

        [ExcelField("SendDataKey")]
        public string SendDataKey { private set; get; }

        public InformationShowType InformationShowType { private set; get; }
        
        [ExcelField("MessageType")]
        public MessageType MessageType { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "MessageType":
                    MessageType type;
                    Enum.TryParse(value, true, out type);
                    MessageType = type;
                    break;
            }
        }
    }
}