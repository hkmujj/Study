using System;
using System.Security.Cryptography;

namespace ES.Facility.PublicModule.ExGks
{
    public class Char
    {
        #region 返回14位数字

        /// <summary>
        /// 返回一个2位数的字符
        /// </summary>
        /// <param name="tempStr"></param>
        /// <returns></returns>
        public static string Get2Id(string tempStr, int num)
        {
            while (tempStr.Length < num) tempStr = "0" + tempStr;
            tempStr = tempStr.Substring(0, num);
            return tempStr;
        }

        public static int Random()
        {
            var random = new Byte[4];
            var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(random);
            
            return BitConverter.ToInt32(random, 0);
        }

        /// <summary>
        /// 抱歉实际是返回的16位哈,哈哈哈哈
        /// </summary>
        /// <returns></returns>
        public static int Get9Id()
        {
            var thisIsNum = string.Empty;
            var thisran = new Random();


            thisIsNum = DateTime.Now.Year.ToString().Substring(3, 1) +
                        Get2Id(DateTime.Now.Month.ToString(), 1) +
                        Get2Id(DateTime.Now.Day.ToString(), 1) +
                        Get2Id(DateTime.Now.Hour.ToString(), 1) +
                        Get2Id(DateTime.Now.Minute.ToString(), 1) +
                        Get2Id(DateTime.Now.Second.ToString(), 1) +
                        Get2Id(DateTime.Now.Millisecond.ToString(), 1) +
                        Get2Id(Math.Abs(Random()).ToString(), 2);
                        
            return int.Parse(thisIsNum);
        }

        #endregion
    }
}
