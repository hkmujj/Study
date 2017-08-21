using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Casco.Controller;
using Tram.CBTC.Infrasturcture.Model.Msg.Details;

namespace Tram.CBTC.Casco.Model
{
    [Export]
    public class MessageModel : NotificationObject
    {
        private Visibility m_HighgMessage;
        private IInformationContent m_SelectItem;
        [ImportingConstructor]
        public MessageModel(MeesageControoler controoler)
        {
            controoler.ViewModel = this;
            Controoler = controoler;
        }

        public IInformationContent SelectItem
        {
            get { return m_SelectItem; }
            set
            {
                if (Equals(value, m_SelectItem))
                    return;
                m_SelectItem = value;
                Controoler.DownCommand.RaiseCanExecuteChanged();
                Controoler.UpCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => SelectItem);
            }
        }

        public Visibility HighgMessage
        {
            get { return m_HighgMessage; }
            set
            {
                if (value == m_HighgMessage)
                    return;
                m_HighgMessage = value;
                RaisePropertyChanged(() => HighgMessage);
            }
        }
        public MeesageControoler Controoler { get; private set; }
    }
}
