using Subway.CBTC.QuanLuTong.Model;
using Subway.CBTC.QuanLuTong.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Subway.CBTC.QuanLuTong.Adapter
{
    public abstract class UpdateDataProviderBase : IUpdateDataProvider
    {
        protected UpdateDataProviderBase(IEventAggregator eventAggregator, DomainViewModel viewModel)
        {
            EventAggregator = eventAggregator;
            ViewModel = viewModel;
        }


        public IEventAggregator EventAggregator { private set; get; }

        public ICommunicationDataService DataService
        {
            get { return GlobalParam.Instance.InitParam.CommunicationDataService; }
        }

        public DomainViewModel ViewModel { get; private set; }

        public virtual void Initalize(bool isDebugModel)
        {
        }

        public virtual void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

        }
    }
}