namespace Motor.TCMS.CRH400BF.Model.ViewSource
{
    class MainTowRoteRateScale : LineTargetDistanceScale
    {

        public MainTowRoteRateScale()
        {
            MaxValue = 10;
            ValueCount = 10 / 1;
            LongScalValueStep = 20;
            TextValueStep = 10;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainTowRoteRateScale Instance = new MainTowRoteRateScale();
    }
}
