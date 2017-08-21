using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    [Export]
    public class BypassModel : NotificationObject
    {
        private ObservableCollection<BypassUnit> m_BypassUnitCollecion;

        public ObservableCollection<BypassUnit> BypassUnitCollecion
        {
            set
            {
                if (Equals(value, m_BypassUnitCollecion))
                {
                    return;
                }
                m_BypassUnitCollecion = value;
                RaisePropertyChanged(() => BypassUnitCollecion);
            }
            get { return m_BypassUnitCollecion; }
        }
    }
}