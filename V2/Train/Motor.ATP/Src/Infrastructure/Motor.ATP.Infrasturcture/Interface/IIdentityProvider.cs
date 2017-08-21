namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 标识提供
    /// </summary>
    public interface IIdentityProvider
    {
        /// <summary>
        /// Identity 唯一标识 
        /// </summary>
        object Identity { get; }
    }

    /// <summary>
    /// 标识提供
    /// </summary>
    public interface IIdentityProvider<out T> : IIdentityProvider
    {
        /// <summary>
        /// Identity 唯一标识 
        /// </summary>
        new T Identity { get; }
    }
}
