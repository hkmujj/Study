using System;

namespace Motor.ATP.DataAdapter.Model
{
    [Serializable]
    public class SignalDataIn
    {
        /// <summary>
        /// 按钮
        /// </summary>
        public bool[] ScreenBotton { set; get; }

        /// <summary>
        /// 定位建立
        /// </summary>
        public bool LocationBuildFlag { set; get; }

        /// <summary>
        /// 列车完整性
        /// </summary>
        public bool TrainCompleteFlag { set; get; }

        /// <summary>
        /// 留意防护
        /// </summary>
        public bool SlideProtectFlag { set; get; }

        /// <summary>
        /// 启动条件
        /// </summary>
        public bool StartConditionFlag { set; get; }

        /// <summary>
        /// 启动完成
        /// </summary>
        public bool StartFinishFlag { set; get; }

        /// <summary>
        /// 越行提示
        /// </summary>
        public bool ExceTipsFlag { set; get; }

        /// <summary>
        /// 冒进提示
        /// </summary>
        public bool MJTipsFlag { set; get; }

        /// <summary>
        /// 禁止调车
        /// </summary>
        public bool SHForbidFlag { set; get; }

        /// <summary>
        /// 司机室激活
        /// </summary>
        public bool DriverActFlag { set; get; }

        /// <summary>
        /// ATP电源
        /// </summary>
        public bool ATPPowerFlag { set; get; }

        /// <summary>
        /// ATP隔离
        /// </summary>
        public bool ATPBypassFlag { set; get; }

        /// <summary>
        /// 公里标是否显示
        /// </summary>
        public bool IsGLBVisible { set; get; }

        /// <summary>
        /// 屏启动完成标志
        /// </summary>
        public bool ScreenStartFinishFlag { set; get; }

        /// <summary>
        /// 绝对停车标志
        /// </summary>
        public bool AbsoluteStopFlag { set; get; }

        /// <summary>
        /// 允许缓解
        /// </summary>
        public bool BrakeReleaseAck { set; get; }

        /// <summary>
        /// 警惕确认
        /// </summary>
        public bool AlertAck { set; get; }

        /// <summary>
        /// 冒进确认
        /// </summary>
        public bool MJAck { set; get; }


        /// <summary>
        /// 越行确认
        /// </summary>
        public bool ExceAck { set; get; }

        /// <summary>
        /// 引导确认
        /// </summary>
        public bool SRAck { set; get; }

        /// <summary>
        /// 进入C2等级确认
        /// </summary>
        public bool EnterC2Ack { set; get; }

        /// <summary>
        /// 进路C3等级确认
        /// </summary>
        public bool EnterC3Ack { set; get; }

        /// <summary>
        /// 留意防护确认
        /// </summary>
        public bool SlideProtectAck { set; get; }


        /// <summary>
        /// 制动测试确认
        /// </summary>
        public bool BrakeTestAck { set; get; }

        /// <summary>
        /// 故障
        /// </summary>
        public bool[] Fault { set; get; }

        /// <summary>
        /// 制动预警时间
        /// </summary>
        public float BrakeWarningTime { set; get; }

        /// <summary>
        /// 目标速度
        /// </summary>
        public float GoalDistance { set; get; }

        /// <summary>
        /// 列车公里标
        /// </summary>
        public float TrainGLB { set; get; }

        /// <summary>
        /// 列车速度
        /// </summary>
        public float TrainSpeed { set; get; }

        /// <summary>
        /// 列车当前限速
        /// </summary>
        public float TrainLimitSpeed { set; get; }

        /// <summary>
        /// 列车目标速度
        /// </summary>
        public float TrainGoalSpeed { set; get; }

        /// <summary>
        /// 常用制动速度
        /// </summary>
        public float SBISpeed { set; get; }

        /// <summary>
        /// 紧急制动速度
        /// </summary>
        public float EBISpeed { set; get; }

        /// <summary>
        /// 开口速度
        /// </summary>
        public float TrainOpeningSpeed { set; get; }

        /// <summary>
        /// 干预速度
        /// </summary>
        public float IntervertionSpeed { set; get; }

        /// <summary>
        /// 报警速度
        /// </summary>
        public float WarningSpeed { set; get; }

        /// <summary>
        /// 当前模式
        /// </summary>
        public int ControlMode { set; get; }

        /// <summary>
        /// 命令列表
        /// </summary>
        public int[] OrderList;

        /// <summary>
        /// 当前等级
        /// </summary>
        public int RunLevel { set; get; }

        /// <summary>
        /// 预告信息
        /// </summary>
        public int YGLevelInfo { set; get; }

        /// <summary>
        /// 车载信号码
        /// </summary>
        public int SignalCode { set; get; }

        /// <summary>
        /// 当前载频
        /// </summary>
        public int Frequency { set; get; }

        /// <summary>
        /// 起模点位置
        /// </summary>
        public float QMPos { set; get; }

        /// <summary>
        /// 起模点结束位置
        /// </summary>
        public float QMEndPos { set; get; }

        /// <summary>
        /// 当前运行区域
        /// </summary>
        public int QMArea { set; get; }

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
            ScreenBotton = new bool[19];
            Fault = new bool[50];
            SpeedZoneInfo = new float[20];
            RampInfo = new float[20];
            YGInfo = new float[20];
            OrderList = new int[3];
        }


        public virtual void ClearInfo()
        {
            ScreenBotton = new bool[19];
            Fault = new bool[50];
            SpeedZoneInfo = new float[20];
            RampInfo = new float[20];
            YGInfo = new float[20];
            OrderList = new int[3];
        }
    }
}