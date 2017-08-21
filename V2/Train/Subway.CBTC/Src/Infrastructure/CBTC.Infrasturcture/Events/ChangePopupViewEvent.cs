using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace CBTC.Infrasturcture.Events
{
    public class ChangePopupViewEvent : CompositePresentationEvent<ChangePopupViewEvent.EventArgs>
    {
        public class EventArgs
        {
            public string RegionName { private set; get; }

            public Uri TargetUri { private set; get; }

            [DebuggerStepThrough]
            public EventArgs(string regionName, Uri targetUri)
            {
                TargetUri = targetUri;
                RegionName = regionName;
            }
        }
    }
}