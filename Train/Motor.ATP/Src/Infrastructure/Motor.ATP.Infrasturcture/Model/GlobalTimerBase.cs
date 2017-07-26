using System;
using System.Threading;

namespace Motor.ATP.Infrasturcture.Model
{
    public class GlobalTimerBase
    {
        private Timer m_Timer500;

        public event EventHandler Timer500MS;

        public event EventHandler Timer1S;

        private int m_Count = 0;

        protected GlobalTimerBase()
        {
            m_Timer500 = new Timer(state =>
            {
                Interlocked.Increment(ref m_Count);

                OnTimer500Ms();
                if (m_Count % 2 == 0)
                {
                    OnTimer1S();
                }

                if (m_Count > int.MaxValue - 10)
                {
                    Interlocked.Exchange(ref m_Count, 0);
                }
            }, null, 1000, 500);
        }

        private void OnTimer500Ms()
        {
            if (Timer500MS != null)
            {
                Timer500MS(this, EventArgs.Empty);
            }
        }

        private void OnTimer1S()
        {
            if (Timer1S != null)
            {
                Timer1S(this, EventArgs.Empty);
            }
        }
    }
}