using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.TCMS.CRH400BF.Model
{
    [Export]
    public class DomainModel : NotificationObject
    {
        private bool m_IsVisble;

        public bool IsVisble
        {
            get { return m_IsVisble; }
            set
            {
                if (value == m_IsVisble)
                {
                    return;
                }

                m_IsVisble = value;
                RaisePropertyChanged(() => IsVisble);
            }
        }
    }
}