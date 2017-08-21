using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Message;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F4DataUpActionResponser : F4OrdinaryActionResponser
    {
        public F4DataUpActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseDown()
        {
            ((Message)ATP.Message).NavigateUp();
        }
    }
}