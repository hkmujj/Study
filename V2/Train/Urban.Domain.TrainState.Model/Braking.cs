using System.Collections.ObjectModel;
using System.Diagnostics;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Model
{
    public class Braking : UpdatingProvider<Braking>, IBraking
    {
        public BrakingType BrakingType { get; private set; }

        public BrakingState BrakingState { get; set; }

        public BrakingLevel BrakingLevel { get; set; }

        public float Value { get; set; }

        [DebuggerStepThrough]
        public Braking(BrakingState state = BrakingState.Relase, BrakingLevel level = BrakingLevel.None, BrakingType brakingType = BrakingType.Air)
        {
            BrakingType = brakingType;
            BrakingState = state;
            BrakingLevel = level;
        }
    }
}