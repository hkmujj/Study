using System;

namespace MMITool.Common.Model
{
    /// <summary>
    /// 方向定义
    /// </summary>
    [Flags]
    public enum Direction 
    {
        /// <summary>
        /// 上
        /// </summary>
        Up = 0x01,
        /// <summary>
        /// 下
        /// </summary>
        Down = ~Up,
        /// <summary>
        /// 左
        /// </summary>
        Left = 0x02,
        /// <summary>
        /// 右
        /// </summary>
        Right = ~Left,
    }
}
