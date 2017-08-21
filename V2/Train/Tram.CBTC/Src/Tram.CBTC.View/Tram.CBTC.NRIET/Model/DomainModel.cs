using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.NRIET.Model.BtnStragy;

namespace Tram.CBTC.NRIET.Model
{
    [Export]
    public class DomainModel : NotificationObject
    {
        private IStateInterface m_CurrentStateInterface;

        private DateTime m_CurrentTime;
 

        [DebuggerStepThrough]
        [ImportingConstructor]
        public DomainModel()
        {
           
        }
      

        public IStateInterface CurrentStateInterface
        {
            private set
            {
                if (Equals(value, m_CurrentStateInterface))
                {
                    return;
                }

                m_CurrentStateInterface = value;
                m_CurrentStateInterface.UpdateState();
                RaisePropertyChanged(() => CurrentStateInterface);
            }
            get { return m_CurrentStateInterface; }
        }

        public void UpdateCurrentState(IStateInterface current)
        {
            CurrentStateInterface = current;
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentTime
        {
            get { return m_CurrentTime; }
            set
            {
                if (value.Equals(m_CurrentTime))
                {
                    return;
                }

                m_CurrentTime = value;
                RaisePropertyChanged(() => CurrentTime);
            }
        }
    }
}