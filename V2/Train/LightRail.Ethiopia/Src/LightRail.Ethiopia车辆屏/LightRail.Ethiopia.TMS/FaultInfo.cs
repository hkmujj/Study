using System;

namespace LightRail.Ethiopia.TMS
{
    /// <summary>
    /// 功能描述：故障
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class FaultInfo
    {
        /// <summary>
        /// 读取或设置车辆编号属性
        /// </summary>
        public String Vehicle { get; set; }

        /// <summary>
        /// 读取或设置故障触发时间属性
        /// </summary>
        public String DateTime { get; set; }

        /// <summary>
        /// 读取或设置故障名称属性
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 读取或设置设备属性
        /// </summary>
        public String Device { get; set; }

        /// <summary>
        /// 读取或设置提示属性
        /// </summary>
        public String Advice { get; set; }

        /// <summary>
        /// 读取或设置位置属性
        /// </summary>
        public String Position { get; set; }

        /// <summary>
        /// 读取或设置是否在主界面确认属性
        /// </summary>
        public Boolean IsConfirm { get; set; }

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
            get { return _startTime; }
            set { _startTime = value; }
        }
        private String _startTime = "-";
        /// <summary>
        /// 读取或设置故障结束时间属性
        /// </summary>
        public String OverTime
        {
            get { return _overTime; }
            set { _overTime = value; }
        }
        private String _overTime = "-";

        /// <summary>
        /// 读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public String Time
        {
            get { return _startTime + "\n" + _overTime; }
        }
    }
}
