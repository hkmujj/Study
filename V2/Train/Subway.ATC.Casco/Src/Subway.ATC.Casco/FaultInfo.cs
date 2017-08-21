namespace Subway.ATC.Casco
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
        public int Logic { get; set; }
        /// <summary>
        /// 读取或设置故障等级属性
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 读取或设置故障代码属性
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 读取或设置故障内容属性
        /// </summary>
        public string Display { get; set; }
        /// <summary>
        /// 读取或设置故障操作提示属性
        /// </summary>
        public string PointOut { get; set; }

        /// <summary>
        /// 读取或设置故障开始时间属性
        /// </summary>
        public string StartTime
        {
            get { return this._startTime; }
            set { this._startTime = value; }
        }
        private string _startTime = "-";
        /// <summary>
        /// 读取或设置故障结束时间属性
        /// </summary>
        public string OverTime
        {
            get { return this._overTime; }
            set { this._overTime = value; }
        }
        private string _overTime = "-";

        /// <summary>
        /// 读取故障的时间（包括开始时间与结束时间）
        /// </summary>
        public string Time
        {
            get { return this._startTime + "\n" + this._overTime; }
        }
    }
}
