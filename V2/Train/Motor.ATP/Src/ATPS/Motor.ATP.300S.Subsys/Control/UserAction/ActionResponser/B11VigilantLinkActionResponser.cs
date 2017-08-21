using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class B11VigilantLinkActionResponser : B11OrdinaryActionResponser
    {


        public B11VigilantLinkActionResponser(IDriverSelectableItem item) : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            ATP.SendInterface.SendAlert();
        }
    }
}