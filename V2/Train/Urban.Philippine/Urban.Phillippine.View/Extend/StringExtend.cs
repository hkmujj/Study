namespace Urban.Phillippine.View.Extend
{
    public static class StringExtend
    {
        public static int ConvertToInt(this string cValue)
        {
            return (int)cValue.ConvertToDouble(); ;
        }

        public static double ConvertToDouble(this string cValue)
        {
            double result = 0;
            double.TryParse(cValue, out result);
            return result;
        }
    }
}