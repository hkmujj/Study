using System;
using System.Diagnostics;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public class Message
    {
        [DebuggerStepThrough]
        public Message(MessageItem content)
        {
            Content = content;
        }

        public DateTime StartTime { set; get; }

        public DateTime EndTime { set; get; }

        public MessageItem Content { private set; get; }
    }
}