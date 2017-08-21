using System;
using CommonUtil.Util;
using MMI.Communacation.Interface.ProtocolLayer;
using MMI.YDCommunicationWrapper;

namespace MMI.Communacation.Control.ProtocolLayer
{
    public class CommandTypeInterpreter : ICommandTypeInterpreter
    {
        public CommandType InterpreterCommandType(byte[] data)
        {
            return (CommandType)BitConverter.ToInt16(data, 0);
        }

        public Command InterpreterCommand(byte[] data)
        {
            return (Command)BytesStructConverter.ByteToStruct(data, typeof(Command));
        }
    }
}