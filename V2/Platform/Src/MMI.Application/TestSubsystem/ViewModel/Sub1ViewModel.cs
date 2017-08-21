using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace TestSubsystem.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class Sub1ViewModel : NotificationObject
    {
        private string m_Txt;

        public string Txt
        {
            set
            {
                if (value == m_Txt)
                {
                    return;
                }
                m_Txt = value;
                RaisePropertyChanged(() => Txt);
            }
            get { return m_Txt; }
        }

        public Sub1ViewModel()
        {
            Txt = "Sub1ViewModel";
        }
    }
}