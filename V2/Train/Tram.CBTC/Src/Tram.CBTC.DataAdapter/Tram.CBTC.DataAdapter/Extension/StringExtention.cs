using System;

namespace Tram.CBTC.DataAdapter.Extension
{
    public static class StringExtention
    {
        public static DateTime ToDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value) || value == "0")
            {
                return new DateTime();
            }
            return new DateTime(Convert.ToInt32(value));
        }
    }
}