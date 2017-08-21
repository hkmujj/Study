using System;
using System.Windows;
using System.Windows.Threading;

namespace Urban.GuiYang.DDU.Model
{
    public class GlobalTimer
    {
        public event EventHandler Tick500MS;

        public event EventHandler Tick1S;

        public static readonly GlobalTimer Instance = new GlobalTimer();

        private DispatcherTimer m_Timer500S;

        private int m_Count500MS;

        public GlobalTimer()
        {
            m_Timer500S = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 500), DispatcherPriority.Normal,
                OnTimeTick, Application.Current.Dispatcher);
        }

        private void OnTimeTick(object sender, EventArgs e)
        {
            OnTick500Ms(sender, e);

            if (m_Count500MS % 2 == 0 && m_Count500MS != 0)
            {
                OnTick1S(sender, e);
            }

            ++m_Count500MS;
        }

        protected virtual void OnTick500Ms(object sender, EventArgs e)
        {
            if (Tick500MS != null)
            {
                Tick500MS(sender, e);
            }
        }

        protected virtual void OnTick1S(object sender, EventArgs e)
        {
            if (Tick1S != null)
            {
                Tick1S(sender, e);
            }
        }

    }
}