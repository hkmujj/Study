using System;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Motor.HMI.CRH380BG.Model.Domain.Maintain
{
    [Export]
    public class TractionHandleDetectionModel:NotificationObject
    {
        private PipeState m_TractionHandleDetectionPipe1State;
        private PipeState m_TractionHandleDetectionPipe2State;
        private float m_TractionHandleDetectionPipe1ProgressBar;

        public float TractionHandleDetectionPipe2ProgressBar
        {
            get { return m_TractionHandleDetectionPipe2ProgressBar; }
            set
            {
                if (value.Equals(m_TractionHandleDetectionPipe2ProgressBar))
                    return;
                m_TractionHandleDetectionPipe2ProgressBar = value;
                RaisePropertyChanged(() => TractionHandleDetectionPipe2ProgressBar);
            }
        }

        public float TractionHandleDetectionPipe1ProgressBar
        {
            get { return m_TractionHandleDetectionPipe1ProgressBar; }
            set
            {
                if (value.Equals(m_TractionHandleDetectionPipe1ProgressBar))
                    return;
                m_TractionHandleDetectionPipe1ProgressBar = value;
                RaisePropertyChanged(() => TractionHandleDetectionPipe1ProgressBar);
            }
        }

        private float m_TractionHandleDetectionPipe2ProgressBar;



        public PipeState TractionHandleDetectionPipe2State
        {
            get { return m_TractionHandleDetectionPipe2State; }
            set
            {
                if (value == m_TractionHandleDetectionPipe2State)
                    return;
                m_TractionHandleDetectionPipe2State = value;
                RaisePropertyChanged(() => TractionHandleDetectionPipe2State);
            }
        }

        public PipeState TractionHandleDetectionPipe1State
        {
            get { return m_TractionHandleDetectionPipe1State; }
            set
            {
                if (value == m_TractionHandleDetectionPipe1State)
                    return;
                m_TractionHandleDetectionPipe1State = value;
                RaisePropertyChanged(() => TractionHandleDetectionPipe1State);
            }
        }
    }
}
