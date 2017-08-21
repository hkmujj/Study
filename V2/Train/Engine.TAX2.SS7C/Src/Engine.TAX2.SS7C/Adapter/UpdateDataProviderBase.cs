using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Engine.TAX2.SS7C.Adapter
{
    public abstract class UpdateDataProviderBase : IUpdateDataProvider
    {
        protected UpdateDataProviderBase(IEventAggregator eventAggregator, SS7CViewModel viewModel)
        {
            EventAggregator = eventAggregator;
            ViewModel = viewModel;
        }


        public IEventAggregator EventAggregator { private set; get; }

        public ICommunicationDataService DataService
        {
            get { return GlobalParam.Instance.InitParam.CommunicationDataService; }
        }

        public SS7CViewModel ViewModel { private set; get; }

        public virtual void Initalize(bool isDebugModel)
        {
        }

        public virtual void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

        }
    }
}