using System;
using MMI.Facility.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction.UpdateStateParam;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDriverInterfaceController : IService, IATPPartial
    {
        event Action<IDriverInterfaceController, IUpdateDriverInterfaceParam, IDriverInterface, IDriverInterface> CurrentInterfaceChanged;

        event Func<IDriverInterfaceController, IUpdateDriverInterfaceParam, IDriverInterface, IDriverInterface, bool> CurrentInterfaceChanging;

            /// <summary>
        /// 接口工厂 
        /// </summary>
        IDriverInterfaceFactory DriverInterfaceFactory { get; }

        /// <summary>
        /// 当前接口
        /// </summary>
        IDriverInterface CurrentInterface { get; }

        /// <summary>
        /// 界面显示
        /// </summary>
        IDriverInterfaceView DriverInterfaceView { get; }

        /// <summary>
        /// 更新数据接口
        /// </summary>
        void UpdateDriverInterface(IDriverInterface driverInterface, IUpdateDriverInterfaceParam updateParam = null);

        /// <summary>
        /// 更新数据接口
        /// </summary>
        void UpdateDriverInterface(DriverInterfaceKey interfaceKey, IUpdateDriverInterfaceParam updateParam = null);

        /// <summary>
        /// 在按键响应完后更新接口
        /// </summary>
        void UpdateDriverInterfaceAfterButtonResponsed(DriverInterfaceKey interfaceKey, IUpdateDriverInterfaceParam updateParam = null);

        /// <summary>
        /// 在按键响应完后更新接口
        /// </summary>
        /// <param name="driverInterface"></param>
        /// <param name="updateParam"></param>
        void UpdateDriverInterfaceAfterButtonResponsed(IDriverInterface driverInterface, IUpdateDriverInterfaceParam updateParam = null);

    }
}