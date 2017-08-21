using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.StateProvider
{
    public class F2TelNumberStateProvider : DriverSelectableItemStateProviderBase
    {
        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            ATP.RegionFStateProvier.TelNumberStateProvider.PropertyChanged += DriverIdStateProviderOnPropertyChanged;
        }

        private void DriverIdStateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.TelNumberStateProvider.Enabled;
        }

        public override void UpdateState()
        {
            UpdateEnable();
        }
    }
}