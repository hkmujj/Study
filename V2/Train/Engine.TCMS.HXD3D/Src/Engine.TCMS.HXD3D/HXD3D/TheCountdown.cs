namespace Engine.TCMS.HXD3D.HXD3D
{
    public class TheCountdown
    {
        // Fields
        private int m_MaxTime;

        private int m_Count;

        // Methods
        public TheCountdown(int maxTime)
        {
            m_MaxTime = maxTime;
        }

        public int Counter()
        {
            if (m_MaxTime > 0)
            {
                m_Count++;
                if (m_Count >= 5)
                {
                    m_MaxTime--;
                    m_Count = 0;
                }
            }
            return m_MaxTime;
        }

        public void CounterOver()
        {
            m_MaxTime = 0;
            m_Count = 0;
        }

        public void CounterStart()
        {
            m_MaxTime = 60;
            m_Count = 0;
        }
    }
}