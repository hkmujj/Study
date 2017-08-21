using System.ComponentModel;
using System.ComponentModel.Composition;
using CommonUtil.Util;
using Engine.TAX2.SS7C.Events;
using Engine.TAX2.SS7C.Model.Domain.Constant;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX;
using Engine.TAX2.SS7C.ViewModel.Domain;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller.Domain.TAX2
{
    [Export]
    public class TAXController : ControllerBase<TAX2ViewModel>
    {
        private CurrentTAXStateChangedEvent m_CurrentTAXStateChangedEvent;

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            m_CurrentTAXStateChangedEvent =
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<CurrentTAXStateChangedEvent>();

            ViewModel.CheckPantographViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
            ViewModel.CheckSonaViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
            ViewModel.CheckSoundViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
            ViewModel.CheckTMISViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
            ViewModel.CheckTrackViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
            ViewModel.CheckTrainControlViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
            ViewModel.CheckWalkViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
            ViewModel.CheckDMISViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
        }

        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName ==
                PropertySupport.ExtractPropertyName<TAXModelBase, TAX2CommunicationState>(t => t.TAX2CommunicationState))
            {
                if (ViewModel.Model.CurrentSelectedModel == sender && ViewModel.Model.CurrentSelectedModel != null)
                {
                    m_CurrentTAXStateChangedEvent.Publish(
                        new CurrentTAXStateChangedEvent.Args(ViewModel.Model.CurrentSelectedModel));
                }
            }
        }
    }
}