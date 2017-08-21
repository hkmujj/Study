using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class SoftWareVersionModel : NotificationObject
    {
        private ObservableCollection<SoftWareVersionUnit> m_SoftWareVersionUnits;

        public SoftWareVersionModel()
        {
            SoftWareVersionUnits = new ObservableCollection<SoftWareVersionUnit>(GlobalParam.Instance.SoftWareVersion.OrderBy(o => o.Row));
        }

        public ObservableCollection<SoftWareVersionUnit> SoftWareVersionUnits
        {
            get { return m_SoftWareVersionUnits; }
            set
            {
                if (Equals(value, m_SoftWareVersionUnits))
                {
                    return;
                }
                m_SoftWareVersionUnits = value;
                RaisePropertyChanged(() => SoftWareVersionUnits);
            }
        }
    }
}