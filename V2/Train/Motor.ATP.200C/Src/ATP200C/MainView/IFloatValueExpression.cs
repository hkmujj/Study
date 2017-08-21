using System;
using System.Globalization;
using System.Text;

namespace ATP200C.MainView
{
    interface IFloatValueExpression
    {
        /// <summary>
        /// 解释 数据的意义
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        object Interprete(float[] values);
    }

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

                info.TrainId = sb.Append((Math.Truncate(values[1])).ToString("####")).ToString();

                info.DriverId = ((Math.Truncate(values[2])).ToString("#######"));
            }
            catch (Exception e)
            {
                return info;
            }
            //BitConverter
            return info;
        }
    }
}
