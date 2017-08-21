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
    public class Bogies : UpdatingProvider<Bogies>, IBogies
    {
        private ReadOnlyCollection<IAxis> m_Axises;
        private ReadOnlyCollection<Axis> m_ConcreteAxises;

        public int CarNumber { get; set; }

        public ICar Parent { get; set; }

        public bool IsFault { get; set; }

        public string FaultInfo { get; set; }

        ReadOnlyCollection<IAxis> IBogies.Axises
        {
            get { return m_Axises; }
        }

        public ReadOnlyCollection<Axis> Axises
        {
            set
            {
                m_ConcreteAxises = value ?? (new ReadOnlyCollection<Axis>(new Axis[0]));

                m_Axises = m_ConcreteAxises.Cast<IAxis>().ToList().AsReadOnly();
            }
            get { return m_ConcreteAxises; }
        }

        public Bogies()
        {
            Axises = new ReadOnlyCollection<Axis>(new Axis[0]);
        }

        public override void Update(IUpdateParam updateParam)
        {
            base.Update(updateParam);

            foreach (var axise in Axises)
            {
                axise.Update(updateParam);
            }
        }
    }
}