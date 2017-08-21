using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace MMITool.Addin.YdConfigCreater.Events
{
    public class UpdateFooterTextEvent : CompositePresentationEvent<UpdateFooterTextEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(string text)
            {
                Text = text;
            }

            public string Text { get; private set; }
        }
    }
}