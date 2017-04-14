using System;
using System.Diagnostics;
using MMI.YDCommunicationWrapper;

namespace MMI.Communacation.Interface.AppLayer
{
    public class NetCommandEventArgs : EventArgs
    {
        [DebuggerStepThrough]
        public NetCommandEventArgs(Command command = default(Command), byte[] rawData = null)
        {
            RawData = rawData;
            Command = command;
        }

        public Command Command { get; private set; }

        public byte[] RawData { private set; get; }
    }
}