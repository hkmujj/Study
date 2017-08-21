using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.ControlModel
{
    public class UnkownControlModel : ControlModelBase
    {
        public UnkownControlModel(ATPDomainBase atpDomain)
            : base(atpDomain)
        {
            Type = ControlType.Unknown;
        }
    }
}