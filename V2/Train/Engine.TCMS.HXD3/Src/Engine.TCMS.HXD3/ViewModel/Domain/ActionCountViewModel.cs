using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class ActionCountViewModel : NotificationObject
    {
        private ObservableCollection<ActionNumUnit> m_NumUnits;

        public ActionCountViewModel()
        {
            NumUnits = new ObservableCollection<ActionNumUnit>(GlobalParam.Instance.ActionNum.OrderBy(o => o.Index));
        }

        public ObservableCollection<ActionNumUnit> NumUnits
        {
            get { return m_NumUnits; }
            set
            {
                if (Equals(value, m_NumUnits))
                {
                    return;
                }
                m_NumUnits = value;
                RaisePropertyChanged(() => NumUnits);
            }
        }
    }
}