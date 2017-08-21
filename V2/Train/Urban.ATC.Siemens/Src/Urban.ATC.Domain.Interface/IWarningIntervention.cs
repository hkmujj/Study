using Motor.ATP.Domain.Interface.TargetDistance;

namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 报警介入
    /// </summary>
    public interface IWarningIntervention : IATPPartial, IVisibility
    {
        /// <summary>
        /// 目标距离刻度
        /// </summary>
        ITargitDistanceScale TargitDistanceScale { get; }

        /// <summary>
        /// 制动报警是否可见
        /// </summary>
        bool BrakeWarningVisible { get; }

        /// <summary>
        /// 制动报警等级
        /// </summary>
        BrakeWarningLevel BrakeWarningLevel { get; }

        /// <summary>
        /// 制动报警颜色
        /// </summary>
        ATPColor BrakeWaringColor { get; }

        /// <summary>
        /// 报警时间, 单位 : 秒
        /// </summary>
        double WarningTime { get; }

        /// <summary>
        /// 目标距离, 单位 : 米
        /// </summary>
        double TargetDistance { get; }

        bool TargetDistanceVisible { get; }
    }

    public enum BrakeWarningLevel
    {
        LevelUnkown,

        /// <summary>
        /// 无显示
        /// </summary>
        Level0,

        /// <summary>
        /// 1/4
        /// </summary>
        Level1,

        /// <summary>
        /// 1/2
        /// </summary>
        Level2,

        /// <summary>
        /// 3/4
        /// </summary>
        Level3,

        /// <summary>
        /// 满
        /// </summary>
        LevelFull,
    }
}
