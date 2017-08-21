using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class DriverInterfaceControllerExtension
    {
        /// <summary>
        /// UpdateDriverInterface
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="interfaceKey"></param>
        public static void UpdateDriverInterface(this IDriverInterfaceController controller, string interfaceKey)
        {
            controller.UpdateDriverInterface(DriverInterfaceKey.Parser(interfaceKey));
        }

        /// <summary>
        /// 在按键响应完后更新接口
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="interfaceKey"></param>
        public static void UpdateDriverInterfaceAfterButtonResponsed(this IDriverInterfaceController controller, string interfaceKey)
        {
            controller.UpdateDriverInterfaceAfterButtonResponsed(DriverInterfaceKey.Parser(interfaceKey));
        }
    }
}