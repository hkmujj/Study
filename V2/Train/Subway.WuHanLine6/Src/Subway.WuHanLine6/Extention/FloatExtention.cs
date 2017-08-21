namespace Subway.WuHanLine6.Extention
{
    /// <summary>
    /// Float转换扩展
    /// </summary>
    public static class FloatExtention
    {
        /// <summary>
        /// 转换为Int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this float value)
        {
            return (int)value;
        }
    }
}