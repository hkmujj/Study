using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface
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
