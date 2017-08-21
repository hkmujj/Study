using System.Collections.Generic;
using System.Drawing;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Model
{
    public class Door : UpdatingProvider<Door>, IDoor
    {
        public int CarNumber { get; set; }

        public ICar Parent { get; set; }

        public bool IsFault
        {
            get { return DoorState.Fault == DoorState; }
            set
            {
                if (value)
                {
                    DoorState = DoorState.Fault;
                }
            }
        }

        public Dictionary<DoorState, Image> ImagesDictionary { get; set; }

        public bool Trainside { get; set; }

        public string FaultInfo { get; set; }

        public string CanCutPartName { get; set; }

        public UseState UseState { get; set; }

        public string Name { get; set; }

        public DoorState DoorState { get; set; }

        public object Identity { get; set; }
    }
}