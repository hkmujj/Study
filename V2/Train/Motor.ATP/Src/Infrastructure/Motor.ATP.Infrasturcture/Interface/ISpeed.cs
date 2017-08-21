using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 速度信息
    /// </summary>
    public interface ISpeed : ITrainInfoPartial, IVisibility
    {
        /// <summary>
        /// 
        /// </summary>
        ISpeedDialPlate SpeedDialPlate { get; }

        /// <summary>
        /// 当前速度
        /// </summary>
        ISpeedModel CurrentSpeed { get; }

        /// <summary>
        /// 目标速度
        /// </summary>
        ISpeedModel TargetSpeed { get; }

        /// <summary>
        /// SBI  最大常用制动速度 
        /// </summary>
        ISpeedModel ServiceBrakeInterventionSpeed { get; }

        /// <summary>
        /// EBI 紧急制动速度
        /// </summary>
        ISpeedModel EmergencyBrakeInterventionSpeed { get; }

        /// <summary>
        /// P 允许速度
        /// </summary>
        ISpeedModel PermittedLimitSpeed { get; }

        /// <summary>
        /// 警报速度
        /// </summary>
        ISpeedModel WarningLimitSpeed { get; }

        /// <summary>
        /// 干预速度 
        /// </summary>
        ISpeedModel IntervertionSpeed { get; }

    }
}