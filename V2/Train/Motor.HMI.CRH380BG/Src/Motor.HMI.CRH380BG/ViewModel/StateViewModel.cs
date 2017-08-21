using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.Controller;
using Motor.HMI.CRH380BG.Model;

namespace Motor.HMI.CRH380BG.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateViewModel : NotificationObject
    {
        private StateController m_Controller;

        [ImportingConstructor]
        [DebuggerStepThrough]
        public StateViewModel(StateModel model, StateController controller)
        {
            Model = model;
            Controller = controller;

            controller.ViewModel = this;
            model.ViewModel = this;
        }

        public IRegionManager RegionManager { get { return DomainViewModel.RegionManager; } }

        public CRH380BGViewModel DomainViewModel { get; set; }

        public StateModel Model { get; private set; }

        public StateController Controller
        {
            get { return m_Controller; }
            private set
            {
                if (Equals(value, m_Controller))
                {
                    return;
                }
                m_Controller = value;
                RaisePropertyChanged(() => Controller);
            }
        }
    }
}