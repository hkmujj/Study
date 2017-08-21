using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Motor.HMI.CRH380BG.Adapter
{
    public abstract class UpdateDataProviderBase : IUpdateDataProvider
    {
        protected UpdateDataProviderBase(IEventAggregator eventAggregator, CRH380BGViewModel viewModel, ViewModelManager manager)
        {
            EventAggregator = eventAggregator;
            ViewModel = viewModel;
            Manager = manager;
        }


        public IEventAggregator EventAggregator { private set; get; }

        public ICommunicationDataService DataService
        {
            get { return GlobalParam.Instance.InitParam.CommunicationDataService; }
        }

        public CRH380BGViewModel ViewModel { private set; get; }

        public ViewModelManager Manager { private set; get; }

        public virtual void Initalize(bool isDebugModel)
        {
        }

        public virtual void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

        }
    }
}