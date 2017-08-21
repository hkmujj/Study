using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.CTCS
{
    public class CTCS2 : CTCSBase
    {
        public CTCS2(ATPDomainBase atpDomain)
            : base(atpDomain)
        {
            Type = CTCSType.CTCS2;
        }
    }
}