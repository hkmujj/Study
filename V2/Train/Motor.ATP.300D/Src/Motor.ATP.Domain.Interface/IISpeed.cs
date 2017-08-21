namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 速度信息
    /// </summary>
    public interface IISpeed : ITrainInfoPartial
    {
        /// <summary>
        /// 当前速度
        /// </summary>
        float CurrentSpeed { get; }

        /// <summary>
        /// 目标速度
        /// </summary>
        float TargetSpeed { get; }

        /// <summary>
        /// SBI  最大常用制动速度 
        /// </summary>
        float ServiceBrakeInterventionSpeed { get; }

        /// <summary>
        /// EBI 紧急制动速度
        /// </summary>
        float EmergencyBrakeInterventionSpeed { get; }

        /// <summary>
        /// P 允许速度
        /// </summary>
        float PermittedLimitSpeed { get; }

        /// <summary>
        /// 警报速度
        /// </summary>
        float WarningLimitSpeed { get; }

    }
}