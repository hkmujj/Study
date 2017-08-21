using System;
using System.ComponentModel.Composition;
using System.Timers;
using CommonUtil.Controls;
using CommonUtil.Util;
using Engine.LCDM.HDX2.DataAdapter.Resource;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using Engine.LCDM.HDX2.DataAdapter.Extension;
using Engine.LCDM.HDX2.DataAdapter.Message;
using Engine.LCDM.HDX2.Entity.Events;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;

namespace Engine.LCDM.HDX2.DataAdapter
{
    [Export]
    public class HXD2DataAdapter : IDisposable, IResetable, IDataListener
    {
        public DateTime Now
        {
            private set
            {
                m_Now = value;
                DomainModel.Other.CurrentDateTime = Now;
            }
            get { return m_Now; }
        }

        private readonly Timer m_UpdateNowTimer;

        private readonly MessageUpdater m_MessageUpdater;

        public SubsystemInitParam InitParam { private set; get; }

        public LCDMModel DomainModel { private set; get; }

        private readonly IEventAggregator m_EventAggregator;

        private readonly MessageManager m_MessageManager;

        private DateTime m_Now;

        private BtnEvent BtnEvent
        {
            get { return m_EventAggregator.GetEvent<BtnEvent>(); }
        }

        [ImportingConstructor]
        public HXD2DataAdapter(LCDMModel domainModel, IEventAggregator eventAggregator)
        {
            DomainModel = domainModel;
            m_MessageManager = new MessageManager();
            m_MessageUpdater = new MessageUpdater(m_MessageManager, domainModel);
            m_EventAggregator = eventAggregator;
            m_UpdateNowTimer = new Timer(500) {AutoReset = true};
            m_UpdateNowTimer.Elapsed += (sender, args) => Now = DateTime.Now;
            m_UpdateNowTimer.Start();

        }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;

            m_MessageManager.Initalize(initParam);
            m_MessageUpdater.Run();

            initParam.RegistDataListener(this);

            initParam.DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += OnCourseStateChanged;

