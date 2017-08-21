using System.Collections.ObjectModel;
using Urban.Domain.TrainState.Interface;

namespace Urban.Domain.TrainState.Model
{
    public class TrainLine : ITrainLine
    {
        public string TrainLineNumber { get; set; }

        public ReadOnlyCollection<IStation> StationCollection { get; set; }

        public IStation CurrentStation { get; set; }

        public IStation NextStation { get; set; }

        public IStation PreviousStation { get; set; }
    }
}