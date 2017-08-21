using System.Collections.Generic;
using System.Linq;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Statues;
using Urban.Domain.TrainState.Model;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Config.ConfigModel;

namespace Urban.Iran.HMI.Domain
{
    public class IranTrain : TrainBase
    {
        public IranTrain()
        {
            var cars = new List<IranCar>()
                       {
                           new IranCar(){ CarName = CarNameConstant.Car01011},
                           new IranCarWithPantograph(){ CarName = CarNameConstant.Car01012},
                           new IranCar(){ CarName = CarNameConstant.Car01013},
                           new IranCarWithPantograph(){ CarName = CarNameConstant.Car01014},
                           new IranCar(){ CarName = CarNameConstant.Car01015},
                       };

            var detailConfig = GlobleParam.Instance.DetailConfig;

            InitalizeDoors(detailConfig, cars);

            InitalizeBogiesBrake(detailConfig, cars);

            InitalizeCarBraking(detailConfig, cars);

            cars[1].Pantographs[0].ListeningModelCollection =
                (new List<ListeningModel>() { new ListeningModel() { Name = "Mp1车 受电弓 升", Type = CommunicationIndexType.InBool } });
            cars[1].Pantographs[0].Updating += OnUpdatingPantograph;

            cars[3].Pantographs[0].ListeningModelCollection =
                (new List<ListeningModel>() { new ListeningModel() { Name = "Mp2车 受电弓 升", Type = CommunicationIndexType.InBool } });
            cars[3].Pantographs[0].Updating += OnUpdatingPantograph;

            Cars = cars.Cast<CarBase>().ToList().AsReadOnly();

        }

        private void InitalizeCarBraking(IranDetailConfig detailConfig, List<IranCar> cars)
        {
            foreach (var carListening in detailConfig.CarConfig.CarListeningCollection)
            {
                var targetCar = cars.Find(f => f.CarName == carListening.CarName);

                targetCar.CarBraking.ListeningModelCollection = carListening.CarBrakingListeningCollection.ToList();
                targetCar.CarBraking.Updating += OnCarBrakingUpdating;
            }
        }

        private void OnCarBrakingUpdating(CarBraking target, IUpdateParam updateParam)
        {
             baseClass baseClass = updateParam.BaseClass;
            CommunicationIndexFacade communicationIndexFacade = updateParam.IndexFacade;
            var parking = target.Brakings.First(f => f.BrakingLevel == BrakingLevel.Parking);
            parking.BrakingState = BrakingState.Relase;
            if (baseClass.BoolList[communicationIndexFacade.FindIndex(target.ListeningModelCollection[1].Type, target.ListeningModelCollection[1].Name)])
            {
                parking.BrakingState = BrakingState.CutOut;
            }
            else if (baseClass.BoolList[communicationIndexFacade.FindIndex(target.ListeningModelCollection[2].Type, target.ListeningModelCollection[2].Name)])
            {
                parking.BrakingState = BrakingState.Relase;
            }
            else if (baseClass.BoolList[communicationIndexFacade.FindIndex(target.ListeningModelCollection[0].Type, target.ListeningModelCollection[0].Name)])
            {
                parking.BrakingState = BrakingState.Apply;
            }

            var emg = target.Brakings.First(f => f.BrakingLevel == BrakingLevel.Emergency);
            emg.BrakingState = BrakingState.Unkown;
            if (baseClass.BoolList[communicationIndexFacade.FindIndex(target.ListeningModelCollection[3].Type, target.ListeningModelCollection[3].Name)])
            {
                emg.BrakingState = BrakingState.Apply;
            }
            else if (baseClass.BoolList[communicationIndexFacade.FindIndex(target.ListeningModelCollection[4].Type, target.ListeningModelCollection[4].Name)])
            {
                emg.BrakingState = BrakingState.Relase;
            }
        }

