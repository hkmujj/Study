using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Interface.InputGuide
{
    public interface IInputNode
    {
        /// <summary>
        /// 当前正在执行的输入 
        /// </summary>
        DriverInterfaceKey CurrentInputKey { get; }

        /// <summary>
        ///  上一个
        /// </summary>
        IInputNode PreviousNode { get; }


        /// <summary>
        /// 下一个节点
        /// </summary>
        IInputNode NextNode { get; }


        /// <summary>
        /// 是否需要输入
        /// </summary>
        bool IsNeedInput();


    }
}