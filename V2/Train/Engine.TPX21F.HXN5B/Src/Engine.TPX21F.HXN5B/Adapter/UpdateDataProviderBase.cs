using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Engine.TPX21F.HXN5B.Adapter
{
    public abstract class UpdateDataProviderBase : IUpdateDataProvider
    {
        protected UpdateDataProviderBase(IEventAggregator eventAggregator, HXN5BViewModel viewModel)
        {
            EventAggregator = eventAggregator;
            ViewModel = viewModel;
        }


        public IEventAggregator EventAggregator { private set; get; }

        public ICommunicationDataService DataService
        {
            get { return GlobalParam.Instance.InitParam.CommunicationDataService; }
        }

        public HXN5BViewModel ViewModel { private set; get; }

        public virtual void Initalize(bool isDebugModel)
        {
        }

        public virtual void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

        }
    }
}