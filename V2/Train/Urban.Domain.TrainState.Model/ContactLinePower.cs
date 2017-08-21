using Urban.Domain.TrainState.Interface;

namespace Urban.Domain.TrainState.Model
{
    public class ContactLinePower : UpdatingProvider<ContactLinePower>, IContactLinePower
    {
        public float ContactLineVoltage { get; set; }   
    }
}