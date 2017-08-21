using System;
using System.Windows.Forms;

namespace MMI.Facility.Interface.Flow
{
    /// <summary>
    ///  流程控制。
    /// </summary>
    public interface IFlowController
    {
        /// <summary>
        /// 流程开始
        /// </summary>
        void Run();

        /// <summary>
        /// 流程初始化完成的事件
        /// </summary>
        event EventHandler<FlowInitializeCompletedEventArgs> FlowInitializeCompletedEvent;

        /// <summary>
        /// 主窗口关闭事件
        /// </summary>
        event FormClosedEventHandler FormClosed;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler Load;

    }
}
