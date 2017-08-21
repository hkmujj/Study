using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.CTCS
{
    public abstract class CTCSBase : ATPPartialBase, ICTCS
    {
        protected CTCSBase(ATPDomainBase parent) : base(parent)
        {
        }

        public CTCSType Type { get; internal protected set; }

        /// <summary>
        /// 准备进入的CTCS
        /// </summary>
        public ICTCS NextCTCS { get; set; }

        /// <summary>
        /// 已经生效
        /// </summary>
        public bool InEffect { get; set; }
    }
}
