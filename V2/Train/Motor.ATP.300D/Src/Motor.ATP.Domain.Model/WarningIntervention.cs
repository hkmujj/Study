
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{

    public class WarningIntervention : ATPPartialBase, IWarningIntervention
    {
        public WarningIntervention(ATPDomainBase parent) : base(parent)
        {
        }

        /// <summary>
        /// 报警时间, 单位 : 秒
        /// </summary>
        public double WarningTime { get; set; }

        /// <summary>
        /// 目标距离, 单位 : 米
        /// </summary>
        public double TargetDistance { get; set; }
    }
}