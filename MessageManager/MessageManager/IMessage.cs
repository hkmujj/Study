using System;
using System.ComponentModel;

namespace MessageManager
{
    /// <summary>
    /// Message's Interface
    /// </summary>
    public interface IMessage : ICloneable, IEquatable<IMessage>, INotifyPropertyChanged
    {
        /// <summary>
        /// Message's ID
        /// </summary>
        int ID { get; }
        /// <summary>
        /// Message's Content
        /// </summary>
        string Content { get; }
    }
}
