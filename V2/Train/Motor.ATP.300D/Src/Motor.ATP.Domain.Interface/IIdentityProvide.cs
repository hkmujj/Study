namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 标识提供
    /// </summary>
    public interface IIdentityProvide
    {
        /// <summary>
        /// Identity 唯一标识 
        /// </summary>
        object Identity { get; }
    }

    /// <summary>
    /// 标识提供
    /// </summary>
    public interface IIdentityProvide<out T> : IIdentityProvide
    {
        /// <summary>
        /// Identity 唯一标识 
        /// </summary>
        new T Identity { get; }
    }

}
