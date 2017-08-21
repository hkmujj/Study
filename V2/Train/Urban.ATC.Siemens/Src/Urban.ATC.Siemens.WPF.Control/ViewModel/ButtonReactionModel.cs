using Microsoft.Practices.Prism.Commands;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class ButtonReactionModel : ViewModelBase
    {
        private Visibility m_ButtonVisibility;
        private Timer m_Timer;

        public ButtonReactionModel(SiemensData data)
        {
            Parent = data;
            SenData = new DelegateCommand<string>(SenDataFun);
            ClearData = new DelegateCommand<string>(ClearDataFun);
            m_Timer = new Timer(state =>
            {
                ButtonVisibility = (ButtonReactivation && DateTime.Now.Second % 2 == 0)
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }, null, 1000, 1000);
        }

        public bool ButtonReactivation { get; set; }

        public Visibility ButtonVisibility
        {
            get { return m_ButtonVisibility; }
            set
            {
                if (value == m_ButtonVisibility)
                {
                    return;
                }
                m_ButtonVisibility = value;
                RaisePropertyChanged(() => ButtonVisibility);
            }
        }

        private void ClearDataFun(string obj)
        {
            Parent.DataService.WriteService.ChangeBool(int.Parse(obj), false);
        }

        private void SenDataFun(string obj)
        {
            Parent.DataService.WriteService.ChangeBool(int.Parse(obj), true);
        }

        public ICommand ClearData { get; private set; }
        public ICommand SenData { get; private set; }
    }
}