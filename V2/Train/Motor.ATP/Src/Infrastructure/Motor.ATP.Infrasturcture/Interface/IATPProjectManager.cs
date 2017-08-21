using System.Collections.ObjectModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IATPProjectManager
    {
        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<IATP> ATPCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        IATP GetOrCreateATP(ScreenIdentity identity);

    }
}
