using System.ComponentModel.Composition;
using Subway.XiaMenLine1.Subsystem.Controller;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    [Export]
    public class MainInstanceViewModel : ViewModelBase
    {
        private string m_Password;

        [ImportingConstructor]
        public MainInstanceViewModel(MainInstanceVuewController controller)
        {
            Controller = controller;
        }

        public string Password
        {
            get { return m_Password; }
            set
            {
                if (value == m_Password)
                {
                    return;
                }
                m_Password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public MainInstanceVuewController Controller { get; private set; }
    }
}