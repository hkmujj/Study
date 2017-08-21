using System.ComponentModel.Composition;
using CRH2MMI.Config.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace CRH2TrainTypeSelector.Model
{
    [Export]
    public class ShellModel : NotificationObject
    {
        private CRH2Type m_SelectedCRH2Type;

        public CRH2Type SelectedCRH2Type
        {
            set
            {
                if (value == m_SelectedCRH2Type)
                {
                    return;
                }
                m_SelectedCRH2Type = value;
                RaisePropertyChanged(() => SelectedCRH2Type);
            }
            get { return m_SelectedCRH2Type; }
        }
    }
}