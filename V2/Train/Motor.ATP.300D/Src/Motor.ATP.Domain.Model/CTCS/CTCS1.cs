using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.CTCS
{
    public class CTCS1 : CTCSBase
    {
        public CTCS1(ATPDomainBase atpDomain)
            : base(atpDomain)
        {
            Type = CTCSType.CTCS1;
        }
    }
}