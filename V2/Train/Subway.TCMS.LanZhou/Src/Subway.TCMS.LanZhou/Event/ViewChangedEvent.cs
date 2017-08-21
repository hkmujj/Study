using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Subway.TCMS.LanZhou.Event
{
    public class ViewChangedEvent: CompositePresentationEvent<ViewChangedEvent.Args>
    {
         public class Args
         {
            [DebuggerStepThrough]
             public Args(Type targetViewType)
             {
                 TargetViewType = targetViewType;
             }

             public Type TargetViewType { get; private set; }
         }
    }
}
