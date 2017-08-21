using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Tram.CBTC.Infrasturcture.Events
{
    public class InputWordEvent: CompositePresentationEvent<InputWordEvent.EventArgs>
    {
        [DebuggerDisplay("control={Control}, Content={Content}")]
        public class EventArgs
        {
            [DebuggerStepThrough]
            public EventArgs(string content, Type control = Type.InputString)
            {
                Control = control;
                Content = content;
            }

            public Type Control { get; private set; }

            public string Content { get; private set; }
        }

        public enum Type
        {
            InputString,
            OK,
            Delete,
        }
    }
}