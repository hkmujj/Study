namespace Subway.CBTC.THALES.Model
{
    internal class TargitDistanceScale120 : TargitDistanceScale
    {
        public TargitDistanceScale120()
        {
            MaxtValue = 1000;
            TextStep = 20;
            LineCount = 10;
            LongLineStep = 20;
            InitItems();
        }
        public static TargitDistanceScale120 Instance = new TargitDistanceScale120();
        public override double GetDgreeLenth(double distance)
        {
            return 0.5;
            //if (distance % 20 == 0)
            //{
            //    return 0.9;
            //}
            //if (distance % 10 == 0)
            //{
            //    return 0.5;
            //}
            //return 0.3;
        }
    }
}
