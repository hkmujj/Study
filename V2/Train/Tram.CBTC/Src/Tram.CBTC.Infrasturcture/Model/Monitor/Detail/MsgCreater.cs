using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.Infrasturcture.Model.Monitor.Detail
{
    public class MsgCreater : NotificationObject
    {
        private int m_Id;
        private string m_Content;
        private bool m_Delete;
        private bool m_Creat;

        public bool Creat
        {
            set
            {
                if (value == m_Creat)
                    return;

                m_Creat = value;
                RaisePropertyChanged(() => Creat);
            }
            get { return m_Creat; }
        }

        public bool Delete
        {
            get { return m_Delete; }
            set
            {
                if (value == m_Delete)
                    return;

                m_Delete = value;
                RaisePropertyChanged(() => Delete);
            }
        }

        public int Id
        {
            get { return m_Id; }
            set
            {
                if (value == m_Id)
                    return;

                m_Id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        public string Content
        {
            get { return m_Content; }
            set
            {
                if (value == m_Content)
                    return;

                m_Content = value;
                RaisePropertyChanged(() => Content);
            }
        }
    }
}