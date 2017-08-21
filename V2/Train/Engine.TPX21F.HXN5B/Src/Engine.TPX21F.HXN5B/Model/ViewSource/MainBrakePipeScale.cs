namespace Engine.TPX21F.HXN5B.Model.ViewSource
{
    public class MainBrakePipeScale : LineTargetDistanceScale
    {
        public MainBrakePipeScale()
        {
            MaxValue = 800;
            ValueCount = 800 / 50;
            LongScalValueStep = 100;
            TextValueStep = 200;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainBrakePipeScale Instance = new MainBrakePipeScale();
    }

    public class MainBrakePipeInMonitorScale : LineTargetDistanceScale
    {
        public MainBrakePipeInMonitorScale()
        {
            MaxValue = 80;
            ValueCount = 80 / 5;
            LongScalValueStep = 10;
            TextValueStep = 20;
            UpdateTargitDistanceScaleItems();
        }

        public static readonly MainBrakePipeInMonitorScale Instance = new MainBrakePipeInMonitorScale();
    }
}