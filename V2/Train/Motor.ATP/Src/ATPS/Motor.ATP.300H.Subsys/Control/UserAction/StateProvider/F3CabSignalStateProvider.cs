using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.StateProvider
{
    public class F3CabSignalStateProvider : DriverSelectableItemStateProviderBase
    {

        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            ATP.RegionFStateProvier.TrainIdStateProvider.PropertyChanged += ShuntingStateProviderOnPropertyChanged;
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.TrainIdStateProvider.Enabled;
        }

        private void ShuntingStateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateState();
        }


        public override void UpdateState()
        {
            UpdateEnable();
            UpdateContent();
        }

        private void UpdateContent()
        {
            ChangedContent = ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState == EnterOrQuit.Enter ? string.Empty : "ÍË³ö»úÐÅ";
        }
    }
}