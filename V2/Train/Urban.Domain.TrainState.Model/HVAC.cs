using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Model
{
    public class HVAC : UpdatingProvider<HVAC>,IHVAC
    {
        public int CarNumber { get; set; }
        public ICar Parent { get; set; }
        public double Temperature { get; set; }
        public bool IsFault { get; set; }
        public string FaultInfo { get; set; }
        public string CanCutPartName { get; set; }
        public UseState UseState { get; set; }
    }
}