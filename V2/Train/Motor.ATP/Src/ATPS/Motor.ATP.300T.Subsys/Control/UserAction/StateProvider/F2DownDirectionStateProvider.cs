using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Motor.ATP.Infrasturcture.Interface;
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
    public class F2DownDirectionStateProvider : DriverSelectableItemStateProviderBase
    {
        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>().Subscribe(OnSelectedFreq);
            ATP.RegionFStateProvier.DownFreqStateProvider.PropertyChanged += DownFreqStateProviderOnPropertyChanged;
        }

        private IEventAggregator m_EventAggregator;
        protected IEventAggregator EventAggregator
        {
            get
            {
                return m_EventAggregator ?? (m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>());
            }
        }

        private void OnSelectedFreq(DriverInputEventArgs<DriverInputFreq> driverInputEventArgs)
        {
            switch (driverInputEventArgs.SelectedContent.InputtedTrainFreq)
            {
                case TrainFreq.Unkown:
                    OutlineVisible = false;
                    break;
                case TrainFreq.Up:
                    OutlineVisible = false;
                    break;
                case TrainFreq.Down:
                    OutlineVisible = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DownFreqStateProviderOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.DownFreqStateProvider.Enabled;
        }

        //private void UpdateOutlineVisible()
        //{
        //    OutlineVisible = false;
        //}

        public override void UpdateState()
        {
            UpdateEnable();
        }
    }
}
