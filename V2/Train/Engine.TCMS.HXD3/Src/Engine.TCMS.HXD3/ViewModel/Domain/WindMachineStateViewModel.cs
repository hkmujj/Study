using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.TCMS.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class WindMachineStateViewModel :NotificationObject
    {
        public List<WindMachineItem> WindMachineItems { private set; get; }

        public IEnumerable<WindMachineItem> Row1 {get { return WindMachineItems.Where(w => w.Config.Row == 1); } }

        public IEnumerable<WindMachineItem> Row2 {get { return WindMachineItems.Where(w => w.Config.Row == 2); } }

        public WindMachineStateViewModel()
        {
            if (GlobalParam.Instance.WindMachineStateConfigs != null)
            {
                WindMachineItems= GlobalParam.Instance.WindMachineStateConfigs.Select(s => new WindMachineItem(s)).ToList();
            }
        }
    }
}