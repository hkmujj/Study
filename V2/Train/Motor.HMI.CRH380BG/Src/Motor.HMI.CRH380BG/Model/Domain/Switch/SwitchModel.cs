using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
using Motor.HMI.CRH380BG.Model.Domain.Switch.AirCondition;
using Motor.HMI.CRH380BG.Model.Domain.Switch.Illumination;
using Motor.HMI.CRH380BG.Model.Domain.Switch.Traction;

namespace Motor.HMI.CRH380BG.Model.Domain.Switch
{
    [Export]
    public class SwitchModel : NotificationObject
    {
        private TractionModel m_TractionModel;
        private AirConditionModel m_AirConditionModel;
        private IlluminationModel m_IlluminationModel;
        private FanType m_FanType;
        private NetWorkCurrentLimitType m_NetWorkCurrentLimitType;
        private FrontWindowHeatingType m_FrontWindowHeatingType;
        private bool m_NetWorkCurrentLimitEnable;

        [ImportingConstructor]
        public SwitchModel(TractionModel tractionModel,AirConditionModel airConditionModel,IlluminationModel illuminationModel)
        {
            TractionModel = tractionModel;
            AirConditionModel = airConditionModel;
            IlluminationModel = illuminationModel;
        }


        public TractionModel TractionModel
        {
            private set
            {
                if (Equals(value,m_TractionModel))
                {
                    return;
                }

                m_TractionModel = value;
                RaisePropertyChanged(() => TractionModel);
            }
            get { return m_TractionModel; }
        }

        public AirConditionModel AirConditionModel
        {
            private set
            {
                if (Equals(value, m_AirConditionModel))
                {
                    return;
                }

                m_AirConditionModel = value;
                RaisePropertyChanged(() => AirConditionModel);
            }
            get { return m_AirConditionModel; }
        }

        public IlluminationModel IlluminationModel
        {
            private set
            {
                if (Equals(value, m_IlluminationModel))
                {
                    return;
                }

                m_IlluminationModel = value;
                RaisePropertyChanged(() => IlluminationModel);
            }
            get { return m_IlluminationModel; }
        }

        public FanType FanType
        {
            set
            {
                if (value == m_FanType)
                {
                    return;
                }

                m_FanType = value;
                RaisePropertyChanged(() => FanType);
            }
            get { return m_FanType; }
        }

        public NetWorkCurrentLimitType NetWorkCurrentLimitType
        {
            set
            {
                if (value == m_NetWorkCurrentLimitType)
                {
                    return;
                }

                m_NetWorkCurrentLimitType = value;
                RaisePropertyChanged(() => NetWorkCurrentLimitType);
            }
            get { return m_NetWorkCurrentLimitType; }
        }

        public FrontWindowHeatingType FrontWindowHeatingType
        {
            set
            {
                if (value == m_FrontWindowHeatingType)
                {
                    return;
                }

                m_FrontWindowHeatingType = value;
                RaisePropertyChanged(() => FrontWindowHeatingType);
            }
            get { return m_FrontWindowHeatingType; }
        }

        public bool NetWorkCurrentLimitEnable
        {
            get { return m_NetWorkCurrentLimitEnable; }
            set
            {
                if (value == m_NetWorkCurrentLimitEnable)
                {
                    return;
                }

                m_NetWorkCurrentLimitEnable = value;
                RaisePropertyChanged(() => NetWorkCurrentLimitEnable);
            }
        }
        
    }
}
