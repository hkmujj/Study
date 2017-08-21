using System;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICloneable<out T> : ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        new T Clone();
    }
}
