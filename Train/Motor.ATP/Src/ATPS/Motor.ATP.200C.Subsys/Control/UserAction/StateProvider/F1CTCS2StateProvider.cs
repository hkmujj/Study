using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.StateProvider
{
    public class F1CTCS2StateProvider : DriverSelectableItemStateProviderBase
    {

        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            ATP.RegionFStateProvier.CTCS2StateProvider.PropertyChanged += ShuntingStateProviderOnPropertyChanged;
        }

        private void ShuntingStateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.CTCS2StateProvider.Enabled;
        }


        public override void UpdateState()
        {
            UpdateEnable();
        }
    }
}