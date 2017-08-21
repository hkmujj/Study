using System;
using CommonUtil.Util;

namespace Mmi.Common.DateTimeInterpreter
{
    /// <summary>
    /// 秒转换为时间
    /// </summary>
    public class SecondDateTimeInterpreter : IDateTimeInterpreter

    {
        /// <summary>
        /// 解释 float 数组为时间
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public DateTime Interpreter(params float[] rawData)
        {

            var secFromHHmmss = ((int)rawData[0]);

            var hh = secFromHHmmss / 3600;

            var mm = (secFromHHmmss % 3600) / 60;

            var ss = secFromHHmmss % 3600 % 60;

            DateTime dt = DateTime.Now;
            try
            {
                dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, ss);
            }
            catch (Exception e)
            {
                LogMgr.Error(
                    "Can not parser dateTime, where Type={0},  hh={1} mm={2}  ss={3}",
                    RawDataType.DateTime,
                    hh, mm, ss);
            }


            return dt;
        }
    }
}
