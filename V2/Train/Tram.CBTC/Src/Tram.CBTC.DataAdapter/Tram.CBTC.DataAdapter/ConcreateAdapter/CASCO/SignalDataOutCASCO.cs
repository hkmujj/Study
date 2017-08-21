using Tram.CBTC.DataAdapter.Model;

namespace Tram.CBTC.DataAdapter.ConcreateAdapter.CASCO
{
    public class SignalDataOutCASCO : SignalDataOut
    {
        /// <summary>
        /// 强制BM确认
        /// </summary>
        public bool BMACK_Confirm { set; get; }

        /// <summary>
        /// 非强制BM确认
        /// </summary>
        public bool BMUNACK_Confirm { set; get; }

        /// <summary>
        /// 声音测试
        /// </summary>
        public bool ScreenVolumeTest { set; get; }
        /// <summary>
        /// 站场图网格显示
        /// </summary>
        public bool StationDiagramShow { set; get; }

        ///<summary>
        /// RADAR声音开关
        /// </summary>
        public bool RadarVolumeSwitch { set; get; }

        ///<summary>
        /// 系统声音开关
        /// </summary>
        public bool SystemVolumeSwitch { set; get; }

        ///<summary>
        /// 车载运行模式
        /// </summary>
        public int TrainRunMode { set; get; }

        /// <summary>
        /// 终点站
        /// </summary>
        public int EndStationNo { set; get; }

        /// <summary>
        /// 线路号
        /// </summary>
        public float RunLineNumber { set; get; }

        ///<summary>
        /// 计划号
        /// </summary>
        public float TrainOperationPlanNumber { set; get; }

        /// <summary>
        /// 列车号
        /// </summary>
        public float TrainNumber { set; get; }     

        /// <summary>
        /// 单程号
        /// </summary>
        public float OneWayNumber { set; get; }



        public SignalDataOutCASCO() : base()
        {

        }

        public override void ClearInfo()
        {

        }
    }
}