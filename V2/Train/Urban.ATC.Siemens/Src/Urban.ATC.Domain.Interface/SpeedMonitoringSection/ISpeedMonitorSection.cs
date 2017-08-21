namespace Motor.ATP.Domain.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 速度监视
    /// </summary>
    public interface ISpeedMonitoringSection : IATPPartial, IVisibility
    {
        /// <summary>
        /// 监视类型
        /// </summary>
        SpeedMonitoringSectionType Type { get; }

        /// <summary>
        /// 监视区坐标系
        /// </summary>
        IPlanSectionCoordinate PlanSectionCoordinate { get; }


        bool ZoomVisible { get; }

        /// <summary>
        /// 速度曲线
        /// </summary>
        ISpeedCurve SpeedCurve { get; }

        /// <summary>
        /// 起模点, 制动曲线区域起始点
        /// </summary>
        double BrakingStartPoint { get; }
    }
}
