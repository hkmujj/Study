using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.CTCS
{
    public class CTCS3 : CTCSBase
    {
        public CTCS3(ATPDomainBase atpDomain)
            : base(atpDomain)
        {
            Type = CTCSType.CTCS3;
        }
    }
}