            if (initParam.DataPackage.Config.SystemConfig.StartModel == StartModel.Edit)
            {
                initParam.CommunicationDataService.WritableReadService.ChangeBool(
                    HXD2Param.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.LCDM显示器电源], true,
                    true);
            }
        }

        public void ResetDatas()
        {
            DomainModel.ResetDatas();
            DomainModel.SendInterface.Reset();
        }

        private void OnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (courseStateChangedArgs.CourseService.CurrentCourseState == CourseState.Stoped)
            {
                AppLog.Debug("clear datas by course stopped.");
                ResetDatas();
            }
            else if (courseStateChangedArgs.CourseService.CurrentCourseState == CourseState.Started)
            {
                AppLog.Debug("send neccess datas by course started.");
                UpdateOutDataWhenCourseStarted();
            }
        }

        private void UpdateOutDataWhenCourseStarted()
        {
            DomainModel.SendInterface.SendTrainId(DomainModel.TrainId);
        }

        private void ReadServiceOnDataChanged(object sender, CommunicationDataChangedArgs args)
        {
            var cb = args.ChangedBools;
            var cf = args.ChangedFloats;

            AdapterAirBrakePressureInitalizeValue(args);

            cf.UpdateIfContains(InFloatKeys.流量, f => DomainModel.FlowRate = f);
            cf.UpdateIfContains(InFloatKeys.均衡缸压力, f => DomainModel.BalancePressure = f);
            cf.UpdateIfContains(InFloatKeys.列车管压力, f => DomainModel.TrainPipePressure = f);
            cf.UpdateIfContains(InFloatKeys.总风缸压力, f => DomainModel.TotalCylinderPressure = f);
            cf.UpdateIfContains(InFloatKeys.制动缸压力, f => DomainModel.BrakeCylinderPressure = f);
            cf.UpdateIfContains(InFloatKeys.操纵节闸缸1压力, f => DomainModel.OperBrakeCylinder.BrakeCylinder1Pressure = f);
            cf.UpdateIfContains(InFloatKeys.操纵节闸缸2压力, f => DomainModel.OperBrakeCylinder.BrakeCylinder2Pressure = f);
            cf.UpdateIfContains(InFloatKeys.非操纵节闸缸1压力, f => DomainModel.NoneOperBrakeCylinder.BrakeCylinder1Pressure = f);
            cf.UpdateIfContains(InFloatKeys.非操纵节闸缸2压力, f => DomainModel.NoneOperBrakeCylinder.BrakeCylinder2Pressure = f);

            AdapterBtnState(cb);

            cb.UpdateIfContains(InBoolKeys.总风压力低显示标志, b => DomainModel.WorkFlags.LowTotalAir = b);
            cb.UpdateIfContains(InBoolKeys.紧急制动伴随60S倒计时, b => DomainModel.WorkFlags.EmergenceBrake = b);
            cb.UpdateIfContains(InBoolKeys.空电联锁阀得电显示标志, b => DomainModel.WorkFlags.AirAndEleInterlock = b);
            cb.UpdateIfContains(InBoolKeys.LCDM显示器电源,
                b => DomainModel.PowerState = b ? PowerState.Started : PowerState.Shutdown);

            var machine = DomainModel.MachineType;
            cb.UpdateIfContains(InBoolKeys.本机, b =>
            {
                if (b)
                {
                    machine = MachineType.Local;
                }
            });
            cb.UpdateIfContains(InBoolKeys.补机, b =>
            {
                if (b)
                {
                    machine = MachineType.Reser;
                }
            });
            cb.UpdateIfContains(InBoolKeys.单机, b =>
            {
                if (b)
                {
                    machine = MachineType.Sigle;
                }
            });

            DomainModel.MachineType = machine;

            var controlModel = EngineControlModel.Main;
            cb.UpdateIfContains(InBoolKeys.主控, b =>
            {
                if (b)
                {
                    controlModel = EngineControlModel.Main;
                }
            });
            cb.UpdateIfContains(InBoolKeys.从控, b =>
            {
                if (b)
                {
                    controlModel = EngineControlModel.Assist;
                }
            });
            DomainModel.EngineControlModel = controlModel;

            cb.UpdateIfContains(InBoolKeys.是否处于开课状态, (s, b) =>
            {
                if (!b)
                {
                    AppLog.Debug("clear datas by {0} = false.", s);
                    ResetDatas();
                }
            });

            m_MessageManager.Update(args);

        }

        private void AdapterAirBrakePressureInitalizeValue(CommunicationDataChangedArgs args)
        {
            args.ChangedBools.UpdateIfContains(InBoolKeys.列车管定压设置为500,
                b => DomainModel.AirBrake.Pressure = b ? AirBrakePressure.KP500 : AirBrakePressure.KP600);
        }

        private void AdapterBtnState(CommunicationDataChangedArgs<bool> cb)
        {
            cb.UpdateIfContains(InBoolKeys.亮度Add键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.LightAdd));
            cb.UpdateIfContains(InBoolKeys.亮度Del键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.LightDelete));
            cb.UpdateIfContains(InBoolKeys.亮度自动键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.LightAuto));
            cb.UpdateIfContains(InBoolKeys.F1键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.F1));
            cb.UpdateIfContains(InBoolKeys.F2键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.F2));
            cb.UpdateIfContains(InBoolKeys.F3键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.F3));
            cb.UpdateIfContains(InBoolKeys.F4键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.F4));
            cb.UpdateIfContains(InBoolKeys.F5键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.F5));
            cb.UpdateIfContains(InBoolKeys.F6键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.F6));
            cb.UpdateIfContains(InBoolKeys.F7键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.F7));
            cb.UpdateIfContains(InBoolKeys.F8键按下状态, b => PublishBtnEvent(b, HXD2HardwareBtn.F8));
        }

        private void PublishBtnEvent(bool b, HXD2HardwareBtn btn, MouseState state = MouseState.MouseUp)
        {
            if (!b)
            {
                BtnEvent.Publish(new BtnEventArg(state, btn));
            }
        }

        public void Dispose()
        {
            m_UpdateNowTimer.Stop();
            m_UpdateNowTimer.Close();
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
            ReadServiceOnDataChanged(sender, dataChangedArgs);
        }
    }
}