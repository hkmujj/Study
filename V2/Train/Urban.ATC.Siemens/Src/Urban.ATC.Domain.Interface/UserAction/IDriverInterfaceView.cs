using System;
using System.Diagnostics;

namespace Motor.ATP.Domain.Interface.UserAction
{
    public interface IDriverInterfaceView
    {
        event EventHandler<DriverActionEventArgs> MouseDownEvent;

        event EventHandler<DriverActionEventArgs> MouseUpEvent;

        event EventHandler<DriverActionEventArgs> MouseClickEvent;

        /// <summary>
        /// 更新界面
        /// </summary>
        void UpdateView(IDriverInterface driverInterface);
    }

    public interface IDriverInterfaceViewProvider
    {
        IDriverInterfaceView DriverInterfaceView { get; }
    }

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

        [DebuggerStepThrough]
        public DriverActionEventArgs(Enum actionType, object dataSource)
        {
            DataSource = dataSource;
            ActionType = actionType;
        }
    }
}