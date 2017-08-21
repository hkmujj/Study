using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.ControlModel
{
    public class FullSupervisionControlModel : ControlModelBase
    {
        public FullSupervisionControlModel(ATPDomainBase atpDomain)
            : base(atpDomain)
        {
            Type = ControlType.FullSupervision;
        }
    }
}