using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class ATPExtension
    {
        /// <summary>
        /// GetInterfaceController
        /// </summary>
        /// <param name="atp"></param>
        /// <returns></returns>
        public static IDriverInterfaceController GetInterfaceController(this IATP atp)
        {
            return App.Current.ServiceManager.GetService<IDriverInterfaceController>(atp.ATPType.ToString());
        }

        /// <summary>
        /// UpdateDriverInterface
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="interfaceKey"></param>
        public static void UpdateDriverInterface(this IATP atp, string interfaceKey)
        {
            atp.UpdateDriverInterface(DriverInterfaceKey.Parser(interfaceKey));
        }

        /// <summary>
        /// UpdateDriverInterface
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="interfaceKey"></param>
        public static void UpdateDriverInterface(this IATP atp, DriverInterfaceKey interfaceKey)
        {
            atp.GetInterfaceController().UpdateDriverInterface(interfaceKey);
        }
    }
}