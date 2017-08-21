using MMI.Facility.Interface.Service;

namespace Motor.ATP.Domain.Interface.UserAction
{
    public interface IDriverInterfaceController : IService, IATPPartial
    {
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
        void UpdateDriverInterface(IDriverInterface driverInterface);

        /// <summary>
        /// 更新数据接口
        /// </summary>
        void UpdateDriverInterface(DriverInterfaceKey interfaceKey);

    }
}