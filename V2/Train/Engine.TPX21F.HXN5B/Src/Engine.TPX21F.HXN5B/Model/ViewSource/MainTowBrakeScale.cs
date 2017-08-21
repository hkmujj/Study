namespace Engine.TPX21F.HXN5B.Model.ViewSource
{
    public class MainTowBrakeScale : LineTargetDistanceScale
    {
        public MainTowBrakeScale()
        {
            MaxValue = 600;
            ValueCount = 600 / 50;
            LongScalValueStep = 100;
            TextValueStep = 200;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainTowBrakeScale Instance = new MainTowBrakeScale();
    }
}