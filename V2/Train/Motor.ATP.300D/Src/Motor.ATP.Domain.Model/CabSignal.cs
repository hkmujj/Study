using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class CabSignal : ATPPartialBase, ICabSignal
    {
        public CabSignal(ATPDomainBase parent)
            : base(parent)
        {
        }

        /// <summary>
        /// 类型
        /// </summary>
        public CabSignalCode Code { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEffective { get; set; }
    }
}
