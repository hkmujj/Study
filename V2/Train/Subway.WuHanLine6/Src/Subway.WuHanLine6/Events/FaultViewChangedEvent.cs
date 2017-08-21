using Microsoft.Practices.Prism.Events;
using Subway.WuHanLine6.FaultInfos;

namespace Subway.WuHanLine6.Events
{
    /// <summary>
    /// 故障视图更改事件
    /// </summary>
    public class FaultViewChangedEvent : CompositePresentationEvent<FaultViewChangedArgs>
    {
    }

    /// <summary>
    /// 故障视图更改参数
    /// </summary>
    public class FaultViewChangedArgs
    {
        /// <summary>
        /// 默认显示的故障等级
        /// </summary>
        public FaultLevel Level { get; set; }

        /// <summary>
        /// 当前故障  今日故障  历史故障
        /// </summary>
        public FaultCurrent Current { get; set; }
    }
}