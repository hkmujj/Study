namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 
    /// </summary>
    public static class DriverInterfaceControllerExtension
    {
        /// <summary>
        /// ���˵���һ��
        /// </summary>
        /// <param name="controller"></param>
        public static void GoBack(this IDriverInterfaceController controller)
        {
            controller.UpdateDriverInterface(controller.CurrentInterface.LastInterfaceStack.Peek());
        }

        /// <summary>
        /// ������ROOT���
        /// </summary>
        /// <param name="controller"></param>
        public static void GotoRoot(this IDriverInterfaceController controller)
        {
            controller.UpdateDriverInterface(DriverInterfaceKey.Root);
        }
    }
}