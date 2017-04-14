using System;
using System.Drawing;

namespace MMITool.Common.Util
{
    /// <summary>
    /// StringExtension
    /// </summary>
    public static class StringExtension
    {

        /// <summary>
        ///  == null || == Empty || == WhiteSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                if (string.Empty == value.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// AsDecimal
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static decimal AsDecimal(this string inputValue)
        {
            decimal result;
            if (!decimal.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsDateTime
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string inputValue)
        {
            DateTime result;
            if (!DateTime.TryParse(inputValue, out result))
            {
                result = DateTime.MinValue;
            }
            return result;
        }

        /// <summary>
        /// AsFloat
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static float AsFloat(this string inputValue)
        {
            float result;
            if (!float.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsDouble
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static double AsDouble(this string inputValue)
        {
            double result;
            if (!double.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsInt
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static int AsInt(this string inputValue)
        {
            int result;
            if (!int.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsByte
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static byte AsByte(this string inputValue)
        {
            byte result;
            if (!byte.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsSbyte
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static sbyte AsSbyte(this string inputValue)
        {
            sbyte result;
            if (!sbyte.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsShort
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static short AsShort(this string inputValue)
        {
            short result;
            if (!short.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsUshort
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static ushort AsUshort(this string inputValue)
        {
            ushort result;
            if (!ushort.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsUint
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static uint AsUint(this string inputValue)
        {
            uint result;
            if (!uint.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }


        /// <summary>
        /// AsLong
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static long AsLong(this string inputValue)
        {
            long result;
            if (!long.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsUlong
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static ulong AsUlong(this string inputValue)
        {
            ulong result;
            if (!ulong.TryParse(inputValue, out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// AsChar
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static char AsChar(this string inputValue)
        {
            char result;
            if (!char.TryParse(inputValue, out result))
            {
                result = '\0';
            }
            return result;
        }

        /// <summary>
        /// AsBool
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static bool AsBool(this string inputValue)
        {
            bool result;
            if (!bool.TryParse(inputValue, out result))
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// AsColor
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static Color AsColor(this string inputValue)
        {
            try
            {
                var converter = new ColorConverter();
                return (Color)converter.ConvertFromString(inputValue);
            }
            catch
            {
                return new Color();
            }
        }

        /// <summary>
        /// AsPoint
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static Point AsPoint(this string inputValue)
        {
            try
            {
                var data = inputValue.Split(new[] {','});
                return new Point(data[0].AsInt(), data[1].AsInt());
            }
            catch (Exception)
            {
                return new Point();
            }
        }

        /// <summary>
        /// AsPointF
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static PointF AsPointF(this string inputValue)
        {
            try
            {
                var data = inputValue.Split(new[] { ',' });
                return new PointF(data[0].AsFloat(), data[1].AsFloat());
            }
            catch (Exception)
            {
                return new PointF();
            }
        }

        /// <summary>
        /// AsSize
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static Size AsSize(this string inputValue)
        {
            try
            {
                var data = inputValue.Split(new[] { ',' });
                return new Size(data[0].AsInt(), data[1].AsInt());
            }
            catch (Exception)
            {
                return new Size();
            }
        }

        /// <summary>
        /// AsSizeF
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static SizeF AsSizeF(this string inputValue)
        {
            try
            {
                var data = inputValue.Split(new[] { ',' });
                return new SizeF(data[0].AsFloat(), data[1].AsFloat());
            }
            catch (Exception)
            {
                return new SizeF();
            }
        }

        /// <summary>
        /// AsRectangle
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static Rectangle AsRectangle(this string inputValue)
        {
            try
            {
                var data = inputValue.Split(new[] { ',' });
                return new Rectangle(data[0].AsInt(), data[1].AsInt(),data[2].AsInt(), data[3].AsInt());
            }
            catch (Exception)
            {
                return new Rectangle();
            }
        }

        /// <summary>
        /// AsRectangleF
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static RectangleF AsRectangleF(this string inputValue)
        {
            try
            {
                var data = inputValue.Split(new[] { ',' });
                return new RectangleF(data[0].AsFloat(), data[1].AsFloat(), data[2].AsFloat(), data[3].AsFloat());
            }
            catch (Exception)
            {
                return new RectangleF();
            }
        }
    }
}
