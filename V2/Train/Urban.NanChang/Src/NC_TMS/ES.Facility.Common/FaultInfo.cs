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
            get { return this._startTime; }
            set { this._startTime = value; }
        }
        private String _startTime = "-";
        /// <summary>
        /// 读取或设置故障结束时间属性
        /// </summary>
        public String OverTime
        {
            get { return this._overTime; }
            set { this._overTime = value; }
        }
        private String _overTime = "-";

        /// <summary>
        /// 读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public String Time
        {
            get { return this._startTime + "\n" + this._overTime; }
        }

        /// <summary>
        /// 读取或设置提示信息属性
        /// </summary>
        public String PointOut { get; set; }

        public Boolean OldValue { get; set; }
    }
}
