using System.ComponentModel;

namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// ATP 的一部分
    /// </summary>
    public interface IATPPartial : INotifyPropertyChanged
    {
        /// <summary>
        /// ATP 
        /// </summary>
        IATP Parent { get; }
    }
}
