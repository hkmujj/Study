using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.TCMS.CRH400BF.Config;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Constant;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Resources.Keys;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    public class CarDoorUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarDoorUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

            foreach (var car in TrainModel.CarCollection)
            {
                car.Door.Surchage1 = GetSurchage(string.Format(InbKeys.Inb超员报警提示, car.Index + 1, 1));
                car.Door.Surchage2 = GetSurchage(string.Format(InbKeys.Inb超员报警提示, car.Index + 1, 2));
                for (int i = 0; i < car.Door.DoorItems.Count; ++i)
                {
                    var it = car.Door.DoorItems[i];
                    it.State = GetDoorState(args, it.ItemConfig, i);


                }
            }
        }

        private bool GetSurchage(string inbookey)
        {
            return DataService.ReadService.GetInBoolOf(inbookey);
        }
        private DoorState GetDoorState(CommunicationDataChangedArgs args, CarDoorConfig it, int index)
        {

            if (DataService.ReadService.GetInBoolOf(GetIsolationIndexName(it, index)))
            {
                return DoorState.Isolation;
            }
            if (DataService.ReadService.GetInBoolOf(GetFaultIndexName(it, index)))
            {
                return DoorState.Fault;
            }

            if (DataService.ReadService.GetInBoolOf(GetCloseIndexName(it, index)))
            {
                return DoorState.Close;
            }
            return DoorState.Open;
        }

        private string GetCloseIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.DoorIndex1Close;
                case 1:
                    return it.DoorIndex2Close;
                case 2:
                    return it.DoorIndex3Close;
                case 3:
                    return it.DoorIndex4Close;
            }
            return String.Empty;
        }

        private string GetIsolationIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.DoorIndex1Isolation;
                case 1:
                    return it.DoorIndex2Isolation;
                case 2:
                    return it.DoorIndex3Isolation;
                case 3:
                    return it.DoorIndex4Isolation;

            }
            return String.Empty;
        }

        private string GetFaultIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.DoorIndex1Fault;
                case 1:
                    return it.DoorIndex2Fault;
                case 2:
                    return it.DoorIndex3Fault;
                case 3:
                    return it.DoorIndex4Fault;
            }
            return String.Empty;
        }

        private string GetOpenIndexName(CarDoorConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.DoorIndex1Open;
                case 1:
                    return it.DoorIndex2Open;
                case 2:
                    return it.DoorIndex3Open;
                case 3:
                    return it.DoorIndex4Open;
            }
            return String.Empty;
        }

    }
}
