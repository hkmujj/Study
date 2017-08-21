using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.StateProvider
{
    public class F2OnSightStateProvider : DriverSelectableItemStateProviderBase
    {

        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            ATP.RegionFStateProvier.OnSightStateProvider.PropertyChanged += ShuntingStateProviderOnPropertyChanged;
        }

        private void ShuntingStateProviderOnPropertyChanged(object sender,
            PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.OnSightStateProvider.Enabled;
        }


        public override void UpdateState()
        {
            UpdateEnable();
            UpdateContent();
        }

        private void UpdateContent()
        {
            ChangedContent = ATP.RegionFStateProvier.OnSightStateProvider.EnterOrQuitState == EnterOrQuit.Enter ? string.Empty : "ÍË³öÄ¿ÊÓ";
        }
    }
}