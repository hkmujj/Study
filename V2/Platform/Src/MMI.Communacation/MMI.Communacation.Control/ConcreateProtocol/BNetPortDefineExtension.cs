using MMI.Facility.Interface.Data.Config;

namespace MMI.Communacation.Control.ConcreateProtocol
{
    internal static class BNetPortDefineExtension
    {
        /// <summary>
        /// 获得真实的端口号
        /// </summary>
        /// <param name="port"></param>
        /// <param name="portType"></param>
        /// <returns></returns>
        public static int GetActurePort(this BNetPortDefine port, BNetPortType portType)
        {
            var isCmd = portType == BNetPortType.Commnad;
            switch (port)
            {
                case BNetPortDefine.Port_1000:
                    return isCmd ? 59395 : 59651;
                case BNetPortDefine.Port_2000:
                    return isCmd ? 53255 : 53511;
                case BNetPortDefine.Port_3000:
                    return isCmd ? 47115 : 47371;
                case BNetPortDefine.Port_4000:
                    return isCmd ? 41231 : 41487;
                    //默认使用Port_1000端口
                default:
                    return isCmd ? 59395 : 59651;
            }
        }

        public static int GetPortNumber(this BNetPortDefine port)
        {
            int tmpPort;
            switch (port)
            {
                case BNetPortDefine.Port_1000: tmpPort = 1000; break;
                case BNetPortDefine.Port_2000: tmpPort = 2000; break;
                case BNetPortDefine.Port_3000: tmpPort = 3000; break;
                case BNetPortDefine.Port_4000: tmpPort = 4000; break;
                default: tmpPort = 1000; break;
            }
            return tmpPort;
        }
    }
}