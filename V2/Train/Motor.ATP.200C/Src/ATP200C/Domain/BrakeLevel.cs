namespace ATP200C.Domain
{
    public enum BrakeLevel
    {
        None = 0,

        Brake1 = 1,

        Brake4 = 2,

        Brake7 = 3,

        /// <summary>
        /// 紧急制动
        /// </summary>
        EmergencyBrake = 4,

        /// <summary>
        /// 允许缓解
        /// </summary>
        AllowEase = 5
    }

    public static class BrakeLeveExtension
    {
        /// <summary>
        /// 是否触发常用制动
        /// </summary>
        /// <param name="brake"></param>
        /// <returns></returns>
        public static bool HasServiceBrake(this BrakeLevel brake)
        {
            return brake >= BrakeLevel.Brake1 && brake <= BrakeLevel.Brake7;
        }

        /// <summary>
        /// 是否触发最大常用制动
        /// </summary>
        /// <param name="brake"></param>
        /// <returns></returns>
        public static bool HasMaxServiceBrake(this BrakeLevel brake)
        {
            return brake == BrakeLevel.Brake7;
        }

        /// <summary>
        /// 是否触发紧急制动
        /// </summary>
        /// <param name="brake"></param>
        /// <returns></returns>
        public static bool HasEmergenceBrake(this BrakeLevel brake)
        {
            return brake == BrakeLevel.EmergencyBrake;
        }

    }
}