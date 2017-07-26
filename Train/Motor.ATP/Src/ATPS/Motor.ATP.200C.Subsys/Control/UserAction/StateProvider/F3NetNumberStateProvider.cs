using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.StateProvider
{
    public class F3NetNumberStateProvider : DriverSelectableItemStateProviderBase
    {
        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            ATP.RegionFStateProvier.NetNumberStateProvider.PropertyChanged += DriverIdStateProviderOnPropertyChanged;
        }

        private void DriverIdStateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.NetNumberStateProvider.Enabled;
        }

        public override void UpdateState()
        {
            UpdateEnable();
        }
    }
}