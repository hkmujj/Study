using Microsoft.Practices.Prism.ViewModel;

namespace Motor.LKJ._2000.Entity.Model
{
    public class LKJ2000MainModel : NotificationObject
    {
        private LKJState m_State;

        public LKJState State
        {
            set
            {
                if (value == m_State)
                {
                    return;
                }
                m_State = value;
                RaisePropertyChanged(() => State);
            }
            get { return m_State; }
        }
    }
}