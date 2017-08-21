using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.ControlModel
{
    public abstract class ControlModelBase : ATPPartialBase, IControlModel
    {
        protected ControlModelBase(ATPDomainBase parent)
            : base(parent)
        {
        }

        public ControlType Type { get; internal protected set; }

        /// <summary>
        /// 准备进入的IControlModel
        /// </summary>
        public IControlModel NextControlModel { get; set; }

        /// <summary>
        /// 已经生效
        /// </summary>
        public bool InEffect { get; set; }
    }
}
