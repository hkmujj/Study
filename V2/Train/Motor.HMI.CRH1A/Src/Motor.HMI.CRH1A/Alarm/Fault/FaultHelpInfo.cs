namespace Motor.HMI.CRH1A.Alarm.Fault
{
    /// <summary>
    /// 功能描述 GT_FaultAView类 
    ///    A类故障信息显示界面
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-23
    /// </summary>
    public class FaultHelpInfo
    {
        /// <summary>
        /// 描述信息
        /// </summary>
        public string HelpDescription { get; set; }

        /// <summary>
        /// 背景信息
        /// </summary>
        public string HelpBackground { get; set; }

        /// <summary>
        /// 现象信息
        /// </summary>
        public string HelpXianXiang { get;set; }

        /// <summary>
        /// 对应故障编号
        /// </summary>
        private int FaultNo { get; set; }
    }
}