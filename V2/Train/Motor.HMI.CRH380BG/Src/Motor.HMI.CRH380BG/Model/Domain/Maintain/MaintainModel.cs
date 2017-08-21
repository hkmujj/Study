using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.HMI.CRH380BG.Model.Domain.Maintain
{
    [Export]
    public class MaintainModel:NotificationObject
    {
        [ImportingConstructor]
        public MaintainModel(TractionHandleDetectionModel tractionHandleDetectionModel, RunSpeedSettingDetectionModel runSpeedSettingDetectionModel)
        {
            TractionHandleDetectionModel = tractionHandleDetectionModel;
            RunSpeedSettingDetectionModel = runSpeedSettingDetectionModel;
        }

        public TractionHandleDetectionModel TractionHandleDetectionModel { get; private set; }
        public RunSpeedSettingDetectionModel RunSpeedSettingDetectionModel { get; private set; }

        private ModelState m_MaintainModelState;

        private float m_IntermediateDClinkProgressBar1;
        private float m_IntermediateDClinkProgressBar3;
        private float m_IntermediateDClinkProgressBar6;
        private float m_IntermediateDClinkProgressBar8;
        private double m_UseElectricity1;
        private double m_UseElectricity2;
        private double m_RegenerationElectricity1;
        private double m_RegenerationElectricity2;

        public float IntermediateDClinkProgressBar8
        {
            get { return m_IntermediateDClinkProgressBar8; }
            set
            {
                if (value.Equals(m_IntermediateDClinkProgressBar8))
                    return;
                m_IntermediateDClinkProgressBar8 = value;
                RaisePropertyChanged(() => IntermediateDClinkProgressBar8);
            }
        }

        public float IntermediateDClinkProgressBar6
        {
            get { return m_IntermediateDClinkProgressBar6; }
            set
            {
                if (value.Equals(m_IntermediateDClinkProgressBar6))
                    return;
                m_IntermediateDClinkProgressBar6 = value;
                RaisePropertyChanged(() => IntermediateDClinkProgressBar6);
            }
        }

        public float IntermediateDClinkProgressBar3
        {
            get { return m_IntermediateDClinkProgressBar3; }
            set
            {
                if (value.Equals(m_IntermediateDClinkProgressBar3))
                    return;
                m_IntermediateDClinkProgressBar3 = value;
                RaisePropertyChanged(() => IntermediateDClinkProgressBar3);
            }
        }

        public float IntermediateDClinkProgressBar1
        {
            get { return m_IntermediateDClinkProgressBar1; }
            set
            {
                if (value.Equals(m_IntermediateDClinkProgressBar1))
                    return;
                m_IntermediateDClinkProgressBar1 = value;
                RaisePropertyChanged(() => IntermediateDClinkProgressBar1);
            }
        }

        public double UseElectricity1
        {
            get { return m_UseElectricity1; }
            set
            {
                if (value.Equals(m_UseElectricity1))
                {
                    return;
                }

                m_UseElectricity1 = value;
                RaisePropertyChanged(() => UseElectricity1);
            }
        }

        public double UseElectricity2
        {
            get { return m_UseElectricity2; }
            set
            {
                if (value.Equals(m_UseElectricity2))
                {
                    return;
                }

                m_UseElectricity2 = value;
                RaisePropertyChanged(() => UseElectricity2);
            }
        }

        public double RegenerationElectricity1
        {
            get { return m_RegenerationElectricity1; }
            set
            {
                if (value.Equals(m_RegenerationElectricity1))
                {
                    return;
                }

                m_RegenerationElectricity1 = value;
                RaisePropertyChanged(() => RegenerationElectricity1);
            }
        }

        public double RegenerationElectricity2
        {
            get { return m_RegenerationElectricity2; }
            set
            {
                if (value.Equals(m_RegenerationElectricity2))
                {
                    return;
                }

                m_RegenerationElectricity2 = value;
                RaisePropertyChanged(() => RegenerationElectricity2);
            }
        }

        public ModelState MaintainModelState
        {
            get { return m_MaintainModelState; }
            set
            {
                if (value == m_MaintainModelState)
                    return;
                m_MaintainModelState = value;
                RaisePropertyChanged("MaintainModelState");
            }
        }
    }

}
