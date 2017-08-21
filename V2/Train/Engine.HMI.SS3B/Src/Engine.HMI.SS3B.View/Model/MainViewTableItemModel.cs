using Microsoft.Practices.Prism.ViewModel;

namespace Engine.HMI.SS3B.View.Model
{
    public class MainViewTableItemModel  : NotificationObject
    {
        private string m_Name;
        private string m_Info1;
        private string m_MineInfo;
        private string m_OtherInfo;

        public string Name
        {
            set
            {
                if (value == m_Name)
                {
                    return;
                }
                m_Name = value;
                RaisePropertyChanged(() => Name);
            }
            get { return m_Name; }
        }

        public string Info1
        {
            set
            {
                if (value == m_Info1)
                {
                    return;
                }
                m_Info1 = value;
                RaisePropertyChanged(() => Info1);
            }
            get { return m_Info1; }
        }

        public string MineInfo
        {
            set
            {
                if (value == m_MineInfo)
                {
                    return;
                }
                m_MineInfo = value;
                RaisePropertyChanged(() => MineInfo);
            }
            get { return m_MineInfo; }
        }

        public string OtherInfo
        {
            set
            {
                if (value == m_OtherInfo)
                {
                    return;
                }
                m_OtherInfo = value;
                RaisePropertyChanged(() => OtherInfo);
            }
            get { return m_OtherInfo; }
        }
    }
}