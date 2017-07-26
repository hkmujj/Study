using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.StateProvider
{
    public class F8CancelFreqStateProvider : DriverSelectableItemStateProviderBase
    {
        public F8CancelFreqStateProvider()
        {
            Enabled = false;
        }
    }
}