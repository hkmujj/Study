using System;
using System.ComponentModel.Composition;
using Motor.TCMS.CRH400BF.Event;
using Motor.TCMS.CRH400BF.Model;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Event;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using MMI.Facility.WPFInfrastructure.Event;

namespace Motor.TCMS.CRH400BF.Adapter
{
    [Export]
    public class DomainAdapter : NotificationObject, IDataListener
    {
        public IEventAggregator EventAggregator { private set; get; }

        public ICommunicationDataService DataService { private set; get; }

        [ImportMany]
#pragma warning disable 649
        private IResetSupport[] m_Reseters;
#pragma warning restore 649

        [ImportMany]
#pragma warning disable 649
        private IUpdateDataProvider[] m_UpdateDataProviders;
#pragma warning restore 649

        [ImportingConstructor]
        public DomainAdapter(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
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
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            ReadServiceOnDataChangedAsync(sender, dataChangedArgs);
        }

        private void ReadServiceOnDataChangedAsync(object sender,
            CommunicationDataChangedArgs communicationDataChangedArgs)
        {
            EventAggregator.GetEvent<DataServiceDataChangedEvent>()
                .Publish(new DataServiceDataChangedEvent.Args(sender, communicationDataChangedArgs));
        }

        private static bool IsInit = false;
        public void Initalize(bool isDebugModel)
        {
            if (IsInit)
            {
                return;
            }

            foreach (var it in m_UpdateDataProviders)
            {
                it.Initalize(isDebugModel);
            }

            DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

            EventAggregator.GetEvent<LazyValueCreatedEvent>().Subscribe(s => DataService.ReadService.RaiseAllDataChanged());

            EventAggregator.GetEvent<SendDataRequestEvent>().Subscribe(SendData);

            EventAggregator.GetEvent<CourseStateChangedEvent>().Subscribe(ResetWhenClassOver, ThreadOption.UIThread);

            GlobalParam.Instance.InitParam.RegistDataListener(this);

            EventAggregator.GetEvent<DataServiceDataChangedEvent>()
                .Subscribe(s => UpdateDatas(s.Sender, s.DataChangedArgs), ThreadOption.UIThread);
            IsInit = true;
        }

        private void ResetWhenClassOver(CourseStateChangedArgs args)
        {
            if (args.CourseService.CurrentCourseState != CourseState.Stoped)
            {
                return;
            }

            foreach (var it in m_Reseters)
            {
                it.Reset();
            }

            DataService.ReadService.RaiseAllDataChanged();
        }

        private void SendData(SendDataRequestEvent.Args args)
        {
            switch (args.Type)
            {
                case SendDataRequestEvent.DataType.InBool:
                    break;
                case SendDataRequestEvent.DataType.OutBool:
                    if (args.Index != -1)
                    {
                        DataService.WriteService.ChangeBool(args.Index, args.ValueB);
                    }
                    else if (!string.IsNullOrWhiteSpace(args.IndexString))
                    {
                        DataService.WriteService.ChangeBool(
                            GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[args.IndexString],
                            args.ValueB);
                    }
                    break;
                case SendDataRequestEvent.DataType.InFloat:
                    break;
                case SendDataRequestEvent.DataType.OutFloat:
                    if (args.Index != -1)
                    {
                        DataService.WriteService.ChangeFloat(args.Index, args.ValueF);
                    }
                    else if (!string.IsNullOrWhiteSpace(args.IndexString))
                    {
                        DataService.WriteService.ChangeFloat(
                            GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[args.IndexString],
                            args.ValueF);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            foreach (var it in m_UpdateDataProviders)
            {
                it.UpdateDatas(sender, args);
            }

        }

    }
}