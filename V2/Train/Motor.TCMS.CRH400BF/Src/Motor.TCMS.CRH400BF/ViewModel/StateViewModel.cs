using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Controller;
using Motor.TCMS.CRH400BF.Model;

namespace Motor.TCMS.CRH400BF.ViewModel
{
    /// <summary>
    /// 状态机
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateViewModel:NotificationObject
    {
        private StateModel m_Model;
        private StateController m_Controller;

        [DebuggerStepThrough]
        [ImportingConstructor]
        public StateViewModel(StateModel model, StateController controller  )
        { 
            Model = model;
            Controller = controller;
            controller.ViewModel = this;

        }

        public IRegionManager RegionManager { get { return DomainViewModel.RegionManager; } }

        public DomainViewModel DomainViewModel { get; set; }


        public StateController Controller
        {
            private set
            {
                if (Equals(value, m_Controller))
                    return;

                m_Controller = value;
                RaisePropertyChanged(() => Controller);
            }
            get { return m_Controller; }
        }

        public StateModel Model
        {
            private set
            {
                if (Equals(value, m_Model))
                    return;

                m_Model = value;
                RaisePropertyChanged(() => Model);
            }
            get { return m_Model; }
        }
    }
}