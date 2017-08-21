using System;

namespace Motor.ATP.DataAdapter.Model
{
    [Serializable]
    public class SignalDataIn
    {
        /// <summary>
        /// 定位建立
        /// </summary>
        public bool LocationBuildFlag { set; get; }

        /// <summary>
        /// 列车完整性
        /// </summary>
        public bool TrainCompleteFlag { set; get; }


        /// <summary>
        /// 司机室激活
        /// </summary>
        public bool KeySwitch { set; get; }


        /// <summary>
        /// 故障
        /// </summary>
        public bool[] Fault { set; get; }

        //********浮点型变量相关定义*******
        /// <summary>
        /// 制动预警时间
        /// </summary>
        public float BrakeWarningTime { set; get; }

        /// <summary>
        /// 目标速度
        /// </summary>
        public float GoalSpeed { set; get; }

        /// <summary>
        /// 目标距离
        /// </summary>
        public float GoalDistance { set; get; }


        /// <summary>
        /// 列车实际速度
        /// </summary>
        public float TrainSpeed { set; get; }

        /// <summary>
        /// 推荐速度
        /// </summary>
        public float RecommendSpeed { set; get; }

        /// <summary>
        /// 允许速度
        /// </summary>
        public float EmergencySpeed { set; get; }

        /// <summary>
        /// 下一站
        /// </summary>
        public float NextStation { set; get; }

        /// <summary>
        /// 终点站
        /// </summary>
        public float DestinationStation { set; get; }

        /// <summary>
        /// 停站时间
        /// </summary>
        public float StopeTime { set; get; }

        /// <summary>
        /// 离站时间
        /// </summary>
        public float LeftTime { set; get; }

        /// <summary>
        /// 当前模式
        /// </summary>
        public int ControlMode { set; get; }

        /// <summary>
        /// 当前等级
        /// </summary>
        public int RunLevel { set; get; }

        /// <summary>
        /// 当前运行区域:正线、侧线、车辆段。。。。。
        /// </summary>
        public int RunArea { set; get; }

        /// <summary>
        /// 制动状态
        /// </summary>
        public int BrakeStatus { set; get; }

        /// <summary>
        /// 制动模式
        /// </summary>
        public int BrakeMode { set; get; }

        /// <summary>
        /// 站台侧
        /// </summary>
        public int StationSide { set; get; }

        /// <summary>
        /// 制动测试状态
        /// </summary>
        public int BrakeTestStatus { set; get; }

        /// <summary>
        /// GSM状态
        /// </summary>
        public int GSMStatus { set; get; }

        /// <summary>
        /// RBC状态
        /// </summary>
        public int RBCStatus { set; get; }

        /// <summary>
        /// RBC连接方式
        /// </summary>
        public int RBCLinkType { set; get; }

        /// <summary>
        /// RBC允许调车
        /// </summary>
        public int RBCAllowSH { set; get; }

        /// <summary>
        /// 紧急消息
        /// </summary>
        public int EmergencyInfo { set; get; }

        /// <summary>
        /// 车站
        /// </summary>
        public int StationNo { set; get; }

        /// <summary>
        /// MRSP速度曲线
        /// </summary>
        public float[] SpeedZoneInfo { set; get; }

        /// <summary>
        /// 坡道信息
        /// </summary>
        public float[] RampInfo { set; get; }

        /// <summary>
        /// 预告信息
        /// </summary>
        public float[] YGInfo { set; get; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime NowTime { set; get; }
      

        public SignalDataIn()
        {
            //ScreenBotton = new bool[19];
            Fault = new bool[50];
            SpeedZoneInfo = new float[20];
            RampInfo = new float[20];
            YGInfo = new float[20];
            //OrderList = new int[3];
        }


        public virtual void ClearInfo()
        {
            //ScreenBotton = new bool[19];
            Fault = new bool[50];
            SpeedZoneInfo = new float[20];
            RampInfo = new float[20];
            YGInfo = new float[20];
            //OrderList = new int[3];
        }
    }
}