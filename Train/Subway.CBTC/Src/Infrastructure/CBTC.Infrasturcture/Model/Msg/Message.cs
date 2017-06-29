using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Msg.Details;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Msg
{
    /// <summary>
    /// 消息， 包括 故障和通知
    /// </summary>
    public class Message : NotificationObject
    {
        [DebuggerStepThrough]
        public Message()
        {
            InformationItems = new ObservableCollection<InformationItem>();
            InformationItems.CollectionChanged += (sender, args) => RaisePropertyChanged(() => ShowingItem);
            InformationItems.CollectionChanged += (sender, args) => RaisePropertyChanged(() => FaultShowingItem);
        }

        public ObservableCollection<InformationItem> InformationItems { get; protected set; }


        public virtual InformationItem FaultShowingItem
        {
            get
            {
                return InformationItems.LastOrDefault(w => w.InformationContent.MessageType == MessageType.Fault);
            }
        }

        public virtual InformationItem ShowingItem
        {
            get
            {
                return InformationItems.LastOrDefault(w => w.InformationContent.MessageType == MessageType.Info);
            }
        }


        public MessageFactory MessageFactory { get; set; }
    }
}