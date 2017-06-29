namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 最高可用驾驶模式
    /// </summary>
    public enum HighDirveModel
    {
        /// <summary>
        /// 不显示
        /// </summary>
        Normal,

        /// <summary>
        /// RM模式
        /// </summary>
        RM,



        /// <summary>
        /// SM模式-点式
        /// </summary>
        SMI,

        /// <summary>
        /// SM模式-连续式
        /// </summary>
        SMC,

        /// <summary>
        /// AM模式-点式
        /// </summary>
        AMI,

        /// <summary>
        /// AM模式-连续式
        /// </summary>
        AMC,



        /// <summary>
        /// CM模式-点式
        /// </summary>
        CMI,

        /// <summary>
        /// CM模式-连续式
        /// </summary>
        CMC,
        /// <summary>
        /// ACM后备模式
        /// </summary>
        ACMBM,
        /// <summary>
        /// AMC-CBTC
        /// </summary>
        AMCCBTC,

    }
    /// <summary>
    /// 最高可用驾驶模式颜色
    /// Casco信号资料编写
    /// </summary>
    public enum HightDriveModelColor
    {
        /// <summary>
        /// 白色
        /// </summary>
        White,
        /// <summary>
        /// 红色
        /// </summary>
        Red,
    }
}