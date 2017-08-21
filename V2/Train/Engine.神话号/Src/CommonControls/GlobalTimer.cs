using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace CommonControls
{
    public class GlobalTimer
    {
        public static readonly GlobalTimer Instance = new GlobalTimer();

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly System.Timers.Timer m_Timer500;

        private int m_TimerCount = 0;

        /// <summary>
        /// 1000ms System.Timers.Timer
        /// </summary>
        public event ElapsedEventHandler TimersElapsed1000MS;

        /// <summary>
        /// 500MS System.Timers.Timer
        /// </summary>
        public event ElapsedEventHandler TimersElapsed500MS;

        private GlobalTimer()
        {
            m_Timer500 = new Timer(500) { };
            m_Timer500.Elapsed += Timer500OnElapsed;
            m_Timer500.Start();
        }

        private void Timer500OnElapsed(object sender, ElapsedEventArgs e)
        {
            OnTimersElapsed500MS(sender, e);
            ++m_TimerCount;
            if (m_TimerCount%2 == 0)
            {
                OnTimersElapsed1000(sender, e);
            }
        }

        protected virtual void OnTimersElapsed1000(object sender, ElapsedEventArgs e)
        {
            if (TimersElapsed1000MS != null)
            {
                try
                {
                    TimersElapsed1000MS(sender, e);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        protected virtual void OnTimersElapsed500MS(object sender, ElapsedEventArgs e)
        {
            if (TimersElapsed500MS != null)
            {
                try
                {
                    TimersElapsed500MS(this, e);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }
    }
}