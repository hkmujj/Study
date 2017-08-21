namespace CRH2MMI.DoorInfo
{
    /// <summary>
    /// 门的状态
    /// </summary>
    internal enum DoorState
    {
        Null,

        /// <summary>
        /// 开
        /// </summary>
        Open,

        /// <summary>
        /// 关
        /// </summary>
        Close,

        /// <summary>
        /// 压紧
        /// </summary>
        Press,

        /// <summary>
        /// 释放
        /// </summary>
        Relase,

        /// <summary>
        /// 故障
        /// </summary>
        Fault,

        /// <summary>
        /// 切除
        /// </summary>
        Cut,
    }

    public enum DoorLocation
    {
        /// <summary>
        /// 车门1位
        /// </summary>
        Door1,

        /// <summary>
        /// 车门2位
        /// </summary>
        Door2,

        /// <summary>
        /// 车门3位
        /// </summary>
        Door3,

        /// <summary>
        /// 车门4位
        /// </summary>
        Door4,

    }

    /// <summary>
    /// 车门操作
    /// </summary>
    public enum DoorOperType
    {

        /// <summary>
        /// 压紧
        /// </summary>
        PressRelase,

        /// <summary>
        /// 开 关
        /// </summary>
        OpenClose,
    }
}
