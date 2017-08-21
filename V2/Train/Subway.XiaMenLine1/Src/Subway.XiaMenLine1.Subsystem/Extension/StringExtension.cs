namespace Subway.XiaMenLine1.Subsystem.Extension
{
    public static class StringExtension
    {
        public static bool ToBool(this string value)
        {
            return value.ToInt() == 1;
        }

        public static int ToInt(this string value)
        {
            return (int)value.ToDouble();
        }

        public static double ToDouble(this string value)
        {
            return double.Parse(value);
        }
    }
}