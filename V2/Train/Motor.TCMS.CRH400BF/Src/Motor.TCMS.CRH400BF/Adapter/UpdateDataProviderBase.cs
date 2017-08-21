using Motor.TCMS.CRH400BF.Model;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Motor.TCMS.CRH400BF.Model.Train;

namespace Motor.TCMS.CRH400BF.Adapter
{
    public abstract class UpdateDataProviderBase : IUpdateDataProvider
    {
        //protected UpdateDataProviderBase(IEventAggregator eventAggregator, DomainViewModel viewModel)
        //{
        //    EventAggregator = eventAggregator;
        //    ViewModel = viewModel;
        //}

        protected UpdateDataProviderBase(IEventAggregator eventAggregator, TrainModel trainModel)
        {
            EventAggregator = eventAggregator;
            TrainModel = trainModel;
        }
        /// <summary>
        /// 事件聚合器
        /// </summary>
        protected IEventAggregator EventAggregator { private set; get; }
        /// <summary>
        /// 通信服务
        /// </summary>

        protected ICommunicationDataService DataService
        {
            get { return GlobalParam.Instance.InitParam.CommunicationDataService; }
        }

        // public DomainViewModel ViewModel { get; private set; }

        protected TrainModel TrainModel { get; set; }
        //protected DomainModel DomainModel { get; set; }

        public virtual void Initalize(bool isDebugModel)
        {
        }

        public virtual void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

        }
    }
}