        private void InitalizeBogiesBrake(IranDetailConfig detailConfig, List<IranCar> cars)
        {
            foreach (var bogey in detailConfig.BogiesConfig.BogiesListeningCollection)
            {
                var targetCar = cars.Find(f => f.CarName == bogey.CarName);

                targetCar.Bogies.Axises = bogey.AxisListeningCollection.OrderBy(o => o.AxisName).GroupBy(g => g.AxisName).Select(s =>
                {
                    var braking = new Braking() { ListeningModelCollection = s.Cast<ListeningModel>().ToList() };
                    braking.Updating += OnAxiseBrakingUpdating;
                    var axis = new Axis() { Braking = braking , Identity = s.Key};
                    axis.Updating += OnAxisUpdating;
                    return axis;
                }).ToList().AsReadOnly();
            }
        }

        private void OnAxiseBrakingUpdating(Braking target, IUpdateParam updateParam)
        {
            baseClass baseClass=updateParam.BaseClass;
            CommunicationIndexFacade communicationIndexFacade = updateParam.IndexFacade;

            target.BrakingState = BrakingState.Unkown;
            if (baseClass.BoolList[communicationIndexFacade.FindIndex(target.ListeningModelCollection[2].Type, target.ListeningModelCollection[2].Name)])
            {
                target.BrakingState = BrakingState.CutOut;
            }
            else if (baseClass.BoolList[communicationIndexFacade.FindIndex(target.ListeningModelCollection[3].Type, target.ListeningModelCollection[3].Name)])
            {
                target.BrakingState = BrakingState.Fault;
            }
            else if (baseClass.BoolList[communicationIndexFacade.FindIndex(target.ListeningModelCollection[1].Type, target.ListeningModelCollection[1].Name)])
            {
                target.BrakingState = BrakingState.Relase;
            }
            else if (baseClass.BoolList[communicationIndexFacade.FindIndex(target.ListeningModelCollection[0].Type, target.ListeningModelCollection[0].Name)])
            {
                target.BrakingState = BrakingState.Apply;
            }
        }

        private void OnAxisUpdating(Axis target, IUpdateParam updateParam)
        {
        }

        private void InitalizeDoors(IranDetailConfig detailConfig, List<IranCar> cars)
        {
            var doorListenings = detailConfig.DoorConfig.DoorListeningModelCollection.GroupBy(g => g.CarName);

            foreach (var doorListening in doorListenings)
            {
                var targetCar = cars.Find(f => f.CarName == doorListening.Key);
                targetCar.Doors = doorListening.OrderBy(o => o.DoorName)
                                               .GroupBy(g => g.DoorName)
                                               .Select(s =>
                                               {
                                                   var door = new Door { Name = s.Key, ListeningModelCollection = s.Cast<ListeningModel>().ToList() };
                                                   door.Updating += OnDoorUpdating;
                                                   return door;
                                               })
                                               .ToList().AsReadOnly();
            }
        }

        private void OnDoorUpdating(Door target,IUpdateParam updateParam)
        {
             baseClass baseClass = updateParam.BaseClass;
            CommunicationIndexFacade communicationIndexFacade = updateParam.IndexFacade;
            target.DoorState = DoorState.Close;
            target.UseState = UseState.Use;
            if (baseClass.BoolList[communicationIndexFacade.FindIndex(CommunicationIndexType.InBool, target.ListeningModelCollection[3].Name)])
            {
                target.DoorState = DoorState.EmergencyUnlock;
            }
            else if (baseClass.BoolList[communicationIndexFacade.FindIndex(CommunicationIndexType.InBool, target.ListeningModelCollection[2].Name)])
            {
                target.DoorState = DoorState.Fault;
            }
            else if (baseClass.BoolList[communicationIndexFacade.FindIndex(CommunicationIndexType.InBool, target.ListeningModelCollection[1].Name)])
            {
                target.DoorState = DoorState.Unkown;
                target.UseState = UseState.Cut;
            }
            else if (baseClass.BoolList[communicationIndexFacade.FindIndex(CommunicationIndexType.InBool, target.ListeningModelCollection[0].Name)])
            {
                target.DoorState = DoorState.Open;
            }
        }

        private void OnUpdatingPantograph(Pantograph target,IUpdateParam updateParam)
        {
             baseClass baseClass = updateParam.BaseClass;
            CommunicationIndexFacade communicationIndexFacade = updateParam.IndexFacade;
            var model = target.ListeningModelCollection[0];
            var state = baseClass.BoolList[communicationIndexFacade.FindIndex(model.Type, model.Name)];
            target.State = state ? PantographState.Up : PantographState.Down;
        }
    }
}