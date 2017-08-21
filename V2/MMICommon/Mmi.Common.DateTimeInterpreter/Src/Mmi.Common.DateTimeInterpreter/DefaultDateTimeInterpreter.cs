using System;
using System.ComponentModel;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CommonUtil.Util;

namespace Mmi.Common.DateTimeInterpreter
{
    internal class DefaultDateTimeInterpreter : IDateTimeInterpreter
    {
        public DateTime Interpreter(params float[] rawData)
        {
            if (rawData.Length < 2)
            {
                throw new ArgumentException("rawData count must larger than 2");
            }

            var dateBytes = BitConverter.GetBytes(rawData[0]);
            var timeBytes = BitConverter.GetBytes(rawData[1]);

            var date = BitConverter.ToInt32(dateBytes, 0);
            var time = BitConverter.ToInt32(timeBytes, 0);

            var dt = DateTime.Now;
            try
            {
                //dt = new DateTime((int)dateBytes[0] * 100 + dateBytes[1], dateBytes[2], dateBytes[3], timeBytes[0], timeBytes[1], timeBytes[2]);
                dt = new DateTime(date/10000, date%10000/100, date%100, time/10000, time%10000/100, time%100);
            }
            catch (Exception e)
            {
                LogMgr.Error(
                    "Can not parser dateTime, where Type={0}, date={1},datebytes={2}, time={3},timebytes={4}, intdate={5}, inttime={6} ,error={7}",
                    RawDataType.DateTime,
                    rawData[0], string.Join(",", dateBytes), rawData[1], string.Join(",", timeBytes), date, time, e);
            }

            return dt;
        }

        public static void Test()
        {
            var fs = new float[]
            {BitConverter.ToSingle(new byte[4] {1, 2, 3, 4}, 0), BitConverter.ToSingle(new byte[4] {10, 22, 03, 03}, 0)};

            var iter = new DefaultDateTimeInterpreter();

            var dt = iter.Interpreter(fs);

            var date = 20161011;
            var time = 131513;
            //var dt = DateTime.MinValue;
            try
            {
                //dt = new DateTime((int)dateBytes[0] * 100 + dateBytes[1], dateBytes[2], dateBytes[3], timeBytes[0], timeBytes[1], timeBytes[2]);
                var d1 = date/10000;
                var d2 = date%10000/100;
                var d3 = date%100;
                var d4 = time/10000;
                var d5 = time%10000/100;
                var d6 = time%100;
                dt = new DateTime(date / 10000, date % 10000 / 100, date % 100, time / 10000, time % 10000 / 100, time % 100);
            }
            catch (Exception e)
            {
                //LogMgr.Error(
                //    "Can not parser dateTime, where Type={0}, date={1},datebytes={2}, time={3},timebytes={4}, intdate={5}, inttime={6} ,error={7}",
                //    RawDataType.DateTime,
                //    rawData[0], string.Join(",", dateBytes), rawData[1], string.Join(",", timeBytes), date, time, e);
            }
        }
    }
}