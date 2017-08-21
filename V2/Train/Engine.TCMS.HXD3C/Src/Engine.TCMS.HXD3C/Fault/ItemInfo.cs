namespace Engine.TCMS.HXD3C.Fault
{
    /// <summary>
    /// 故障结构体
    /// </summary>
    public struct ItemInfo
    {
        /// <summary>
        /// 故障图标类型
        /// </summary>
        public string m_PicType;

        /// <summary>
        /// 故障代码
        /// </summary>
        public string m_Code;

        /// <summary>
        /// 故障开始时间
        /// </summary>
        public string m_StartTime;

        /// <summary>
        /// 故障结束时间
        /// </summary>
        public string m_OverTime;

        /// <summary>
        /// 故障名
        /// </summary>
        public string m_FaultName;

        /// <summary>
        /// 解决编号名称
        /// </summary>
        public string m_SolveName;

        /// <summary>
        /// 故障是否结束
        /// </summary>
        public bool m_EventOver;

        /// <summary>
        /// 网压
        /// </summary>
        public string m_WangYa;

        /// <summary>
        /// 电流
        /// </summary>
        public string m_DianLiu;

        /// <summary>
        /// 列车状态(暂时不知道什么意思，到时再改)
        /// </summary>
        public string m_TrainState;

        /// <summary>
        /// 级位
        /// </summary>
        public string m_JiWei;

        /// <summary>
        /// 列车速度
        /// </summary>
        public string m_TrainSpeed;
    }
}