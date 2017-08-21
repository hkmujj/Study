using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.CTCS
{
    public class CTCS0 : CTCSBase
    {
        public CTCS0(ATPDomainBase atpDomain)
            : base(atpDomain)
        {
            Type = CTCSType.CTCS0;
        }
    }
}