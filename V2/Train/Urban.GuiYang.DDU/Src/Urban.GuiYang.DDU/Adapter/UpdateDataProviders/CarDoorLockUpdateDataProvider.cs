using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.Train.CarPart;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof (IUpdateDataProvider))]
    public class CarDoorLockUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarDoorLockUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

            var doors = ViewModel.TrainViewModel.Model.CarCollection.SelectMany(s => s.Door.LockStates).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnkonwIndex));

            foreach (var it in doors)
            {
                it.State = GetDoorLockState(args, it);
            }
        }

        private DoorLockState GetDoorLockState(CommunicationDataChangedArgs args, DoorLockStateItem it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.LockAllIndex))
            {
                return DoorLockState.AllLocked;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AtLastOnUnlockIndex))
            {
                return DoorLockState.AtLastOnUnlock;
            }
            return DoorLockState.Unkown;
        }
    }
}