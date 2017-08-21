using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.Model.Domain.SecondLevel.Details;
using Engine.TAX2.SS7C.Model.Interface;
using Engine.TAX2.SS7C.ViewModel.Domain.SecondLevel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller.Domain.SecondLevel
{
    [Export]
    //[Export(typeof(IResetSupport))]
    public class SetMonitorController : ControllerBase<SetMonitorViewModel>, IResetSupport
    {
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.MonitorItemCollection =
                new Lazy<ReadOnlyCollection<MonitorItem>>(
                    () =>
                        GlobalParam.Instance.SetMonitorItemConfigs.Value.Select(s => new MonitorItem(s))
                            .ToList()
                            .AsReadOnly());
        }

        public void Reset()
        {
            if (ViewModel.Model.MonitorItemCollection.IsValueCreated)
            {
                SeletFirst();
            }

            ViewModel.Model.SuredSelectedMonitorItem = null;
        }

        public void SeletFirst()
        {
            ViewModel.Model.SelectedMonitorItem = ViewModel.Model.MonitorItemCollection.Value.FirstOrDefault();
        }

        public void SureCurrent()
        {
            ViewModel.Model.SuredSelectedMonitorItem = ViewModel.Model.SelectedMonitorItem;
        }

        public void SeletNext()
        {
            var coll = ViewModel.Model.MonitorItemCollection.Value;
            ViewModel.Model.SelectedMonitorItem =
                coll[(coll.IndexOf(ViewModel.Model.SelectedMonitorItem) + 1)%coll.Count];
        }
    }
}