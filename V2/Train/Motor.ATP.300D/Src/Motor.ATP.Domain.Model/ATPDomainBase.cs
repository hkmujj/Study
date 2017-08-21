using System;
using System.Collections.Generic;
using MMI.Facility.Interface;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Fault;
using Motor.ATP.Domain.Model.ControlModel;
using Motor.ATP.Domain.Model.CTCS;
using Motor.ATP.Domain.Model.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model
{
    public abstract class ATPDomainBase : baseClass, IATP
    {
        protected TrainInfo m_TrainInfo;
        protected Speed m_Speed;
        protected Driver m_Driver;
        protected ControlModelBase m_ControlModel;
        protected ICTCS m_CTCS;
        protected SpeedMonitoringSectionBase m_SpeedMonitoringSection;
        protected Message.Message m_Message;
        protected WarningIntervention m_WarningIntervention;
        protected TargetSpeedMonitoringSection m_TargetSpeedMonitoringSection;
        protected CeilingSpeedMonitoringSection m_CeilingSpeedMonitoringSection;
        protected SpeedProfile m_SpeedProfile;
        protected CabSignal m_CabSignal;
        //private IRegionManager m_RegionManager;
        //private IATPState m_ATPState;
        //private IATPStateControlPanel m_StateControlPanel;
        //private IApplicationService m_ApplicationService;
        //private ICommunicationDataFacade m_CommunicationDataFacade;

        /// <summary>
        /// root config  只需要一个
        /// </summary>
        //private static ATPRootConfig m_ATPRootConfig;

        protected ATPDomainBase()
        {
            m_ControlModel = new UnkownControlModel(this);
        }

        object IIdentityProvide.Identity
        {
            get { return Identity; }
        }

        public ScreenIdentity Identity { get; set; }

        public Visibility Visibility { get; set; }

        public ATPType ATPType { get; protected set; }


        public IMessage Message
        {
            get { return m_Message; }
        }

        public ITrainInfo TrainInfo
        {
            get { return m_TrainInfo; }
        }

        public ICTCS CTCS
        {
            get { return m_CTCS; }
            set
            {
                m_CTCS = value;
                if (value == null)
                {
                    m_CTCS = new UnkownCTCS(this);
                }
                
            }
        }

        public IControlModel ControlModel
        {
            get { return m_ControlModel; }
            set
            {
                m_ControlModel = (ControlModelBase)value;
                if (value == null)
                {
                    m_ControlModel = new UnkownControlModel(this);
                }
                
            }
        }

        public ICabSignal CabSignal { get { return m_CabSignal; } }

        public IWarningIntervention WarningIntervention
        {
            get { return m_WarningIntervention; }
        }

        public ISpeedMonitoringSection SpeedMonitoringSection
        {
            get { return m_SpeedMonitoringSection; }
            set { m_SpeedMonitoringSection = value as SpeedMonitoringSectionBase;  }
        }

        public virtual void Initalize(ScreenIdentity identity)
        {
            this.Identity = identity;
            ATPRepository.Instance.Regist(this);
        }


        public virtual void OnFaultOccused(IFaultItem faultItem)
        {
            throw new NotImplementedException();
        }

        public virtual void OnFaultFixed(IFaultItem faultItem)
        {
            throw new NotImplementedException();
        }

        public sealed override bool init(ref int nErrorObjectIndex)
        {
            Initalize(ScreenIdentityHelper.CreateIdentity(this));

            return true;
        }

    }
}