namespace Motor.TCMS.CRH400BF.Model.ViewSource
{
    class MainNetVRoteRateScale : LineTargetDistanceScaleSpecial
    {

        public MainNetVRoteRateScale()
        {
            MinValue = 18;
            MaxValue = 32;
            ValueCount = 32-18 / 1;
            LongScalValueStep = 5;
            TextValueStep = 5;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainNetVRoteRateScale Instance = new MainNetVRoteRateScale();
    }
}
