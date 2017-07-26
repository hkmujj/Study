using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F8VigilantActionResponser : DriverActionResponserBase
    {
        public F8VigilantActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F8)
        {
        }


        public override void ResponseMouseClick()
        {
            ATP.SendInterface.SendAlert();
        }
    }
}