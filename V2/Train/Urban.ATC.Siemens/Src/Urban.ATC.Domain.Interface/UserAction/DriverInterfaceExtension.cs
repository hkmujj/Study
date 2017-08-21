namespace Motor.ATP.Domain.Interface.UserAction
{
    public static class DriverInterfaceExtension
    {
        /// <summary>
        /// 查找 root 节点
        /// </summary>
        /// <param name="driverInterface"></param>
        /// <returns></returns>
        public static IDriverInterface FindRoot(this IDriverInterface driverInterface)
        {
            if (driverInterface.Id.Paths == DriverInterfaceKey.EmptyPaths)
            {
                return driverInterface;
            }
            var tmp = driverInterface;
            while (tmp.Parent.Id.Paths != DriverInterfaceKey.EmptyPaths)
            {
                tmp = tmp.Parent;
            }
            return tmp;
        }
    }
}