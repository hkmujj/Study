namespace Motor.TCMS.CRH400BF.Model.ViewSource
{
    class MainNetVRoteRateScaleSpecial : LineTargetDistanceScale
    {

        public MainNetVRoteRateScaleSpecial()
        {
            MinValue = 0;
            MaxValue = 18;
            ValueCount = 18 / 1;
            LongScalValueStep = 17;
            TextValueStep = 5;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainNetVRoteRateScaleSpecial Instance = new MainNetVRoteRateScaleSpecial();
    }
}
