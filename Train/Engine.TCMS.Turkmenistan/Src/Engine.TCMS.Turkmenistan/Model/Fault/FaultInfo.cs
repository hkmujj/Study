using System;

namespace Engine.TCMS.Turkmenistan.Model.Fault
{
    public class FaultInfo
    {
        /// <summary>
        /// 逻辑号
        /// </summary>
        public int LogicNumber { get; set; }
        /// <summary>
        /// 故障类型
        /// </summary>
        public FaultType Type { get; set; }
        /// <summary>
        /// true  本车  false 他车
        /// </summary>
        public bool IsCurrentCar { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime HappenTime { get; set; }
        /// <summary>
        /// 恢复时间
        /// </summary>
        public DateTime ConfirmTime { get; set; }
    }

    public enum FaultType
    {
        
    }
}
