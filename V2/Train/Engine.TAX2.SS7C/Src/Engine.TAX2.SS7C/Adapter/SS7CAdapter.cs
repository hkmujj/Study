using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Events;
using Engine.TAX2.SS7C.Extension;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.Resources.Keys;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Engine.TAX2.SS7C.Adapter
{
    [Export]
    public class SS7CAdapter :NotificationObject, IDataListener
    {
        [ImportMany]
#pragma warning disable 649
        private IUpdateDataProvider[] m_UpdateDataProviders;
#pragma warning restore 649

        public SS7CViewModel ViewModel { private set; get; }

        private ICommunicationDataService m_DataService;

        public void Initalize(bool isDebugModel)
        {
            ViewModel = ServiceLocator.Current.GetInstance<SS7CViewModel>();
            m_DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<DomainmodelLazyValueCreatedEvent>()
                .Subscribe(
                    s =>
                    {
                        m_DataService.ReadService.RaiseAllDataChanged();
                    }, ThreadOption.BackgroundThread);

            if (isDebugModel)
            {
                SetValueWhenDebug();
            }

            foreach (var provider in m_UpdateDataProviders)
            {
                provider.Initalize(isDebugModel);
            }

        }

        private void SetValueWhenDebug()
        {
            m_DataService.WritableReadService.ChangeBool(InB.黑屏标志, true);
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs args)
        {
            args.ChangedBools.UpdateIfContains(InB.黑屏标志, b => ViewModel.Model.IsVisible = b);

            foreach (var provider in m_UpdateDataProviders)
            {
                provider.UpdateDatas(sender, args);
            }
        }
    }
}