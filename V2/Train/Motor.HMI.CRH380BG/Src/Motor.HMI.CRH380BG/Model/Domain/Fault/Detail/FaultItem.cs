using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.Model.ConfigModel;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Model.Domain.Fault.Detail
{
    [DebuggerDisplay("CarNumber={InfoConfig.CarNumber},FaultType={InfoConfig.FaultType},FaultCode={InfoConfig.FaultCode},FaultName={InfoConfig.FaultName}")]
    public class FaultItem : NotificationObject
    {
        private DateTime m_ResetTime;

        [DebuggerStepThrough]
        public FaultItem(NotifyInfoConfig infoConfig, DateTime occuseTime)
        {
            InfoConfig = infoConfig;
            OccurseTime = occuseTime;
            FaultReadState = FaultReadState.NotRead;
        }

        public NotifyInfoConfig InfoConfig { get; private set; }

        public DateTime OccurseTime { private set; get; }

        public DateTime ResetTime
        {
            get { return m_ResetTime; }
            set
            {
                if (value.Equals(m_ResetTime))
                {
                    return;
                }

                m_ResetTime = value;
                RaisePropertyChanged(() => ResetTime);
            }
        }


        public FaultReadState FaultReadState { get; set; }
    }
}
