using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.TCMS.Domain;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class OpenStateViewModel : NotificationObject
    {
        public DelegateCommand OpenCommand { private set; get; }

        public ObservableCollection<OpenStateItem> StateItems { private set; get; }

        private readonly IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public OpenStateViewModel(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
            OpenCommand = new DelegateCommand(OnOpen);
            StateItems =
                new ObservableCollection<OpenStateItem>(
                    GlobalParam.Instance.OpenStateConfigs.Select(s => new OpenStateItem(s)));
        }

        private void OnOpen()
        {
            m_EventAggregator.GetEvent<OpenEvent>().Publish(new OpenEvent.EventArgs(StateItems.Where(w => w.IsSelected)));
        }
    }
}