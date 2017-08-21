using CommonUtil.Util;

namespace CRH2MMI.DriveState
{
    class RemovalStateQueue
    {
        private readonly string[] m_RemovalStates;

        private int m_CurentZeroIndex;

        public uint Count { private set; get; }

        public string this[int idx]
        {
            get { return m_RemovalStates[(idx + m_CurentZeroIndex) % Count]; }
            private set { m_RemovalStates[(idx + m_CurentZeroIndex) % Count] = value; }
        }

        public RemovalStateQueue(uint cacheSize)
        {
            Count = cacheSize;
            m_RemovalStates = new string[cacheSize];
            m_CurentZeroIndex = 0;
        }

        public void Add(string state)
        {
            for (var i = 0; i < Count; i++)
            {
                if (this[i].IsNullOrWhiteSpace())
                {
                    this[i] = state;
                    return;
                }
            }

            // 去掉老的
            this[0] = state;
            ++m_CurentZeroIndex;
        }

        public void Delete(string state)
        {
            for (var i = 0; i < Count; i++)
            {
                if (this[i] == state)
                {
                    this[i] = null;
                    for (int j = i; j < Count-1; j++)
                    {
                        this[j] = this[j + 1];
                        this[j + 1] = null;
                    }
                    return;
                }
            }
        }
    }
}
