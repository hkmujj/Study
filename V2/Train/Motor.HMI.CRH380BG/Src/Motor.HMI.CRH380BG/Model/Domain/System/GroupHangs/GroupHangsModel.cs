using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Model.Domain.System.GroupHangs
{
    [Export]
    public class GroupHangsModel : NotificationObject
    {
        private GroupHangsType m_EMU1FrontHangType;
        private GroupHangsType m_EMU1AfterHangType;
        private GroupHangsType m_EMU2FrontHangType;
        private GroupHangsType m_EMU2AfterHangType;
        private bool m_EMU2HangVisible;
        private bool m_SpeedVisible;
        private bool m_EMU2CloseCarCoverVisible;
        private bool m_ConfirmInfo1Visible;
        private bool m_ConfirmInfo2Visible;

        public GroupHangsType EMU1FrontHangType
        {
            set
            {
                if (value == m_EMU1FrontHangType)
                {
                    return;
                }

                m_EMU1FrontHangType = value;
                RaisePropertyChanged(() => EMU1FrontHangType);
            }
            get { return m_EMU1FrontHangType; }
        }

        public GroupHangsType EMU1AfterHangType
        {
            set
            {
                if (value == m_EMU1AfterHangType)
                {
                    return;
                }

                m_EMU1AfterHangType = value;
                RaisePropertyChanged(() => EMU1AfterHangType);
            }
            get { return m_EMU1AfterHangType; }
        }

        public GroupHangsType EMU2FrontHangType
        {
            set
            {
                if (value == m_EMU2FrontHangType)
                {
                    return;
                }

                m_EMU2FrontHangType = value;
                RaisePropertyChanged(() => EMU2FrontHangType);
            }
            get { return m_EMU2FrontHangType; }
        }

        public GroupHangsType EMU2AfterHangType
        {
            set
            {
                if (value == m_EMU2AfterHangType)
                {
                    return;
                }

                m_EMU2AfterHangType = value;
                RaisePropertyChanged(() => EMU2AfterHangType);
            }
            get { return m_EMU2AfterHangType; }
        }

        public bool EMU2HangVisible
        {
            get { return m_EMU2HangVisible; }
            set
            {
                if (value == m_EMU2HangVisible)
                {
                    return;
                }

                m_EMU2HangVisible = value;
                RaisePropertyChanged(() => EMU2HangVisible);
            }
        }

        public bool SpeedVisible
        {
            get { return m_SpeedVisible; }
            set
            {
                if (value == m_SpeedVisible)
                {
                    return;
                }

                m_SpeedVisible = value;
                RaisePropertyChanged(() => SpeedVisible);
            }
        }

        public bool EMU2CloseCarCoverVisible
        {
            get { return m_EMU2CloseCarCoverVisible; }
            set
            {
                if (value == m_EMU2CloseCarCoverVisible)
                {
                    return;
                }

                m_EMU2CloseCarCoverVisible = value;
                RaisePropertyChanged(() => EMU2CloseCarCoverVisible);
            }
        }

        public bool ConfirmInfo1Visible
        {
            get { return m_ConfirmInfo1Visible; }
            set
            {
                if (value == m_ConfirmInfo1Visible)
                {
                    return;
                }

                m_ConfirmInfo1Visible = value;
                RaisePropertyChanged(() => ConfirmInfo1Visible);
            }
        }

        public bool ConfirmInfo2Visible
        {
            get { return m_ConfirmInfo2Visible; }
            set
            {
                if (value == m_ConfirmInfo2Visible)
                {
                    return;
                }

                m_ConfirmInfo2Visible = value;
                RaisePropertyChanged(() => ConfirmInfo2Visible);
            }
        }
    }
}
