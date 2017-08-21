using CommonUtil.Util;

namespace Subway.ShenZhenLine11.Extention
{
    public static class StringExtention
    {
        public static int ToInt(this string value)
        {
            return (int)value.ToDouble();
        }

        public static double ToDouble(this string value)
        {
            double result = 0;
            if (!double.TryParse(value, out result))
            {
                AppLog.Info($"{value} {nameof(ToDouble)} 失败！");

            }

            return result;
        }

    }
}