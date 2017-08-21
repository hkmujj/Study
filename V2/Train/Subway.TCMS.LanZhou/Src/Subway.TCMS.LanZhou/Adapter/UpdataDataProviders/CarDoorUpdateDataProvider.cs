using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    public class CarDoorUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarDoorUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

            foreach (var car in ViewModel.TrainViewModel.Model.CarModels)
            {
                for (int i = 0; i < car.DoorModel.DoorItems.Count; ++i)
                {
                    var it = car.DoorModel.DoorItems[i];

                    it.State = GetDoorState(args, it.ItemConfig, i);
                }
            }
        }

        private DoorState GetDoorState(CommunicationDataChangedArgs args, DoorConfig it, int index)
        {

            if (DataService.ReadService.GetInBoolOf(GetOpenIndexName(it, index)))
            {
                return DoorState.Open;
            }
            if (DataService.ReadService.GetInBoolOf(GetEmergencyReleaseIndexName(it, index)))
            {
                return DoorState.EmergencyRelease;
            }
            if (DataService.ReadService.GetInBoolOf(GetDetectsObstaclesIndexName(it, index)))
            {
                return DoorState.DetectsObstacles;
            }
            if (DataService.ReadService.GetInBoolOf(GetClosedIndexName(it, index)))
            {
                return DoorState.Closed;
            }
            if (DataService.ReadService.GetInBoolOf(GetFaultIndexName(it, index)))
            {
                return DoorState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(GetResectionIndexName(it, index)))
            {
                return DoorState.Resection;
            }
            if (DataService.ReadService.GetInBoolOf(GetSwitchIndexName(it, index)))
            {
                return DoorState.Switch;
            }
            return DoorState.Unknow;
        }

        private string GetFaultIndexName(DoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexFault;
                case 1:
                    return it.Door2IndexFault;
                case 2:
                    return it.Door3IndexFault;
                case 3:
                    return it.Door4IndexFault;
                case 4:
                    return it.Door5IndexFault;
                case 5:
                    return it.Door6IndexFault;
                case 6:
                    return it.Door7IndexFault;
                case 7:
                    return it.Door8IndexFault;
                case 8:
                    return it.Door9IndexFault;
                case 9:
                    return it.Door10IndexFault;
            }
            return String.Empty;
        }

        private string GetSwitchIndexName(DoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexSwitch;
                case 1:
                    return it.Door2IndexSwitch;
                case 2:
                    return it.Door3IndexSwitch;
                case 3:
                    return it.Door4IndexSwitch;
                case 4:
                    return it.Door5IndexSwitch;
                case 5:
                    return it.Door6IndexSwitch;
                case 6:
                    return it.Door7IndexSwitch;
                case 7:
                    return it.Door8IndexSwitch;
                case 8:
                    return it.Door9IndexSwitch;
                case 9:
                    return it.Door10IndexSwitch;
            }
            return String.Empty;
        }

        private string GetResectionIndexName(DoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexResection;
                case 1:
                    return it.Door2IndexResection;
                case 2:
                    return it.Door3IndexResection;
                case 3:
                    return it.Door4IndexResection;
                case 4:
                    return it.Door5IndexResection;
                case 5:
                    return it.Door6IndexResection;
                case 6:
                    return it.Door7IndexResection;
                case 7:
                    return it.Door8IndexResection;
                case 8:
                    return it.Door9IndexResection;
                case 9:
                    return it.Door10IndexResection;
            }
            return String.Empty;
        }

        private string GetEmergencyReleaseIndexName(DoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexEmergencyRelease;
                case 1:
                    return it.Door2IndexEmergencyRelease;
                case 2:
                    return it.Door3IndexEmergencyRelease;
                case 3:
                    return it.Door4IndexEmergencyRelease;
                case 4:
                    return it.Door5IndexEmergencyRelease;
                case 5:
                    return it.Door6IndexEmergencyRelease;
                case 6:
                    return it.Door7IndexEmergencyRelease;
                case 7:
                    return it.Door8IndexEmergencyRelease;
                case 8:
                    return it.Door9IndexEmergencyRelease;
                case 9:
                    return it.Door10IndexEmergencyRelease;
            }
            return String.Empty;
        }

        private string GetDetectsObstaclesIndexName(DoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexDetectsObstacles;
                case 1:
                    return it.Door2IndexDetectsObstacles;
                case 2:
                    return it.Door3IndexDetectsObstacles;
                case 3:
                    return it.Door4IndexDetectsObstacles;
                case 4:
                    return it.Door5IndexDetectsObstacles;
                case 5:
                    return it.Door6IndexDetectsObstacles;
                case 6:
                    return it.Door7IndexDetectsObstacles;
                case 7:
                    return it.Door8IndexDetectsObstacles;
                case 8:
                    return it.Door9IndexDetectsObstacles;
                case 9:
                    return it.Door10IndexDetectsObstacles;
            }
            return String.Empty;
        }

        private string GetClosedIndexName(DoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexClosed;
                case 1:
                    return it.Door2IndexClosed;
                case 2:
                    return it.Door3IndexClosed;
                case 3:
                    return it.Door4IndexClosed;
                case 4:
                    return it.Door5IndexClosed;
                case 5:
                    return it.Door6IndexClosed;
                case 6:
                    return it.Door7IndexClosed;
                case 7:
                    return it.Door8IndexClosed;
                case 8:
                    return it.Door9IndexClosed;
                case 9:
                    return it.Door10IndexClosed;
            }
            return String.Empty;
        }

        private string GetOpenIndexName(DoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexOpen;
                case 1:
                    return it.Door2IndexOpen;
                case 2:
                    return it.Door3IndexOpen;
                case 3:
                    return it.Door4IndexOpen;
                case 4:
                    return it.Door5IndexOpen;
                case 5:
                    return it.Door6IndexOpen;
                case 6:
                    return it.Door7IndexOpen;
                case 7:
                    return it.Door8IndexOpen;
                case 8:
                    return it.Door9IndexOpen;
                case 9:
                    return it.Door10IndexOpen;
            }
            return String.Empty;
        }

    }
}
