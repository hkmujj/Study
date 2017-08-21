namespace Motor.HMI.CRH1A.Config.ConfigModel
{
    public class AdaptConfig
    {
        public AdaptConfig()
        {
            CurrentSpeedCoefficient = 1f;
            LimitSpeedCoefficient = 1f;
        }

        public float CurrentSpeedCoefficient { get; set; }

        public float LimitSpeedCoefficient { set; get; }
    }
}