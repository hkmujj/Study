using System;

namespace Motor.ATP._200H.Common
{
    class DateTimeExpression : IFloatValueExpression
    {
        public object Interprete(float[] values)
        {
            var date = Convert.ToInt32(values[0]);
            var time = Convert.ToInt32(values[1]);



            var dt = new DateTime(Math.Min(9999, Math.Max(date/10000, 1)), Math.Min(12, Math.Max(1, date%10000/100)),
                Math.Min(60, Math.Max(date%100, 1)), Math.Min(23, Math.Max(0, time/10000)),
                Math.Min(59, Math.Max(0, time%10000/100)), Math.Min(59, Math.Max(0, time%100)));

            return dt;
        }
    }
}