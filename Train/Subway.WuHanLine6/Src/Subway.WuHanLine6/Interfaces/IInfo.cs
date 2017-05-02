using System;

namespace Subway.WuHanLine6.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface IInfo : ICloneable
    {
        /// <summary>
        /// 标识
        /// </summary>
        int Key { get; }

        /// <summary>
        /// 是否确认
        /// </summary>
        bool IsConfirm { get; }

        /// <summary>
        /// 确认
        /// </summary>
        void Confirm();
    }
}