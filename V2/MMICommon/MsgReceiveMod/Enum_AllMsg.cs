namespace MsgReceiveMod
{
    /// <summary>
    /// 消息等级
    /// </summary>
    public enum MsgLevels
    {
        /// <summary>
        /// 最高等级
        /// </summary>
        Level0 = 0,

        /// <summary>
        /// 
        /// </summary>
        Level1,

        /// <summary>
        /// 
        /// </summary>
        Level2,

        /// <summary>
        /// 
        /// </summary>
        Level3,

        /// <summary>
        /// 
        /// </summary>
        Level4,

        /// <summary>
        /// 
        /// </summary>
        Level5,

        /// <summary>
        /// 
        /// </summary>
        Level6,

        /// <summary>
        /// 
        /// </summary>
        Level7,

        /// <summary>
        /// 
        /// </summary>
        Level8,

        /// <summary>
        /// 
        /// </summary>
        Level9,
    }

    /// <summary>
    /// 消息排序方式
    /// </summary>
    public enum SortCriteriaOfMsg
    {
        /// <summary>
        /// 按时间排序(时间早的在前面)
        /// </summary>
        TimeUp,

        /// <summary>
        /// 按时间排序(时间早的在后面)
        /// </summary>
        TimeDown,

        /// <summary>
        /// 按消息等级排序
        /// </summary>
        Level,

        /// <summary>
        /// 按消息是否标记排序
        /// </summary>
        Flag,

        /// <summary>
        /// 按 消息时间->消息等级 排序
        /// </summary>
        TimeThenLevel,

        /// <summary>
        /// 按 消息等级->消息时间 排序
        /// </summary>
        LevelThenTime,

        /// <summary>
        /// 按 消息是否标记->消息时间->消息等级 排序
        /// </summary>
        FlagThenTimeThenLevel,

        /// <summary>
        /// 按 消息是否标记->消息等级->消息时间 排序
        /// </summary>
        FlagThenLevelThenTime,

    }
}
