using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8OkActionResponser : OkActionResponser
    {
        public F8OkActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F8)
        {
        }
    }
}