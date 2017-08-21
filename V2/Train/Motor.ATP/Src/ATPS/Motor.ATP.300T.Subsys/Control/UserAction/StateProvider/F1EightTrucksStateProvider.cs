using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.UserAction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Motor.ATP._300T.Subsys.Control.UserAction.StateProvider
{
    public class F1EightTrucksStateProvider : DriverSelectableItemStateProviderBase
    {
        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>().Subscribe(OnSelectedTrainData);
            ATP.RegionFStateProvier.EightTrucksStateProvider.PropertyChanged += EightTrucksStateProviderOnPropertyChanged;
        }

        private IEventAggregator m_EventAggregator;
        protected IEventAggregator EventAggregator
        {
            get
            {
                return m_EventAggregator ?? (m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>());
            }
        }

        private void OnSelectedTrainData(DriverInputEventArgs<DriverInputTrainData> driverInputEventArgs)
        {
            switch (driverInputEventArgs.SelectedContent.InputtedTrainData[0])
            {
                case "":
                    OutlineVisible = false;
                    break;
                case "8辆":
                    OutlineVisible = true;
                    break;
                case "16辆":
                    OutlineVisible = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EightTrucksStateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.EightTrucksStateProvider.Enabled;
        }

        public override void UpdateState()
        {
            UpdateEnable();
        }
    }
}
