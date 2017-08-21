namespace Subway.ATC.Casco.Common
{
    /// <summary>
    /// 闪烁
    /// </summary>
    public class FlashTimer
    {
        /// <summary>
        /// 闪烁用递增量
        /// </summary>
        int flashCount = 0;

        /// <summary>
        /// 闪烁间隔时间
        /// </summary>
        int flashTime = 0;

        public FlashTimer(int interval)
        {
            flashTime = interval;
        }

        /// <summary>
        /// 闪烁判断
        /// </summary>
        /// <returns></returns>
        public bool IsNeedFlash()
        {
            if (flashCount < flashTime * 5)
            {
                ++flashCount;
                return true;
            }
            else if (flashCount >= flashTime * 5 && flashCount < flashTime * 10)
            {
                ++flashCount;
                return false;
            }
            else
            {
                flashCount = 0;
                return false;
            }
        }
    }

}
