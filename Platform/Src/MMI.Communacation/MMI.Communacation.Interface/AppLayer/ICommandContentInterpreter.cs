using MMI.YDCommunicationWrapper;

namespace MMI.Communacation.Interface.AppLayer
{
    /// <summary>
    /// 命令内容解释 
    /// </summary>
    public interface ICommandContentInterpreter
    {
        object Interpreter(Command data);
    }

    /// <summary>
    /// 命令内容解释 
    /// </summary>
    public interface ICommandContentInterpreter<out T> : ICommandContentInterpreter
    {
        new T Interpreter(Command data);
    }
}