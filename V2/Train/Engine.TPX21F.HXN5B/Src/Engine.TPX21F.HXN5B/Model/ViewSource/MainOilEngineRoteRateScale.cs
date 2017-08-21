namespace Engine.TPX21F.HXN5B.Model.ViewSource
{
    public class MainOilEngineRoteRateScale : LineTargetDistanceScale
    {
        public MainOilEngineRoteRateScale()
        {
            MaxValue = 1200;
            ValueCount = 1200/40;
            LongScalValueStep = 200;
            TextValueStep = 400;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainOilEngineRoteRateScale Instance = new MainOilEngineRoteRateScale();
    }
}