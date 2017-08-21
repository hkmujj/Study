using System.Collections.Generic;

namespace Subway.CBTC.BeiJiaoKong.Models.RegionA
{
    public class GraduationResouce
    {
        public static GraduationResouce Instance = new GraduationResouce();
        public Dictionary<double, double> Graduation = new Dictionary<double, double>();
        public const double MaxPix = 321;

        private GraduationResouce()
        {
            Graduation.Add(1, 1);
            Graduation.Add(2, 289d / MaxPix);
            Graduation.Add(5, 243d / MaxPix);
            Graduation.Add(10, 209d / MaxPix);
            Graduation.Add(20, 174d / MaxPix);
            Graduation.Add(50, 133d / MaxPix);
            Graduation.Add(100, 97d / MaxPix);
            Graduation.Add(200, 63d / MaxPix);
            Graduation.Add(500, 19d / MaxPix);
            Graduation.Add(750, 0d / MaxPix);
        }

        public double GetScal(double current)
        {
            double ret = 0d;

            if (current > float.Epsilon && current <= 1)
            {
                ret = Graduation[1];
            }
            if (current > 1 && current <= 2)
            {
                ret = Graduation[1] - (Graduation[1] - Graduation[2]) * ((current - 1) / 1d); //Graduation[1] - (current - 1) / 1d;
            }
            if (current > 2 && current <= 5)
            {
                ret = Graduation[2] - (Graduation[2] - Graduation[5]) * ((current - 2) / 3d);
            }
            if (current > 5 && current <= 10)
            {
                ret = Graduation[5] - (Graduation[5] - Graduation[10]) * ((current - 5) / 5d);
            }
            if (current > 10 && current <= 20)
            {
                ret = Graduation[10] - (Graduation[10] - Graduation[20]) * ((current - 10) / 10d);
            }
            if (current > 20 && current <= 50)
            {
                ret = Graduation[20] - (Graduation[20] - Graduation[50]) * ((current - 20) / 30d);
            }
            if (current > 50 && current <= 100)
            {
                ret = Graduation[50] - (Graduation[50] - Graduation[100]) * ((current - 50) / 50d);
            }
            if (current > 100 && current <= 200)
            {
                ret = Graduation[100] - (Graduation[100] - Graduation[200]) * ((current - 100) / 100d);
            }
            if (current > 200 && current <= 500)
            {
                ret = Graduation[200] - (Graduation[200] - Graduation[500]) * ((current - 200) / 300d);
            }
            if (current > 500 && current <= 750)
            {
                ret = Graduation[500] - (Graduation[500] - Graduation[750]) * ((current - 500) / 250d);
            }
            return ret;
        }
    }
}