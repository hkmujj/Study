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
        public InformationContent(int id, string happenDataKey, string content, InformationShowType showType = InformationShowType.YellowFrameFlick, string sendDataKey = null)
        {
            InformationShowType = showType;
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

        public DateTime HappenDate { get; set; }

        [ExcelField("InformationShowType")]
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
                    MessageType msgType;
                    Enum.TryParse(value, true, out msgType);
                    MessageType = msgType;
                    break;

                case "InformationShowType":
                    InformationShowType InfoType;
                    Enum.TryParse(value, true, out InfoType);
                    InformationShowType = InfoType;
                    break;
            }
        }

        public object Clone()
        {
            var info = new InformationContent();
            info.Id = Id;
            info.HappenDataKey = HappenDataKey;
            info.Content = Content;
            info.SendDataKey = SendDataKey;
            info.InformationShowType = InformationShowType;
            info.MessageType = MessageType;
            return info;
        }
    }
}