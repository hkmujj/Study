using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Addin.MMIDataDebugger.Model
{
    public class DataCollection : NotificationObject
    {
        private DataPage m_InData;
        private DataPage m_OutData;

        public DataCollection()
        {
            InData = new DataPage();
            OutData = new DataPage();
        }

        public DataPage InData
        {
            get { return m_InData; }
            private set
            {
                if (Equals(value, m_InData))
                    return;

                m_InData = value;
                RaisePropertyChanged(() => InData);
            }
        }

        public DataPage OutData
        {
            get { return m_OutData; }
            private set
            {
                if (Equals(value, m_OutData))
                    return;

                m_OutData = value;
                RaisePropertyChanged(() => OutData);
            }
        }
    }
}