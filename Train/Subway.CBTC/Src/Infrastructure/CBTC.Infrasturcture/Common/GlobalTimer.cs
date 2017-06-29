using System;
using System.Threading;
using System.Windows.Threading;

namespace CBTC.Infrasturcture.Common
{
    public sealed class GlobalTimer
    {
        public static readonly GlobalTimer Instance = new GlobalTimer();

        private DispatcherTimer m_Timer500;

        public event EventHandler Timer500MS;

        public event EventHandler Timer1S;

        private int m_Count = 0;

        private GlobalTimer()
        {
            m_Timer500 = new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Background,
                (sender, args) =>
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
                }, Dispatcher.CurrentDispatcher);

            m_Timer500.Start();
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