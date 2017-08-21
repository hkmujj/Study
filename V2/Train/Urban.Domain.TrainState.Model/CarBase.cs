using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Model
{
    public abstract class CarBase : UpdatingProvider<CarBase>, ICar
    {
        private ReadOnlyCollection<IPantograph> m_PantographCollection;
        private ReadOnlyCollection<Pantograph> m_Pantographs;
        private ReadOnlyCollection<IDoor> m_DoorCollection;
        private ReadOnlyCollection<Door> m_ConcreteDoorCollection;
        private Bogies m_Bogies;
        private CarBraking m_CarBraking;

        private ReadOnlyCollection<AirCylinder> m_AirCylinder;
        private ReadOnlyCollection<IAirCylinder> m_ReadOnlyAirCylinders;
        private ReadOnlyCollection<IHVAC> m_ReadonlyHvacCollection;
        private ReadOnlyCollection<HVAC> m_HvacCollection;
        private ReadOnlyCollection<IPassenger> m_ReadonlyPassengerCollection;
        private ReadOnlyCollection<Passenger> m_PassengerCollection;
        public int CarNumber { get; set; }

        public ITrain Parent { get; set; }

        public double Temperature { get; set; }

        public string CarName { get; set; }

        public ITrainUnit TrainUnit { get; set; }

        public CarPower Power { get; protected set; }

        ICarPower ICar.Power
        {
            get { return Power; }
        }

        public IToilet Toilet { get; set; }

        ReadOnlyCollection<IPantograph> ICar.Pantographs { get { return m_PantographCollection; } }

        public ReadOnlyCollection<Pantograph> Pantographs
        {
            set
            {
                m_Pantographs = value ?? new ReadOnlyCollection<Pantograph>(new List<Pantograph>());
                foreach (var pantograph in m_Pantographs)
                {
                    pantograph.Parent = this;
                }
                m_PantographCollection = m_Pantographs.Cast<IPantograph>().ToList().AsReadOnly();
            }
            get { return m_Pantographs; }
        }

        ReadOnlyCollection<IDoor> ICar.Doors { get { return m_DoorCollection; } }

        public ReadOnlyCollection<Door> Doors
        {
            set
            {
                m_ConcreteDoorCollection = value ?? new ReadOnlyCollection<Door>(new List<Door>());
                foreach (var door in m_ConcreteDoorCollection)
                {
                    door.Parent = this;
                }
                m_DoorCollection = m_ConcreteDoorCollection.Cast<IDoor>().ToList().AsReadOnly();
            }
            get { return m_ConcreteDoorCollection; }
        }

        IBogies ICar.Bogies { get { return Bogies; } }

        ICarBraking ICar.CarBraking { get { return m_CarBraking; } }

        public ReadOnlyCollection<AirCylinder> AirCylinder
        {
            get { return m_AirCylinder; }
            set
            {
                m_AirCylinder = value;
                m_ReadOnlyAirCylinders = m_AirCylinder.Cast<IAirCylinder>().ToList().AsReadOnly();
            }
        }

        ReadOnlyCollection<IAirCylinder> ICar.AirCylinder { get { return m_ReadOnlyAirCylinders; } }

        public CarBraking CarBraking
        {
            set
            {
                m_CarBraking = value;
                if (m_CarBraking != null)
                {
                    m_CarBraking.Parent = this;
                }
            }
            get { return m_CarBraking; }
        }

        public Bogies Bogies
        {
            set
            {
                m_Bogies = value;
                if (m_Bogies != null)
                {
                    m_Bogies.Parent = this;
                }
            }
            get { return m_Bogies; }
        }

        public ReadOnlyCollection<HVAC> HvacCollection
        {
            set
            {
                m_HvacCollection = value;
                m_ReadonlyHvacCollection = value.Cast<IHVAC>().ToList().AsReadOnly();
            }
            get { return m_HvacCollection; }
        }

        public ReadOnlyCollection<Passenger> PassengerCollection
        {
            set
            {
                m_PassengerCollection = value;
                m_ReadonlyPassengerCollection =
                    m_PassengerCollection.Cast<IPassenger>().ToList().AsReadOnly();
            }
            get { return m_PassengerCollection; }
        }

        ReadOnlyCollection<IPassenger> ICar.PassengerCollection
        {
            get { return m_ReadonlyPassengerCollection; }
        }

        ReadOnlyCollection<IHVAC> ICar.HvacCollection
        {
            get { return m_ReadonlyHvacCollection; }
        }

        protected CarBase()
        {
            Pantographs = new ReadOnlyCollection<Pantograph>(new List<Pantograph>());
            Doors = new ReadOnlyCollection<Door>(new List<Door>());
            Bogies = new Bogies();
            CarBraking = new CarBraking();
            AirCylinder = (new List<AirCylinder>()).AsReadOnly();
            Power = new CarPower();
            PassengerCollection = new ReadOnlyCollection<Passenger>(new List<Passenger>());
        }

        public override void Update(IUpdateParam updateParam)
        {
            base.Update(updateParam);

            Power.Update(updateParam);

            foreach (var pantograph in Pantographs)
            {
                pantograph.Update(updateParam);
            }

            foreach (var door in m_ConcreteDoorCollection)
            {
                door.Update(updateParam);
            }

            m_CarBraking.Update(updateParam);

            foreach (var cylinder in AirCylinder)
            {
                cylinder.Update(updateParam);
            }

            foreach (var passenger in PassengerCollection)
            {
                passenger.Update(updateParam);
            }

            m_Bogies.Update(updateParam);
        }

        public object Type { get; set; }
    }
}