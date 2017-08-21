using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Msg.Details;

namespace Tram.CBTC.Infrasturcture.Model.Msg
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
            InformationItems.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(() => ShowingItem);
                RaisePropertyChanged(() => FaultShowingItem);
                RaisePropertyChanged(() => InfosCollection);
                RaisePropertyChanged(() => FaultCollction);
            };
        }

        public List<IInformationContent> InfosCollection
        {
            get
            {
                return InformationItems.Where(w => w.InformationContent.MessageType == MessageType.Info).OrderBy(o => o.InformationContent.HappenDate).Select(s => s.InformationContent).ToList();
            }
        }
        public List<IInformationContent> FaultCollction
        {
            get
            {
                return InformationItems.Where(w => w.InformationContent.MessageType == MessageType.Fault).OrderBy(o => o.InformationContent.HappenDate).Select(s => s.InformationContent).ToList();
            }
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