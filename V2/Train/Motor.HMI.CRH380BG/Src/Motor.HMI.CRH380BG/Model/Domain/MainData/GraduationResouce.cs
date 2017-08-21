using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Model.Domain.MainData
{
    public class GraduationResouce
    {
        public static GraduationResouce Instance = new GraduationResouce();
        public Dictionary<double, double> Graduation = new Dictionary<double, double>();
        public const double MaxPix = 357;

        private GraduationResouce()
        {
            Graduation.Add(0, 1);
            Graduation.Add(17, 315d / MaxPix);
            Graduation.Add(32, 0d / MaxPix);
        }

        public double GetScal(double current)
        {
            double ret = 0d;
            if (current > float.Epsilon && current <= 0)
            {
                ret = Graduation[0];
            }
            if (current > 0 && current <= 17)
            {
                ret = Graduation[0] - (Graduation[0] - Graduation[17]) * ((current - 0 ) / 17d);
            }
            if (current > 17 && current <= 32)
            {
                ret = Graduation[17] - (Graduation[17] - Graduation[32]) * ((current - 17) / 15d);
            }
            return ret;
        }
    }
}
