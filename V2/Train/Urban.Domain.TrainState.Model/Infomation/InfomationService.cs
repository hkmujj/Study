using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using Urban.Domain.TrainState.Interface.Infomation;

namespace Urban.Domain.TrainState.Model.Infomation
{
    public class InfomationService : IInfomationService
    {

        public event Action<IInfomationItem> InfomationBegin;

        public event Action<IInfomationItem> InfomationEnd;

        public IInformationCreater InformationCreater { get; private set; }

        public InfomationService(TrainBase parent, IInformationCreater informationCreater)
        {
            Contract.Requires(informationCreater != null);
            InformationCreater = informationCreater;
            informationCreater.InfomationCreated += InformationCreaterOnInfomationCreated;
            informationCreater.InfomationDeleted += InformationCreaterOnInfomationDeleted;
        }


        public void Initalize()
        {
            InformationCreater.Initalize();
        }

        private void InformationCreaterOnInfomationDeleted(ReadOnlyCollection<IInfomationItemContent> infomationItems)
        {
            foreach (var item in infomationItems.Reverse())
            {
                var info = new InfomationItem(item) { EndTime = DateTime.Now };
                OnInfomationEnd(info);
            }
        }

        private void InformationCreaterOnInfomationCreated(ReadOnlyCollection<IInfomationItemContent> infomationItems)
        {
            foreach (var item in infomationItems)
            {
                var info = new InfomationItem(item) { StartTime = DateTime.Now };
                OnInfomationBegin(info);
            }
        }

        protected virtual void OnInfomationBegin(IInfomationItem infomation)
        {
            var handler = InfomationBegin;
            if (handler != null)
            {
                handler(infomation);
            }
        }

        protected virtual void OnInfomationEnd(IInfomationItem infomation)
        {
            var handler = InfomationEnd;
            if (handler != null)
            {
                handler(infomation);
            }
        }
    }
}