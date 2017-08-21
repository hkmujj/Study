using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.CTCS
{
    public class UnkownCTCS : CTCSBase
    {
        public UnkownCTCS(ATPDomainBase atpDomain)
            : base(atpDomain)
        {
            Type = CTCSType.Unkown;
        }
    }
}
