using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Model.BtnStragy;
using Engine.TCMS.HXD3.ViewModel;
using Engine.TCMS.HXD3.ViewModel.Domain;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.TCMS
{
    [Export]
    public class HXD3TCMSModel : NotificationObject
    {
        private IStateInterface m_CurrentStateInterface;
        private Other m_Other;
        public IEventAggregator EventAggregator { get; private set; }

        [ImportingConstructor]
        public HXD3TCMSModel(TCMSViewModel tcmsView, Other ot, IEventAggregator eventAggregator)
        {

            Other = ot;
            EventAggregator = eventAggregator;
        }

        public HXD3TCMSViewModel Parent { get; set; }



        public IStateInterface CurrentStateInterface
        {
            private set
            {
                if (Equals(value, m_CurrentStateInterface))
                {
                    return;
                }

                m_CurrentStateInterface = value;
                m_CurrentStateInterface.UpdateState();
                EventAggregator.GetEvent<ViewChangedEvent>().Publish(value);
                RaisePropertyChanged(() => CurrentStateInterface);
            }
            get { return m_CurrentStateInterface; }
        }

        public void UpdateCurrentState(IStateInterface current)
        {
            CurrentStateInterface = current;
        }

        public Other Other
        {
            private set
            {
                if (Equals(value, m_Other))
                {
                    return;
                }

                m_Other = value;
                RaisePropertyChanged(() => Other);
            }
            get { return m_Other; }
        }
    }
}