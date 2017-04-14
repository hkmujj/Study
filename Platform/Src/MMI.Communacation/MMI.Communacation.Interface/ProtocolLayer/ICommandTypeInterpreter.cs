using MMI.YDCommunicationWrapper;

namespace MMI.Communacation.Interface.ProtocolLayer
{
    /// <summary>
    /// 命令类型解析
    /// </summary>
    public interface ICommandTypeInterpreter
    {
        CommandType InterpreterCommandType(byte[] data);

        Command InterpreterCommand(byte[] data);
    }
}