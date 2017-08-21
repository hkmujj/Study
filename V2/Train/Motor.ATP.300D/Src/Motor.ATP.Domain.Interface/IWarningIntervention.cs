namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 报警介入
    /// </summary>
    public interface IWarningIntervention : IATPPartial
    {
        /// <summary>
        /// 报警时间, 单位 : 秒
        /// </summary>
        double WarningTime { get; }

        /// <summary>
        /// 目标距离, 单位 : 米
        /// </summary>
        double TargetDistance { get; }
    }
}
