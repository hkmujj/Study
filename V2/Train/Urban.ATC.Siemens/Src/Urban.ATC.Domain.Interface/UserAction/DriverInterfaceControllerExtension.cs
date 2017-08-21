namespace Motor.ATP.Domain.Interface.UserAction
{
    public static class DriverInterfaceControllerExtension
    {
        /// <summary>
        /// 回退到上一步
        /// </summary>
        /// <param name="controller"></param>
        public static void GoBack(this IDriverInterfaceController controller)
        {
            controller.UpdateDriverInterface(controller.CurrentInterface.LastInterfaceStack.Peek());
        }

        /// <summary>
        /// 导航到ROOT结点
        /// </summary>
        /// <param name="controller"></param>
        public static void GotoRoot(this IDriverInterfaceController controller)
        {
            controller.UpdateDriverInterface(DriverInterfaceKey.Root);
        }
    }
}