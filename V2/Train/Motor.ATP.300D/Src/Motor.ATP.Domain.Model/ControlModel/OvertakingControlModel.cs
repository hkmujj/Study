using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.ControlModel
{
    public class OvertakingControlModel : ControlModelBase
    {
        public OvertakingControlModel(ATPDomainBase atpDomain)
            : base(atpDomain)
        {
            Type = ControlType.Overtaking;
        }
    }
}