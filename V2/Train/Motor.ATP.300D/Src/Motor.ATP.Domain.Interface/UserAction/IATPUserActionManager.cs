using System.Collections.ObjectModel;

namespace Motor.ATP.Domain.Interface.UserAction
{
    public interface IATPUserActionManager
    {
        ReadOnlyCollection<IATPButton>  ATPButtons { get; }
    }
}