using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Infomation;
using Urban.Domain.TrainState.Interface.Statues;
using Urban.Domain.TrainState.Model.Common;
using Urban.Domain.TrainState.Model.Infomation;

namespace Urban.Domain.TrainState.Model
{
    public abstract class TrainBase : UpdatingProvider<TrainBase>, ITrain
    {
        protected readonly UpdateParam m_UpdateParam;

        private ReadOnlyCollection<ICar> m_Cars;

        private ReadOnlyCollection<CarBase> m_CarBases;

        private ReadOnlyCollection<IBraking> m_ReadonlyBrakingCollection;
        private ReadOnlyCollection<Braking> m_BrakingCollection;

        public TrainType TrainType { get; private set; }

        public ATP ATP { get; protected set; }

        public Power Power { get; protected set; }

        IPower ITrain.Power
        {
            get { return Power; }
        }

        IATP ITrain.ATP
        {
            get { return ATP; }
        }

        public ITraction Traction { get; private set; }

        ReadOnlyCollection<IBraking> ITrain.BrakingCollection { get { return m_ReadonlyBrakingCollection; } }

        public ReadOnlyCollection<Braking> BrakingCollection
        {
            protected set
            {
                m_BrakingCollection = value ?? new ReadOnlyCollection<Braking>(new List<Braking>());
                m_ReadonlyBrakingCollection = m_BrakingCollection.Cast<IBraking>().ToList().AsReadOnly();
            }
            get { return m_BrakingCollection; }
        }

        public Speed Speed { protected set; get; }

        ISpeed ITrain.Speed { get { return Speed; } }

        public ReadOnlyCollection<ITrainUnit> TrainUnits { get; private set; }

        ReadOnlyCollection<ICar> ITrain.Cars
        {
            get { return m_Cars; }
        }

        public ReadOnlyCollection<CarBase> Cars
        {
            protected set
            {
                m_CarBases = value;
                if (value == null)
                {
                    m_CarBases = new ReadOnlyCollection<CarBase>(new List<CarBase>());
                }
                m_Cars = m_CarBases.Cast<ICar>().ToList().AsReadOnly();
            }
            get { return m_CarBases; }
        }

        public bool Visible { get; set; }

        public TrainLine TrainLine { set; get; }

        public InfomationCollection InfomationCollection { protected set; get; }

        IInfomationCollection ITrain.InfomationCollection
        {
            get { return InfomationCollection; }
        }

        ITrainLine ITrain.TrainLine
        {
            get { return TrainLine; }
        }

        protected TrainBase()
        {
            m_UpdateParam = new UpdateParam(); 
            InfomationCollection = new InfomationCollection();
            Speed = new Speed();
            BrakingCollection = new ReadOnlyCollection<Braking>(new Braking[0]);
            Cars = new ReadOnlyCollection<CarBase>(new List<CarBase>());
            ATP = new ATP();
            Power = new Power();
        }

        public override void Update(IUpdateParam updateParam)
        {
            OnUpdating(this, updateParam);

            Speed.Update(updateParam);

            Power.Update(updateParam);

            foreach (var braking in BrakingCollection)
            {
                braking.Update(updateParam);
            }

            ATP.Update(updateParam);

            foreach (var car in Cars)
            {
                car.Update(updateParam);
            }
        }

        public void VoluntaryResponse(ICommunicationDataService dataService)
        {
            m_UpdateParam.DataService = dataService;
            m_UpdateParam.ChangedBools = null;
            m_UpdateParam.ChangedFloats = null;

            Update(m_UpdateParam);
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> communicationDataChangedArgs)
        {
            m_UpdateParam.ChangedBools = communicationDataChangedArgs;
            m_UpdateParam.ChangedFloats = null;

            Update(m_UpdateParam);
        }

        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> communicationDataChangedArgs)
        {
            m_UpdateParam.ChangedBools = null;
            m_UpdateParam.ChangedFloats = communicationDataChangedArgs;

            Update(m_UpdateParam);
        }
    }
}