using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.Common.Train
{
    /// <summary>
    /// 车的 ID 和显示的string之间的转换
    /// </summary>
    public class TrainIDConvert
    {
        public static string Convert(int id)
        {
            switch (id)
            {
                case 0:
                    return "01";
                case 1:
                    return "02";
                case 2:
                    return "03";
                case 3:
                    return "04";
                case 4:
                    return "05";
                case 5:
                    return "06";
                case 6:
                    return "07";
                case 7:
                    return "00";
                default:
                    throw new Exception(string.Format("There is no train where id is 【{0}】.", id));
            }
        }
    }
}
