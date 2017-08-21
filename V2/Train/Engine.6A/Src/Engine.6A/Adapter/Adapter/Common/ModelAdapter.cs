using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using System.Windows;
using Engine._6A.Adapter.Adapter.Common.DataMonitor;
using Engine._6A.Adapter.Adapter.Common.SystemSetting;
using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Adapter.Resouce;
using Engine._6A.Config;
using Engine._6A.Constance;
using Engine._6A.Enums;
using Engine._6A.Event;
using Engine._6A.EventArgs;
using Engine._6A.Interface;
using Engine._6A.Interface.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Engine._6A.Adapter.Adapter.Common
{
    [Export(typeof(IEngineAdapter))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ModelAdapter : IEngineAdapter, IHeartTime, IDisposable
    {

        private Timer m_Timer;
        public ModelAdapter()
        {
            switch (GlobalParam.Engine6AConfig.Type)
            {
                case Engine6AType.Alex6:
                    Model = ServiceLocator.Current.GetInstance<IEngine6AViewModel>(ContractName.Axle6);
                    Model.Title.Category = "六轴机车";
                    break;
                case Engine6AType.Alex8:
                    Model = ServiceLocator.Current.GetInstance<IEngine6AViewModel>(ContractName.Axle8);
                    Model.Title.Category = "A节数据";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            InitAdapter();
            m_Timer = new Timer(state =>
            {
                Heart();

            }, null, 1000, 1000);
            GlobalParam.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += ModelAdapter_CourseStateChanged;
        }

        private ButtonEvent ButtonEvent;
        private DialAdapter m_DialAdapter;
        private TitleAdapter m_TitleAdapter;
        private FaultAdapter m_FaultAdapter;
        private PlateformInfoAdapter m_PlateFormInfo;
        private BrakeDataAdapter m_BrakeDataAdapter;
        private InsulationAdapter m_InsulationAdapter;
        private RunLineOneAdapter m_RunLineOneAdapter;
        private MonitorAdapter m_MonitorAdapter;
        private MicrocomputerInfoAdapter m_MicrocomputerInfoAdapter;
        private ElectropsychometerTestAdapter m_Electropsychometer;
        private OlLevelMeterAdapter m_OlLevelMeterAdapter;
        private ForTheColumnPageOneAdapter m_ColumnPageOneAdapter;
        public void Heart()
        {
            m_DialAdapter.Heart();
            m_TitleAdapter.Heart();
        }
        private void InitAdapter()
        {
            FaultManage = new FaultManage(12);
            FaultManage.LoadFile(GlobalParam.InitParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory);
            m_PlateFormInfo = new PlateformInfoAdapter(this);
            m_DialAdapter = new DialAdapter(this);
            m_TitleAdapter = new TitleAdapter(this);
            m_FaultAdapter = new FaultAdapter(this);
            m_BrakeDataAdapter = new BrakeDataAdapter(this);
            m_InsulationAdapter = new InsulationAdapter(this);
            m_RunLineOneAdapter = new RunLineOneAdapter(this);
            m_MonitorAdapter = new MonitorAdapter(this);
            m_MicrocomputerInfoAdapter = new MicrocomputerInfoAdapter(this);
            m_Electropsychometer = new ElectropsychometerTestAdapter(this);
            m_OlLevelMeterAdapter = new OlLevelMeterAdapter(this);
            m_ColumnPageOneAdapter = new ForTheColumnPageOneAdapter(this);
            ButtonEvent = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonEvent>();
        }
        private void DataChanged(object sender, CommunicationDataChangedArgs args)
        {
            m_DialAdapter.DataChnged(sender, args);
            m_TitleAdapter.DataChnged(sender, args);
            m_FaultAdapter.DataChnged(sender, args);
            m_BrakeDataAdapter.DataChnged(sender, args);
            m_InsulationAdapter.DataChnged(sender, args);
            m_RunLineOneAdapter.DataChnged(sender, args);
            m_MonitorAdapter.DataChnged(sender, args);
            m_MicrocomputerInfoAdapter.DataChnged(sender, args);
            m_Electropsychometer.DataChnged(sender, args);
            m_OlLevelMeterAdapter.DataChnged(sender, args);
            m_ColumnPageOneAdapter.DataChnged(sender, args);
            ResponseButton(args.ChangedBools);
        }

        void ResponseButton(CommunicationDataChangedArgs<bool> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.F1];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.F1);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.F2];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.F2);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.F3];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.F3);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.F4];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.F4);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.F5];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.F5);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.上];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.Up);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.下];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.Down);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.左];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.Left);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.右];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.Right);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.确定2];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.OneConfirm);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.上翻页];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.Last);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.下翻页];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.Next);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.测试];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.Test);
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.确认1];
            if (args.NewValue.ContainsKey(index))
            {
                if (args.NewValue[index])
                {
                    ButtonEvent.Publish(ButttonType.TwoConfirm);
                }
            }

        }
        void ModelAdapter_CourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            var events = Model.EventAggregator.GetEvent<NavigateEvent>();
            switch (e.CourseService.CurrentCourseState)
            {
                case CourseState.Unknown:

                    break;
                case CourseState.Started:

                    break;
                case CourseState.Stoped:

                    Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void Changed(CommunicationDataChangedArgs<float> args)
        {

        }

        public IFaultManage FaultManage { get; private set; }


        public void Changed(CommunicationDataChangedArgs<bool> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.亮屏];

            if (args.NewValue.ContainsKey(index))
            {
                var events = Model.EventAggregator.GetEvent<NavigateEvent>();
                if (args.NewValue[index])
                {
                    events.Publish(new NavigateEventArgs()
                    {
                        Region = RegionNames.MainRegion,
                        Name = CoontrolNameBase.StartingView,
                    });

                    Clear();

                    Model.MMIBlack = Visibility.Visible;

                }
                else
                {
                    events.Publish(new NavigateEventArgs()
                    {
                        Region = RegionNames.MainRegion,
                        Name = CoontrolNameBase.BlackScreenView,
                    });


                }
            }
        }

        public void DataChnged(object sender, CommunicationDataChangedArgs args)
        {
            Changed(args.ChangedBools);
            Changed(args.ChangedFloats);
            DataChanged(sender, args);
        }



        public void Clear()
        {
            Model.Clear();
        }

        public IEngine6AViewModel Model { get; private set; }

        public void Dispose()
        {
            m_Timer.Dispose();
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            Changed(dataChangedArgs);
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            Changed(dataChangedArgs);
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            DataChanged(sender, dataChangedArgs);
        }
    }
}