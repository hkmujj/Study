using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.StateProvider
{
    public class F8CancelTrainDataStateProvider : DriverSelectableItemStateProviderBase
    {
        public F8CancelTrainDataStateProvider()
        {
            Enabled = false;
        }
    }
}