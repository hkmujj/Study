using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Model
{
    public class CarBraking : UpdatingProvider<CarBraking>, ICarBraking
    {
        private ReadOnlyCollection<IBraking> m_BrakingCollection;
        private ReadOnlyCollection<Braking> m_ConcreteBrakingCollection;

        public string CanCutPartName { get; set; }

        public UseState UseState { get; set; }

        public int CarNumber { get; set; }

        public ICar Parent { get; set; }

        ReadOnlyCollection<IBraking> ICarBraking.Brakings { get { return m_BrakingCollection; } }
        public ReadOnlyCollection<Braking> Brakings
        {
            set
            {
                m_ConcreteBrakingCollection = value ?? new ReadOnlyCollection<Braking>(new List<Braking>());
                m_BrakingCollection = m_ConcreteBrakingCollection.Cast<IBraking>().ToList().AsReadOnly();
            }
            get { return m_ConcreteBrakingCollection; }
        }

        public CarBraking()
        {
            Brakings = new ReadOnlyCollection<Braking>(new List<Braking>());
        }
    }
}