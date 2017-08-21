using System.Diagnostics;
using Urban.Domain.TrainState.Interface.Infomation;

namespace Urban.Domain.TrainState.Model.Infomation
{
    [DebuggerDisplay("Id ={Id} Priority = {Priority} MessageContent = {MessageContent}")]
    public class InfomationItemContent : IInfomationItemContent
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string MessageContent { get; set; }
        public object Other { get; set; }

        [DebuggerStepThrough]
        public InfomationItemContent()
        {
            Priority = int.MaxValue;
        }
    }
}