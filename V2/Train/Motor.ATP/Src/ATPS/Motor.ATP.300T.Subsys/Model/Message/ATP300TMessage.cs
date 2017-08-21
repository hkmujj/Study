using System;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP._300T.Subsys.Model.Message
{
    public class ATP300TMessage : Infrasturcture.Model.Message.Message
    {
        public ATP300TMessage(ATPDomain atp, Predicate<IMessageItem> showingMessageFilter = null)
            : base(atp, showingMessageFilter)
        {
        }

        protected override void InfomationServiceOnInfomationEnd(IInfomationItem infomationItem)
        {
            base.InfomationServiceOnInfomationEnd(infomationItem);

            if (infomationItem.Content.IsOnlyTextInfo() && infomationItem.Content.NextControlType != ControlType.Unknown)
            {
                Parent.ControlModel.NextControlType = ControlType.Unknown;
                Parent.ControlModel.NextControlTypeVisible = false;
            }
        }

        protected override void InfomationServiceOnInfomationBegin(IInfomationItem infomationItem)
        {
            base.InfomationServiceOnInfomationBegin(infomationItem);

            if (infomationItem.Content.IsOnlyTextInfo() && infomationItem.Content.NextControlType != ControlType.Unknown)
            {
                Parent.ControlModel.NextControlType = infomationItem.Content.NextControlType;
                Parent.ControlModel.NextControlTypeVisible = true;
            }
        }
    }
}