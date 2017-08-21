using System.Collections.ObjectModel;

namespace Motor.ATP.Domain.Interface
{
    public interface IATPProjectManager
    {
        ReadOnlyCollection<IATP> ATPCollection { get; }

        IATP GetOrCreateATP(ScreenIdentity identity);

    }
}
