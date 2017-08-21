using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Message;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F5DataDownActionResponser : F5OrdinaryActionResponser
    {
        public F5DataDownActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseDown()
        {
            ((Message)ATP.Message).NavigateDown();
        }
    }
}