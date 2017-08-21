using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Model
{
    public class Pantograph : ListeningModelProviderBase, IPantograph, IUpdatingEventProvider<Pantograph>
    {
        public event UpdateEvent<Pantograph> Updating;

        public bool IsFault { get; set; }

        public string FaultInfo { get; set; }

        public int CarNumber { get; set; }

        public ICar Parent { get; set; }

        public string CanCutPartName { get; set; }

        public UseState UseState { get; set; }

        public PantographState State { get; set; }

        public string Name { get; set; }

        public void Update(IUpdateParam updateParam)
        {
            if (Updating != null)
            {
                Updating(this, updateParam);
            }
        }
    }
}