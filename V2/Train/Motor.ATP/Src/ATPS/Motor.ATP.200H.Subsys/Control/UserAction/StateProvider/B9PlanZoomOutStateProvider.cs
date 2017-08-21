using System.ComponentModel;
using CommonUtil.Util;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.StateProvider
{
    public class B9PlanZoomOutStateProvider : DriverSelectableItemStateProviderBase
    {
        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            ATP.SpeedMonitoringSection.PropertyChanged += SpeedMonitoringSectionOnPropertyChanged;
            UpdateEnable();

        }

        private void UpdateEnable()
        {
            Enabled = ATP.SpeedMonitoringSection.PlanSectionCoordinate.CanZoomOut;
        }

        private void SpeedMonitoringSectionOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<ISpeedMonitoringSection, IPlanSectionCoordinate>(a => a.PlanSectionCoordinate))
            {
                UpdateEnable();
            }
        }
    }
}