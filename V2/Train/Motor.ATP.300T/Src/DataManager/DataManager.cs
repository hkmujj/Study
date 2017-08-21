using System.Collections.Generic;

namespace Motor.ATP._300T.DataManager
{
    public class DataManager : IDataClearable
    {
        public static readonly DataManager Instance = new DataManager();

        private readonly List<IDataClearable> m_DataNeedClearCollection;

        private DataManager()
        {
            m_DataNeedClearCollection = new List<IDataClearable>();
        }

        public void AddRequest(IDataClearable dataClearable)
        {
            if (!m_DataNeedClearCollection.Contains(dataClearable))
            {
                m_DataNeedClearCollection.Add(dataClearable);
            }
        }

        public void ClearData(DataClearFlag clearFlag = DataClearFlag.All)
        {
            m_DataNeedClearCollection.ForEach(e => e.ClearData(clearFlag));
        }
    }
}