namespace Engine.TCMS.Turkmenistan.Model
{
    internal class TargitDistanceScale120 : TargitDistanceScale
    {
        public TargitDistanceScale120()
        {
            MaxtValue = 112;
            TextStep = 20;
            LineCount = (int)MaxtValue / 2;
            LongLineStep = 20;
            InitItems();
        }
        public static TargitDistanceScale120 Instance = new TargitDistanceScale120();
        public override double GetDgreeLenth(double distance)
        {
            if (distance % 20 == 0)
            {
                return 0.9;
            }
            if (distance % 10 == 0)
            {
                return 0.5;
            }
            return 0.3;
        }
    }
    internal class TargitDistanceScale1200 : TargitDistanceScale
    {
        public TargitDistanceScale1200()
        {
            MaxtValue = 1120;
            TextStep = 200;
            LineCount = (int)MaxtValue / 20;
            LongLineStep = 200;
            InitItems();
        }
        public static TargitDistanceScale1200 Instance = new TargitDistanceScale1200();
        public override double GetDgreeLenth(double distance)
        {
            if (distance % 200 == 0)
            {
                return 0.9;
            }
            if (distance % 100 == 0)
            {
                return 0.5;
            }
            return 0.3;
        }
    }
    internal class TargitDistanceScale112 : TargitDistanceScale
    {
        public TargitDistanceScale112()
        {
            MaxtValue = 1120;
            TextStep = 200;
            LineCount = (int)MaxtValue / 20;
            LongLineStep = 200;
            InitItems();
        }
        public static TargitDistanceScale112 Instance = new TargitDistanceScale112();
        public override double GetDgreeLenth(double distance)
        {
            if (distance % 200 == 0)
            {
                return 0.9;
            }
            if (distance % 100 == 0)
            {
                return 0.5;
            }
            return 0.3;
        }

        public override string GetDistanceText(double distance)
        {
            if ((int)distance % TextStep == 0)
            {
                if (distance < float.Epsilon || distance == MaxtValue)
                {
                    return string.Empty;
                }
                return (distance / 1000d).ToString("F1");
            }
            return string.Empty;
        }
    }
    internal class TargitDistanceScale9 : TargitDistanceScale
    {
        public TargitDistanceScale9()
        {
            MaxtValue = 9000;
            TextStep = 2000;
            LineCount = (int)MaxtValue / 200;
            LongLineStep = 2000;
            InitItems();
        }
        public static TargitDistanceScale9 Instance = new TargitDistanceScale9();
        public override double GetDgreeLenth(double distance)
        {
            if (distance % 2000 == 0)
            {
                return 0.9;
            }
            if (distance % 1000 == 0)
            {
                return 0.5;
            }
            return 0.3;
        }

        public override string GetDistanceText(double distance)
        {
            if ((int)distance % TextStep == 0)
            {
                if (distance < float.Epsilon || distance == MaxtValue)
                {
                    return string.Empty;
                }
                return (distance / 1000d).ToString("F0");
            }
            return string.Empty;
        }
    }

}