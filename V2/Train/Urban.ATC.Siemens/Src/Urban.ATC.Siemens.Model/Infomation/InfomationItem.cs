using System;
using System.Diagnostics;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Infomation;

namespace Motor.ATP.Domain.Model.Infomation
{
    public class InfomationItem : IInfomationItem
    {
        [DebuggerStepThrough]
        public InfomationItem(IInfomationItemContent content)
        {
            Content = content;
        }

        public IInfomationItemContent Content { get; private set; }

        public DateTime StartTime { get; set; }

        object IIdentityProvider.Identity { get { return Identity; } }

        public IInfomationItemContent Identity { get { return Content; } }
    }

    [DebuggerDisplay("Id ={Id} Priority = {Priority} MessageContent = {MessageContent} PopupTitle = {PopupTitle}")]
    public class InfomationItemContent : IInfomationItemContent
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public InfomationShowType ShowType { get; set; }
        public InfomationSystemResonseType SystemResonseType { get; set; }
        public InfomationResponseType ResponseType { get; set; }
        public InfomationAutoDeleteType AutoCancelType { get; set; }
        public string MessageContent { get; set; }
        public string PopupTitle { get; set; }
        public string PopupContent { get; set; }
        public int PopupContentCodePage { get; set; }
        public int OkReturnIndex { get; set; }
        public int CancelReturnIndex { get; set; }
        public string PopupContentRtf { set; get; }


        [DebuggerStepThrough]
        public InfomationItemContent()
        {
            OkReturnIndex = int.MaxValue;
            CancelReturnIndex = int.MaxValue;
            Priority = int.MaxValue;
        }
    }
}