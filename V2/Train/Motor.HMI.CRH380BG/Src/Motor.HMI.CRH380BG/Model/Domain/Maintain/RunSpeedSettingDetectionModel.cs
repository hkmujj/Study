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
    public class RunSpeedSettingDetectionModel:NotificationObject
    {
        private PipeState m_RunSpeedSettingDetectionPipeOneState;

        private PipeState m_RunSpeedSettingDetectionPipeTwoState;

        private float m_RunSpeedSettingDetectionPipeTwoProgressBar;
        private float m_RunSpeedSettingDetectionPipeOneProgressBar;

        public float RunSpeedSettingDetectionPipeTwoProgressBar
        {
            get { return m_RunSpeedSettingDetectionPipeTwoProgressBar; }
            set
            {
                if (value.Equals(m_RunSpeedSettingDetectionPipeTwoProgressBar))
                    return;
                m_RunSpeedSettingDetectionPipeTwoProgressBar = value;
                RaisePropertyChanged(() => RunSpeedSettingDetectionPipeTwoProgressBar);
            }
        }

        public float RunSpeedSettingDetectionPipeOneProgressBar
        {
            get { return m_RunSpeedSettingDetectionPipeOneProgressBar; }
            set
            {
                if (value.Equals(m_RunSpeedSettingDetectionPipeOneProgressBar))
                    return;
                m_RunSpeedSettingDetectionPipeOneProgressBar = value;
                RaisePropertyChanged(() => RunSpeedSettingDetectionPipeOneProgressBar);
            }
        }


        public PipeState RunSpeedSettingDetectionPipeOneState
        {
            get { return m_RunSpeedSettingDetectionPipeOneState; }
            set
            {
                if (value == m_RunSpeedSettingDetectionPipeOneState)
                    return;
                m_RunSpeedSettingDetectionPipeOneState = value;
                RaisePropertyChanged(() => RunSpeedSettingDetectionPipeOneState);
            }
        }

        public PipeState RunSpeedSettingDetectionPipeTwoState
        {
            get { return m_RunSpeedSettingDetectionPipeTwoState; }
            set
            {
                if (value == m_RunSpeedSettingDetectionPipeTwoState)
                    return;
                m_RunSpeedSettingDetectionPipeTwoState = value;
                RaisePropertyChanged(() => RunSpeedSettingDetectionPipeTwoState);
            }
        }
             
    }
}
