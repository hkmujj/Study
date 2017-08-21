using System;
using System.Diagnostics;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Infomation;

namespace Urban.Domain.TrainState.Model.Infomation
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
        public DateTime FixTime { get; set; }
        public DateTime EndTime { get; set; }

        object IIdentityProvider.Identity
        {
            get { return Identity; }
        }

        public IInfomationItemContent Identity { get { return Content; } }
    }
}