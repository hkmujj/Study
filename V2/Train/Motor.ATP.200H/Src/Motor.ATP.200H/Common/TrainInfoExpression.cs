using System;
using System.Globalization;
using System.Text;

namespace Motor.ATP._200H.Common
{
    class TrainInfoExpression : IFloatValueExpression
    {

        public object Interprete(float[] values)
        {
            var info = new TrainInfo();

            if (values.Length != 3)
            {
                return info;
            }

            try
            {
                var sb = new StringBuilder();

                var head = Convert.ToChar(BitConverter.GetBytes(values[0])[0]);

                var c = Convert.ToChar(head);
                if (Char.IsLower(c) || Char.IsUpper(c))
                {
                    sb.Append(c.ToString(CultureInfo.InvariantCulture).ToUpper());
                }

                info.TrainID = sb.Append((Math.Truncate( values[1])).ToString("####")).ToString();

                info.DriverID = ((Math.Truncate( values[2])).ToString("#######"));
            }
            catch (Exception)
            {
                return info;
            }
            //BitConverter
            return info;
        }
    }
}