using System;
using System.Windows.Threading;
using CommonUtil.Util;

namespace Engine.TAX2.SS7C.Model
{
    public class GlobalTimer
    {
        public static readonly GlobalTimer Instance = new GlobalTimer();

        private DispatcherTimer m_DispatcherTimer;

        public event Action Time500MS;
        public event Action Time1S;

        private uint m_Count = 0;

        private GlobalTimer()
        {
            m_DispatcherTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Normal, On500MS,
                Dispatcher.CurrentDispatcher);
            m_DispatcherTimer.Start();
        }

        private void On500MS(object sender, EventArgs eventArgs)
        {
            ++m_Count;
            ActionUtil.OnAction(Time500MS);
            if (m_Count%2==0)
            {
                m_Count = 0;
                ActionUtil.OnAction(Time1S);
            }
        }

    }
}