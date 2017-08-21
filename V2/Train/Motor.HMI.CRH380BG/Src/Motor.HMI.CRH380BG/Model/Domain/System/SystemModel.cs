using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.Model.Domain.System.GearboxMotorTemperature;
using Motor.HMI.CRH380BG.Model.Domain.System.WheelTemperature;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.System.GroupHangs;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Model.Domain.System
{
    [Export]
    public class SystemModel : NotificationObject
    {
        private bool m_CompiledVisible1;
        private bool m_CompiledVisible2;
        private bool m_CompiledVisible3;
        private CatenaryType m_CatenaryType;
        private bool m_CatenaryTypeIsSelect;
        private double m_CurrentHighNet;
        private WheelTemperatureModel m_WheelTemperatureModel;
        private GearboxMotorTemperatureModel m_GearboxMotorTemperatureModel;
        private GroupHangsModel m_GroupHangsModel;

        [ImportingConstructor]
        public SystemModel(WheelTemperatureModel wheelTemperatureModel, GearboxMotorTemperatureModel gearboxMotorTemperatureModel,GroupHangsModel groupHangsModel)
        {
            WheelTemperatureModel = wheelTemperatureModel;
            GearboxMotorTemperatureModel = gearboxMotorTemperatureModel;
            GroupHangsModel = groupHangsModel;
        }

        public WheelTemperatureModel WheelTemperatureModel
        {
            private set
            {
                if (Equals(value, m_WheelTemperatureModel))
                {
                    return;
                }

                m_WheelTemperatureModel = value;
                RaisePropertyChanged(() => WheelTemperatureModel);
            }
            get { return m_WheelTemperatureModel; }
        }

        public GearboxMotorTemperatureModel GearboxMotorTemperatureModel
        {
            private set
            {
                if (Equals(value, m_GearboxMotorTemperatureModel))
                {
                    return;
                }

                m_GearboxMotorTemperatureModel = value;
                RaisePropertyChanged(() => GearboxMotorTemperatureModel);
            }
            get { return m_GearboxMotorTemperatureModel; }
        }

        public GroupHangsModel GroupHangsModel
        {
            private set
            {
                if (Equals(value, m_GroupHangsModel))
                {
                    return;
                }

                m_GroupHangsModel = value;
                RaisePropertyChanged(() => GroupHangsModel);
            }
            get { return m_GroupHangsModel; }
        }

        public bool CompiledVisible1
        {
            get { return m_CompiledVisible1; }
            set
            {
                if (value == m_CompiledVisible1)
                {
                    return;
                }

                m_CompiledVisible1 = value;
                RaisePropertyChanged(() => CompiledVisible1);
            }
        }

        public bool CompiledVisible2
        {
            get { return m_CompiledVisible2; }
            set
            {
                if (value == m_CompiledVisible2)
                {
                    return;
                }

                m_CompiledVisible2 = value;
                RaisePropertyChanged(() => CompiledVisible2);
            }
        }

        public bool CompiledVisible3
        {
            get { return m_CompiledVisible3; }
            set
            {
                if (value == m_CompiledVisible3)
                {
                    return;
                }

                m_CompiledVisible3 = value;
                RaisePropertyChanged(() => CompiledVisible3);
            }
        }

        public CatenaryType CatenaryType
        {
            set
            {
                if (value == m_CatenaryType)
                {
                    return;
                }

                m_CatenaryType = value;
                RaisePropertyChanged(() => CatenaryType);
            }
            get { return m_CatenaryType; }
        }

        public bool CatenaryTypeIsSelect
        {
            get { return m_CatenaryTypeIsSelect; }
            set
            {
                if (value == m_CatenaryTypeIsSelect)
                {
                    return;
                }

                m_CatenaryTypeIsSelect = value;
                RaisePropertyChanged(() => CatenaryTypeIsSelect);
            }
        }

        public double CurrentHighNet
        {
            get { return m_CurrentHighNet; }
            set
            {
                if (value.Equals(m_CurrentHighNet))
                {
                    return;
                }

                m_CurrentHighNet = value;
                RaisePropertyChanged(() => CurrentHighNet);
            }
        }
    }
}
