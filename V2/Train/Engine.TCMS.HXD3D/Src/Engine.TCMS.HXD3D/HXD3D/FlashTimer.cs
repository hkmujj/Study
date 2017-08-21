namespace Engine.TCMS.HXD3D.HXD3D
{
    public class FlashTimer
    {
        // Fields
        private int m_FlashCount = 0;

        private readonly int m_FlashTime = 0;

        // Methods
        public FlashTimer(int interval)
        {
            m_FlashTime = interval;
        }

        public bool IsNeedFlash()
        {
            if (m_FlashCount < m_FlashTime * 5)
            {
                m_FlashCount++;
                return true;
            }
            if ((m_FlashCount >= m_FlashTime * 5) && (m_FlashCount < m_FlashTime * 10))
            {
                m_FlashCount++;
                return false;
            }
            m_FlashCount = 0;
            return false;
        }
    }
}