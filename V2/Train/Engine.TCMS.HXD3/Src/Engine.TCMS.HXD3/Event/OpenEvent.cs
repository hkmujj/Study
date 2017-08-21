using System.Collections.Generic;
using System.Diagnostics;
using Engine.TCMS.HXD3.Model.TCMS.Domain;
using Microsoft.Practices.Prism.Events;

namespace Engine.TCMS.HXD3.Event
{
    public class OpenEvent : CompositePresentationEvent<OpenEvent.EventArgs>
    {
        public class EventArgs
        {
            [DebuggerStepThrough]
            public EventArgs(IEnumerable<OpenStateItem> selectedItems)
            {
                SelectedItems = selectedItems;
            }

            public IEnumerable<OpenStateItem> SelectedItems { private set; get; }
        }
    }
}