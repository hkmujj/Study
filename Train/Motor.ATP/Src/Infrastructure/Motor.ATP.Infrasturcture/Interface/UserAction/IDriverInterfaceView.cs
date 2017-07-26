using System;
using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDriverInterfaceView
    {
        /// <summary>
        /// 按下
        /// </summary>
        event EventHandler<DriverActionEventArgs> MouseDownEvent;

        /// <summary>
        /// 弹起
        /// </summary>
        event EventHandler<DriverActionEventArgs> MouseUpEvent;

        /// <summary>
        /// 点击
        /// </summary>
        event EventHandler<DriverActionEventArgs> MouseClickEvent;

        /// <summary>
        /// 更新界面
        /// </summary>
        void UpdateView(IDriverInterface driverInterface);
    }

    /// <summary>
    /// DriverInterfaceView
    /// </summary>
    public interface IDriverInterfaceViewProvider
    {
        /// <summary>
        /// DriverInterfaceView
        /// </summary>
        IDriverInterfaceView DriverInterfaceView { get; }
    }

    /// <summary>
    /// DriverActionEventArgs
    /// </summary>
    public class DriverActionEventArgs : EventArgs
    {
        /// <summary>
        /// 后台数据模型
        /// </summary>
        public object DataSource { private set; get; }

        /// <summary>
        /// 作用类型，即按键类型 
        /// </summary>
        public Enum ActionType { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="dataSource"></param>
        [DebuggerStepThrough]
        public DriverActionEventArgs(Enum actionType, object dataSource)
        {
            DataSource = dataSource;
            ActionType = actionType;
        }
    }
}