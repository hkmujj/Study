namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubsystemConfig
    {
        /// <summary>
        /// 子系统类型
        /// </summary>
        SubsystemType SubsystemType { get; }

        /// <summary>
        /// 子系统工程类型
        /// </summary>
        ProjectType ProjectType { get; }

        /// <summary>
        /// 是否需要加载
        /// </summary>
        bool NeedLoad { get; }

        /// <summary>
        /// 子系统名字
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 子系统dll
        /// </summary>
        string Dll { get; }

        /// <summary>
        /// 入口类
        /// </summary>
        string EntryClass { get; }
        /// <summary>
        /// PTT共享内存名称
        /// </summary>
        string ShareName { get; }
        /// <summary>
        /// PTT共享内存Index
        /// </summary>
        int ShareIndex { get; }
    }
}