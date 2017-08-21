using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.BrakeSys.Events
{
    [Export]
    public class BrakeSysEventModel : NotificationObject
    {
        private volatile bool m_EmergerBrakeFlag;
        private volatile int m_CurrentEmergerBrakeCount;
        private bool m_EmergerBrakeUnlockFlag;

        public int CurrentEmergerBrakeCount
        {
            get { return m_CurrentEmergerBrakeCount; }
            set
            {
                if (value == m_CurrentEmergerBrakeCount)
                {
                    return;
                }

                m_CurrentEmergerBrakeCount = value;
                RaisePropertyChanged(() => CurrentEmergerBrakeCount);
            }
        }

        public bool EmergerBrakeFlag
        {
            get { return m_EmergerBrakeFlag; }
            set
            {
                if (value == m_EmergerBrakeFlag)
                {
                    return;
                }

                m_EmergerBrakeFlag = value;
                RaisePropertyChanged(() => EmergerBrakeFlag);
            }
        }

        public bool EmergerBrakeUnlockFlag
        {
            get { return m_EmergerBrakeUnlockFlag; }
            set
            {
                if (value == m_EmergerBrakeUnlockFlag)
                {
                    return;
                }

                m_EmergerBrakeUnlockFlag = value;
                RaisePropertyChanged(() => EmergerBrakeUnlockFlag);
            }
        }
    }
}