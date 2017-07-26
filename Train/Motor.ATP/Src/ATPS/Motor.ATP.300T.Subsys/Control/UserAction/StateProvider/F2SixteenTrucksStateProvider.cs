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
    public class F2SixteenTrucksStateProvider : DriverSelectableItemStateProviderBase
    {
        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>().Subscribe(OnSelectedTrainData);
            ATP.RegionFStateProvier.SixteenTrucksStateProvider.PropertyChanged += SixteenTrucksStateProviderOnPropertyChanged;
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
                    OutlineVisible = false;
                    break;
                case "16辆":
                    OutlineVisible = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SixteenTrucksStateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.SixteenTrucksStateProvider.Enabled;
        }

        public override void UpdateState()
        {
            UpdateEnable();
        }
    }
}
