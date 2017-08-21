namespace Motor.TCMS.CRH400BF.Model.ViewSource
{
    public class MainNetARoteRateScale : LineTargetDistanceScale
    {
        public MainNetARoteRateScale()
        {
            MaxValue = 800;
            ValueCount = 800 / 20;
            LongScalValueStep = 100;
            TextValueStep = 200;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainNetARoteRateScale Instance = new MainNetARoteRateScale();
    }
}
