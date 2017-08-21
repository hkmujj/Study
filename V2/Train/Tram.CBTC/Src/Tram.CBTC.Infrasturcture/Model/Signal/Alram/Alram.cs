using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Signal.Alram
{
    /// <summary>
    /// 警报
    /// </summary>
    public class Alram
    {
        public Alram()
        {
            CurrentLimitSpeedAlram = new AlramModel<float>() { Text = "当前限速" };
            ForwardLimitSppedAlram = new AlramModel<float>() { Text = "前方限速" };
            SemaphoreAlram = new AlramModel<SemaphoreStaus>() { Text = "前方信号" };
            RoadRequestAlram = new AlramModel<float>();
        }
        /// <summary>
        /// 当前限速
        /// </summary>
        public AlramModel<float> CurrentLimitSpeedAlram { get; private set; }
        /// <summary>
        /// 前方限速
        /// </summary>
        public AlramModel<float> ForwardLimitSppedAlram { get; private set; }
        /// <summary>
        /// 信号机
        /// </summary>
        public AlramModel<SemaphoreStaus> SemaphoreAlram { get; private set; }
        /// <summary>
        /// 进路请求
        /// </summary>
        public AlramModel<float> RoadRequestAlram { get; private set; }

    }
}
