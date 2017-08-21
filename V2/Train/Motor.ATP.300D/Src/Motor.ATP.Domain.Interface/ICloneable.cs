using System;

namespace Motor.ATP.Domain.Interface
{
    public interface ICloneable<out T> : ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T Clone();
    }
}
