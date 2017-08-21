using System;

namespace ES.Facility.PublicModule.Memory
{
    /// <summary>
    /// 兼容FMS用
    /// </summary>
    public class FileHead
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public int Vers { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
    }

    /// <summary>
    /// 兼容FMS用
    /// </summary>
    public class BlockHead : FileHead
    {
        /// <summary>
        /// 数据区尺寸
        /// </summary>
        public int DataSize { get; set; }

        /// <summary>
        /// 数据开始位置
        /// </summary>
        public IntPtr StartPos { get; set; }

        /// <summary>
        /// 头开始位置
        /// </summary>
        public IntPtr HeadPos { get; set; }
    }
}
