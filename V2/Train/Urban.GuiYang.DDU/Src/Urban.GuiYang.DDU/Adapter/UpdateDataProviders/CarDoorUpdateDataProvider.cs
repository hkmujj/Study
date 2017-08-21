using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof (IUpdateDataProvider))]
    public class CarDoorUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarDoorUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

            foreach (var car in ViewModel.TrainViewModel.Model.CarCollection)
            {
                for (int i = 0; i < car.Door.DoorItems.Count; ++i)
                {
                    var it = car.Door.DoorItems[i];

                    it.State = GetDoorState(args, it.ItemConfig, i);

                }
            }
        }

        private DoorState GetDoorState(CommunicationDataChangedArgs args, CarDoorConfig it, int index)
        {
            if (DataService.ReadService.GetInBoolOf(GetIsolationIndexName(it, index)))
            {
                return DoorState.Isolation;
            }
            if (DataService.ReadService.GetInBoolOf(GetFaultCloseIndexName(it, index)))
            {
                return DoorState.FaultClose;
            }
            if (DataService.ReadService.GetInBoolOf(GetOpenIndexName(it, index)))
            {
                return DoorState.Open;
            }
            if (DataService.ReadService.GetInBoolOf(GetCloseIndexName(it, index)))
            {
                return DoorState.Close;
            }
            if (DataService.ReadService.GetInBoolOf(GetCheckedObstructionIndexName(it, index)))
            {
                return DoorState.CheckedObstruction;
            }
            if (DataService.ReadService.GetInBoolOf(GetEmergLockIndexName(it, index)))
            {
                return DoorState.EmergLock;
            }
            if (DataService.ReadService.GetInBoolOf(GetFaultOpenIndexName(it, index)))
            {
                return DoorState.FaultOpen;
            }
            return DoorState.Unkonw;
        }

        private string GetFaultOpenIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexFaultOpen;
                case 1:
                    return it.Door2IndexFaultOpen;
                case 2:
                    return it.Door3IndexFaultOpen;
                case 3:
                    return it.Door4IndexFaultOpen;
                case 4:
                    return it.Door5IndexFaultOpen;
                case 5:
                    return it.Door6IndexFaultOpen;
                case 6:
                    return it.Door7IndexFaultOpen;
                case 7:
                    return it.Door8IndexFaultOpen;
            }
            return String.Empty;
        }

        private string GetIsolationIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexIsolation;
                case 1:
                    return it.Door2IndexIsolation;
                case 2:
                    return it.Door3IndexIsolation;
                case 3:
                    return it.Door4IndexIsolation;
                case 4:
                    return it.Door5IndexIsolation;
                case 5:
                    return it.Door6IndexIsolation;
                case 6:
                    return it.Door7IndexIsolation;
                case 7:
                    return it.Door8IndexIsolation;
            }
            return String.Empty;
        }

        private string GetFaultCloseIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexFaultClose;
                case 1:
                    return it.Door2IndexFaultClose;
                case 2:
                    return it.Door3IndexFaultClose;
                case 3:
                    return it.Door4IndexFaultClose;
                case 4:
                    return it.Door5IndexFaultClose;
                case 5:
                    return it.Door6IndexFaultClose;
                case 6:
                    return it.Door7IndexFaultClose;
                case 7:
                    return it.Door8IndexFaultClose;
            }
            return String.Empty;
        }

        private string GetEmergLockIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexEmergLock;
                case 1:
                    return it.Door2IndexEmergLock;
                case 2:
                    return it.Door3IndexEmergLock;
                case 3:
                    return it.Door4IndexEmergLock;
                case 4:
                    return it.Door5IndexEmergLock;
                case 5:
                    return it.Door6IndexEmergLock;
                case 6:
                    return it.Door7IndexEmergLock;
                case 7:
                    return it.Door8IndexEmergLock;
            }
            return String.Empty;
        }

        private string GetCheckedObstructionIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexCheckedObstruction;
                case 1:
                    return it.Door2IndexCheckedObstruction;
                case 2:
                    return it.Door3IndexCheckedObstruction;
                case 3:
                    return it.Door4IndexCheckedObstruction;
                case 4:
                    return it.Door5IndexCheckedObstruction;
                case 5:
                    return it.Door6IndexCheckedObstruction;
                case 6:
                    return it.Door7IndexCheckedObstruction;
                case 7:
                    return it.Door8IndexCheckedObstruction;
            }
            return String.Empty;
        }

        private string GetCloseIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.Door1IndexClose;
                case 1:
                    return it.Door2IndexClose;
                case 2:
                    return it.Door3IndexClose;
                case 3:
                    return it.Door4IndexClose;
                case 4:
                    return it.Door5IndexClose;
                case 5:
                    return it.Door6IndexClose;
                case 6:
                    return it.Door7IndexClose;
                case 7:
                    return it.Door8IndexClose;
            }
            return String.Empty;
        }

        private string GetOpenIndexName(CarDoorConfig it, int index)
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
            }
            return String.Empty;
        }

        //private PECUState GetPECUState(CommunicationDataChangedArgs args, CarDoorConfig it, int index)
        //{
        //    if (DataService.ReadService.GetInBoolOf(GetActiveIndexName(it, index)))
        //    {
        //        return PECUState.Active;
        //    }
        //    if (DataService.ReadService.GetInBoolOf(GetUsingIndexName(it, index)))
        //    {
        //        return PECUState.Using;
        //    }
        //    return PECUState.Unactive;
        //}
        //private string GetActiveIndexName(CarPECUConfig it, int index)
        //{
        //    switch (index)
        //    {
        //        case 0:
        //            return it.PECU1IndexActive;
        //        case 1:
        //            return it.PECU2IndexActive;
        //        case 2:
        //            return it.PECU3IndexActive;
        //        case 3:
        //            return it.PECU4IndexActive;
        //        case 4:
        //            return it.PECU5IndexActive;
        //        case 5:
        //            return it.PECU6IndexActive;
        //        case 6:
        //            return it.PECU7IndexActive;
        //        case 7:
        //            return it.PECU8IndexActive;

        //    }

        //    return String.Empty;
        //}
        //private string GetUsingIndexName(CarPECUConfig it, int index)
        //{
        //    switch (index)
        //    {
        //        case 0:
        //            return it.PECU1IndexUsing;
        //        case 1:
        //            return it.PECU2IndexUsing;
        //        case 2:
        //            return it.PECU3IndexUsing;
        //        case 3:
        //            return it.PECU4IndexUsing;
        //        case 4:
        //            return it.PECU5IndexUsing;
        //        case 5:
        //            return it.PECU6IndexUsing;
        //        case 6:
        //            return it.PECU7IndexUsing;
        //        case 7:
        //            return it.PECU8IndexUsing;

        //    }
        //    return String.Empty;
        //}

        //private PECUState GetPECUState(CommunicationDataChangedArgs args, CarPECUConfig it)
        //{
        //    if (DataService.ReadService.GetInBoolOf(it.PECU1IndexActive))
        //    {
        //        return PECUState.Active;
        //    }
        //    if (DataService.ReadService.GetInBoolOf(it.PECU1IndexUsing))
        //    {
        //        return PECUState.Using;
        //    }
        //    return PECUState.Unactive;
        //}
    }
}