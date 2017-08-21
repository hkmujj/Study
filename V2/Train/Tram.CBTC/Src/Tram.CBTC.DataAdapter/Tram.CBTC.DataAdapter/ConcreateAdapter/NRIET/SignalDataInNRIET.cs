using System;
using Tram.CBTC.DataAdapter.Model;

namespace Tram.CBTC.DataAdapter.ConcreateAdapter.NRIET
{
    [Serializable]
    public class SignalDataInNRIET : SignalDataIn
    {
        /// <summary>
        ///信标故障状态
        /// </summary>
        public int BaliseState { set; get; }
        /// <summary>
        /// GPS故障状态
        /// </summary>
        public int GPSState { set; get; }
        /// <summary>
        /// NTC故障状态
        /// </summary>
        public int NTCState { set; get; }
        /// <summary>
        ///雷达故障状态
        /// </summary>
        public int RadarState { set; get; }
        /// <summary>
        /// STA故障状态
        /// </summary>
        public int STAState { set; get; }
        /// <summary>
        /// TOD故障状态
        /// </summary>
        public int TODState { set; get; }
        /// 无线故障状态
        /// </summary>
        public int WirelessState { set; get; }
        /// <summary>
        /// 到下一站时间
        /// </summary>
        public float ArriveNextStationTimeSpan { set; get; }
        /// <summary>
        /// 下一目标
        /// </summary>
        public int NextTarget { set; get; }
        /// <summary>
        /// 上站到下站偏移量
        /// </summary>
        public int LastToNextStationOffSet { set; get; }
        /// <summary>
        /// 障碍物距离
        /// </summary>
        public float[] Obstacle { set; get; }

        /// <summary>
        /// 进出折返预告
        /// </summary>
        public int TurnBackNotice { set; get; }
        /// <summary>
        /// 出入库预告
        /// </summary>
        public int DepotNotice { set; get; }

        //内部数据
        public int StopStationTimeSec { set; get; }
        public int ArriveNextStationTimeSec { set; get; }

        public SignalDataInNRIET() :base()
        {
            Obstacle = new float[6];

        }

        public override void ClearInfo()
        {
            base.ClearInfo();
            Obstacle = new float[6];
        }

    }
}