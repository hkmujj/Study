using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.ControlModel
{
    public class StandByControlModel : ControlModelBase
    {
        public StandByControlModel(ATPDomainBase atpDomain)
            : base(atpDomain)
        {
            Type = ControlType.StandBy;
        }
    }
}