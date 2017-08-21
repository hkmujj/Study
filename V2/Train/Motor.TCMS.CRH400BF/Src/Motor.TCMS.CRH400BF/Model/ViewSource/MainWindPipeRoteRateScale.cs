namespace Motor.TCMS.CRH400BF.Model.ViewSource
{
    public class MainWindPipeRoteRateScale : LineTargetDistanceScale
    {
        public MainWindPipeRoteRateScale()
        {
            MaxValue = 1300;
            ValueCount = 1300 / 100;
            LongScalValueStep = 200;
            TextValueStep = 200;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainWindPipeRoteRateScale Instance = new MainWindPipeRoteRateScale();
    }
}
