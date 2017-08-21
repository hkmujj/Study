using System;

namespace ES.Facility.Common
{
    /// <summary>
    /// 功能描述：故障
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class FaultInfo
    {
        /// <summary>
        /// 读取或设置故障逻辑属性
        /// </summary>
        public Int32 Logic { get; set; }
        /// <summary>
        /// 读取或设置故障等级属性
        /// </summary>
        public Int32 Grade { get; set; }
        /// <summary>
        /// 读取或设置故障代码属性
        /// </summary>
        public String Code { get; set; }
        /// <summary>
        /// 读取或设置故障内容属性
        /// </summary>
        public String Display { get; set; }
        /// <summary>
        /// 读取或设置故障开始时间属性
        /// </summary>
        public String StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }
        private String m_StartTime = "-";
        /// <summary>
        /// 读取或设置故障结束时间属性
        /// </summary>
        public String OverTime
        {
            get { return m_OverTime; }
            set { m_OverTime = value; }
        }
        private String m_OverTime = "-";

        /// <summary>
        /// 读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public String Time
        {
            get { return m_StartTime + "\n" + m_OverTime; }
        }
    }
}
