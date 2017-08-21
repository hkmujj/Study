using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.HXD3.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class TowEleMachineViewModel : NotificationObject
    {
        public ObservableCollection<TowEleMachineItem> TowEleMachineCollection { set; get; }

        public TowEleMachineViewModel()
        {
            if (GlobalParam.Instance.TowEleMachineConfigs != null)
            {
                CreateItems();
            }
        }

        private void CreateItems()
        {
            TowEleMachineCollection =
                new ObservableCollection<TowEleMachineItem>(
                    GlobalParam.Instance.TowEleMachineConfigs.Select(s => new TowEleMachineItem(s)));
        }
    }
}