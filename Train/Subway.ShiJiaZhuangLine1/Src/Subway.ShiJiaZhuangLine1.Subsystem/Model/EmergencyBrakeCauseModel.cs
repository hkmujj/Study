using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    [Export]
    public class EmergencyBrakeCauseModel : NotificationObject
    {
        private ObservableCollection<EmergencyBrakeCauseUnit> m_EmergencyBrakeCauseUnits;

        public ObservableCollection<EmergencyBrakeCauseUnit> EmergencyBrakeCauseUnits
        {
            get { return m_EmergencyBrakeCauseUnits; }
            set
            {
                if (Equals(value, m_EmergencyBrakeCauseUnits))
                    return;
                m_EmergencyBrakeCauseUnits = value;
                RaisePropertyChanged(() => EmergencyBrakeCauseUnits);
            }
        }
    }
}
