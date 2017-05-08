namespace Engine.TCMS.Turkmenistan.Model
{
    internal class TargitDistanceScale120 : TargitDistanceScale
    {
        public TargitDistanceScale120()
        {
            MaxtValue = 120;
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
            MaxtValue = 1200;
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
}