using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.Infrasturcture.Interface.Extension
{
    public static class InfomationTimeShowTypeExtension
    {
        /// <summary>
        /// 时间是否从现在开始
        /// </summary>
        /// <param name="showType"></param>
        /// <returns></returns>
        public static bool IsTimeFromNow(this InfomationTimeShowType showType)
        {
            return showType == InfomationTimeShowType.Normal || showType == InfomationTimeShowType.TimeFromOccuse;
        }

        /// <summary>
        /// 是否需要计时
        /// </summary>
        /// <param name="showType"></param>
        /// <returns></returns>
        public static bool NeedTime(this InfomationTimeShowType showType)
        {
            return showType == InfomationTimeShowType.TimeFromOccuse || showType == InfomationTimeShowType.TimeFrom0;
        }
    }
}