using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ATP200C
{
    public class CommonConvertTo
    {
        public static float ConverToFloat(string str)
        {
            var ch = str.ToCharArray();
            var strs = (from c in ch where char.IsLower(c) || char.IsUpper(c) select c.ToString().ToUpper().ToCharArray()).Aggregate(string.Empty, (current, cs) => current + Convert.ToByte(cs[0]));
            // var strs = ch.Aggregate(string.Empty, (current, c) => current + Convert.ToByte(c).ToString("00"));
            return float.Parse(strs);
        }

        public static string ConverToString(float fPara)
        {
            var str = fPara.ToString(CultureInfo.InvariantCulture);
            var retunstr = string.Empty;
            var temp = str.Length >= 2 ? str.Substring(str.Length - 2, 2):"";

            var bytes=new byte[1];
            bytes[0] = (byte)Convert.ToInt32(temp);
            str = temp.Length>=2 ? str.Substring(0, str.Length - 2) : string.Empty;
            retunstr = Encoding.Default.GetString(bytes) + retunstr;
            while (str.Length >= 2)
            {
                temp = str.Substring(str.Length - 2, 2);
                bytes[0] = (byte)Convert.ToInt32(temp);
                str = temp.Length >= 2 ? str.Substring(0, str.Length - 2) : string.Empty;
                retunstr = Encoding.Default.GetString(bytes) + retunstr;
            }
            return  retunstr;
        }
    }
}