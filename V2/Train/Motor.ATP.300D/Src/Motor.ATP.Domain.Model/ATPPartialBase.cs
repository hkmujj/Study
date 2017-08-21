using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public abstract class ATPPartialBase :  IATPPartial
    {
        protected ATPPartialBase(ATPDomainBase parent)
        {
            Parent = parent;
        }

        public ATPDomainBase  Parent { set; get; }

        IATP IATPPartial.Parent { get { return Parent; } }
    }
}
