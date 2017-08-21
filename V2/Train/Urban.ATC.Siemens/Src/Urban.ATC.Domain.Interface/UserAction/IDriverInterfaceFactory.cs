using MMI.Facility.Interface.Service;

namespace Motor.ATP.Domain.Interface.UserAction
{
    public interface IDriverInterfaceFactory : IService
    {
        /// <summary>
        /// 获得一个用户接口
        /// </summary>
        /// <param name="interfaceKey"></param>
        /// <returns></returns>
        IDriverInterface GetOrCreateDriverInterface(DriverInterfaceKey interfaceKey);
    }
}