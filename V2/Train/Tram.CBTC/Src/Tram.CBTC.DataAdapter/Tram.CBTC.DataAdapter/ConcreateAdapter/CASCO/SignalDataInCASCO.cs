using System;
using Tram.CBTC.DataAdapter.Model;

namespace Tram.CBTC.DataAdapter.ConcreateAdapter.CASCO
{
    [Serializable]
    public class SignalDataInCASCO : SignalDataIn
    {
        /// <summary>
        /// 前方信号机距离
        /// </summary>
        public float FrontSignalDistance { set; get; }		 //

        /// <summary>
        /// 进路预选区终点距离
        /// </summary>
        public float RouteSelectEndDistance { set; get; }	 //

        /// <summary>
        /// 前车距离
        /// </summary>
        public float FrontTrainDistance { set; get; }		//

        /// <summary>
        /// 后车距离
        /// </summary>
        public float BackTrainDistance { set; get; }	//



        /// <summary>
        /// 折返信息
        /// </summary>
        public int TurnBackInfo { set; get; }				

        /// <summary>
        /// ATP保护控制
        /// </summary>
        public int ATPProtectCtrl { set; get; }				

        /// <summary>
        /// ATP启动状态
        /// </summary>
        public int ATPStartStatus { set; get; }				

        /// <summary>
        /// 信标状态
        /// </summary>
        public int BaliseStatus { set; get; }		

        /// <summary>
        /// 列车早晚点
        /// </summary>
        public int TrainSoonerOrLaters { set; get; } 

        /// <summary>
        /// 车载运行模式
        /// </summary>
        public int TrainRunMode { set; get; }           



        /// <summary>
        /// 列车号
        /// </summary>
        public float TrainNumber { set; get; }        

        /// <summary>
        /// 路径号
        /// </summary>
        public float RouteNumber { set; get; }    

        /// <summary>
        /// 线路号
        /// </summary>
        public float RunLineNumber { set; get; }       

        /// <summary>
        /// 单程号
        /// </summary>
        public float OneWayNumber { set; get; }        

        /// <summary>
        /// ATP状态
        /// </summary>
        public int ATPStatus { set; get; }

        /// <summary>
        /// ELS状态
        /// </summary>
        public int ELSStatus { set; get; }

        /// <summary>
        /// RR状态
        /// </summary>
        public int RRStatus { set; get; }

        /// <summary>
        /// CP状态
        /// </summary>
        public int CPStatus { set; get; }

        /// <summary>
        /// VOBC车载主机状态
        /// </summary>
        public int VOBCMasterStatus { set; get; }      

        /// <summary>
        /// 车载定位状态
        /// </summary>
        public int VOBCLocationStatus { set; get; }       

        /// <summary>
        /// 车载系统与调度中心通信状态
        /// </summary>
        public int VOBCToCTCStatus { set; get; }

        /// <summary>
        /// GPS工作状态
        /// </summary>
        public int GPSWorkStatus { set; get; }

        /// <summary>
        /// GPS状态
        /// </summary>
        public int	 GPSStatus { set; get; }						

        /// <summary>
        /// NTC状态
        /// </summary>
        public int NTCStatus { set; get; }							

        /// <summary>
        /// 雷达状态
        /// </summary>
        public int	RadarStatus { set; get; }		

        /// <summary>
        /// STA状态
        /// </summary>
        public int	STAStatus { set; get; }							

        /// <summary>
        /// TOD状态
        /// </summary>
        public int	TODStatus { set; get; }							

        /// <summary>
        /// 无线连接状态
        /// </summary>
        public int	 WireLineStatus { set; get; }						

        /// <summary>
        /// 到下一站时间
        /// </summary>
        public float	ToNextStationTime { set; get; }					

        /// <summary>
        /// 下一目标
        /// </summary>
        public int		NextGoal { set; get; }						

        /// <summary>
        /// 上站到下站偏移量
        /// </summary>
        public float	PreStationToNextStationOffSet { set; get; }	//

        /// <summary>
        /// 系统状态
        /// </summary>
        public float SystemStatus { set; get; }
        public SignalDataInCASCO()
        {
           
        }

        public override void ClearInfo()
        {
            base.ClearInfo();
        }
    }
}
