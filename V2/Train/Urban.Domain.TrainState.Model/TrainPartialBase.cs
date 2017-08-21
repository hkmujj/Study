using Microsoft.Practices.Prism.ViewModel;
using Urban.Domain.TrainState.Interface;

namespace Urban.Domain.TrainState.Model
{
    public abstract class TrainPartialBase : NotificationObject, ITrainPartial
    {
        public TrainBase Parent { protected set; get; }

        ITrain ITrainPartial.Parent { get { return Parent; } }

        protected TrainPartialBase(TrainBase parent)
        {
            Parent = parent;
        }

    }
}