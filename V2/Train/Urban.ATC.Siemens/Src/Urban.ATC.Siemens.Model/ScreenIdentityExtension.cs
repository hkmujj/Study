using MMI.Facility.Interface;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public static class ScreenIdentityHelper
    {
        /// <summary>
        /// 创建一个id
        /// </summary>
        /// <param name="baseClass"></param>
        /// <returns></returns>
        public static ScreenIdentity CreateIdentity(baseClass baseClass)
        {
            return new ScreenIdentity(baseClass.RecPath, null, null);
        }
    }
}