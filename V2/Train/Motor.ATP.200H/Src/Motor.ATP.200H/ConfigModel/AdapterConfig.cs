namespace Motor.ATP._200H.ConfigModel
{
    public class AdapterConfig
    {
        public AdapterConfig()
        {
            CurrentSpeedCoefficient = 1f;
            TargetSpeedCoefficient = 1f;
            PermitSpeedCoefficient = 1f;
            NormalBrakeSpeedCoefficient = 1f;
            EmergBrakeSpeedCoefficient = 1f;
        }

        public float CurrentSpeedCoefficient { get; set; }
        public float TargetSpeedCoefficient { get; set; }
        public float PermitSpeedCoefficient { get; set; }
        public float NormalBrakeSpeedCoefficient { get; set; }
        public float EmergBrakeSpeedCoefficient { get; set; }
    }
}