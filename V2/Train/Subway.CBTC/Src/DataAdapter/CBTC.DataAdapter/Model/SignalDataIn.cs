using System;
using CBTC.Infrasturcture.Model.Constant;
using CommonUtil.Annotations;

namespace CBTC.DataAdapter.Model
{
    [Serializable]
    public class SignalDataIn
    {
        //bool--------------------------------------------------
        /// <summary>
        /// 确认按钮
        /// </summary>
        public bool TxtConfirmFlag { set; get; }
        /// <summary>
        /// 亮屏
        /// </summary>
        public bool PowerFlag { set; get; }

        /// <summary>
        /// 定位建立
        /// </summary>
        public bool LocationBuildFlag { set; get; }

        /// <summary>
        /// 列车完整性
        /// </summary>
        public bool TrainCompleteFlag { set; get; }

        /// <summary>
        /// 确认折返
        /// </summary>
        public bool TurnBackConfirmFlag { set; get; }

        /// <summary>
        /// 进入车辆段
        /// </summary>
        public bool DepotEnterFlag { set; get; }

        /// <summary>
        /// 所有门关闭状态
        /// </summary>
        public bool AllDoorCloseSign { set; get; }

        /// <summary>
        /// 屏蔽门全部关闭标志
        /// </summary>
        public bool AllPSDCloseSign { set; get; }

        /// <summary>
        /// 停车标范围内
        /// </summary>
        public bool BStopRight { set; get; }

        /// <summary>
        /// 允许自动折返
        /// </summary>
        public bool BPermitATB { set; get; }

        /// <summary>
        /// 允许人工折返
        /// </summary>
        public bool BPermitMTB { set; get; }

        /// <summary>
        /// 列车在站内
        /// </summary>
        public bool BAtStation { set; get; }

        /// <summary>
        /// 列车正在折返
        /// </summary>
        public bool BATBing { set; get; }

        /// <summary>
        /// 屏保
        /// </summary>
        public  bool ScreenProtection { set; get; }

        /// <summary>
        /// 警惕按键
        /// </summary>
        public bool AlertButton { set; get; }

        /// <summary>
        /// 模式闪烁标志
        /// </summary>
        public bool ModeFlashFlag { set; get; }
        /// <summary>
        /// 车载设备电源正常
        /// </summary>
        public bool DevicePower { set; get; }

        /// <summary>
        /// 电钥匙
        /// </summary>
        public bool KeySwitch { set; get; }

        /// <summary>
        /// ATC切除开关
        /// </summary>
        public bool ATCPass { set; get; }

        /// <summary>
        /// ATB模式选择确认
        /// </summary>
        public bool AtbButton { set; get; }

        /// <summary>
        /// 模式升按钮
        /// </summary>
        public bool ModeUp { set; get; }

        /// <summary>
        /// 模式降按钮
        /// </summary>
        public bool ModeDown { set; get; }

        /// <summary>
        /// 进站标志
        /// </summary>
        public bool StoppingStationFlag { set; get; }

        ///<summary>
        /// 低速释放
        /// </summary>
        public bool LowSpeedReleaseConfirm { set; get; }

        ///<summary>
        /// 制动测试状态
        /// </summary>
        public bool EBBrakeTestSwitch { set; get; }

        ///<summary>
        /// MMI黑屏文字显示
        /// </summary>
        public bool MMIBlackShowWord { set; get; }

        ///<summary>
        /// 红网状态
        /// </summary>
        public bool RedWirelineStatus { set; get; }

        ///<summary>
        /// 蓝网状态
        /// </summary>
        public bool BlueWirelineStatus { set; get; }

        ///<summary>
        /// 故障信息
        /// </summary>
        public bool[] Fault { set; get; }

        ///<summary>
        /// 消息文本
        /// </summary>
        public bool[] MessageInfo { set; get; }

        ///// <summary>
        ///// ATP电源
        ///// </summary>
        //public bool ATPPowerFlag { set; get; }

        ///// <summary>
        ///// ATP隔离
        ///// </summary>
        //public bool ATPBypassFlag { set; get; }



        //float--------------------------------------------------

        /// <summary>
        /// 目标距离
        /// </summary>
        public float GoalDistance { set; get; }

        /// <summary>
        /// 列车运行速度
        /// </summary>
        public float TrainSpeed { set; get; }

        /// <summary>
        /// 列车限制速度
        /// </summary>
        public float TrainLimitSpeed { set; get; }

        /// <summary>
        /// 列车推荐速度
        /// </summary>
        public float TrainRecommendSpeed { set; get; }

        /// <summary>
        /// 列车目标速度
        /// </summary>
        public float TrainGoalSpeed { set; get; }

        /// <summary>
        /// 列车报警速度
        /// </summary>
        public float TrainWarningSpeed { set; get; }

        /// <summary>
        /// 列车紧急速度
        /// </summary>
        public float TrainEmergSpeed { set; get; }

        /// <summary>
        /// 控制模式
        /// </summary>
        public int ControlMode { set; get; }

        /// <summary>
        /// 运行等级
        /// </summary>
        public int RunLevel { set; get; }

        /// <summary>
        /// 制动状态
        /// </summary>
        public int BrakeStatus { set; get; }

        /// <summary>
        /// CBTC连接状态
        /// </summary>
        public int CBTCStatus { set; get; }

        /// <summary>
        /// 下一站
        /// </summary>
        public int NextStationNo { set; get; }

        /// <summary>
        /// 终点站
        /// </summary>
        public int EndStationNo { set; get; }

        /// <summary>
        /// 工况
        /// </summary>
        public int WorkStatus { set; get; }

        /// <summary>
        /// 最高可用模式
        /// </summary>
        public int MaxPermitMode { set; get; }

        /// <summary>
        /// 车首OBCU
        /// </summary>
        public int OBCUHeadStatus { set; get; }

        /// <summary>
        /// 车尾OBCU
        /// </summary>
        public int OBCUTailStatus { set; get; }

        /// <summary>
        /// 进入折返区域
        /// </summary>
        public int TurnBackAreaStatus { set; get; }
  
        /// <summary>
        /// 左车门允许
        /// </summary>
        public int DoorLeftPermit { set; get; }

        /// <summary>
        /// 右车门允许
        /// </summary>
        public int DoorRightPermit { set; get; }

        /// <summary>
        /// 左侧屏蔽门允许
        /// </summary>
        public int PSDLeftPermit { set; get; }

        /// <summary>
        /// 右侧屏蔽门允许
        /// </summary>
        public int PSDRightPermit { set; get; }

        /// <summary>
        /// 左侧车门状态
        /// </summary>
        public int DoorLeftStatus { set; get; }

        /// <summary>
        /// 右侧车门状态
        /// </summary>
        public int DoorRightStatus { set; get; }

        /// <summary>
        /// 左侧屏蔽门状态
        /// </summary>
        public int PSDLeftStatus { set; get; }

        /// <summary>
        /// 右侧屏蔽门状态
        /// </summary>
        public int PSDRightStatus { set; get; }

        /// <summary>
        /// 停车状态
        /// </summary>
        public int DockedStatus { set; get; }

        /// <summary>
        /// 速度指示
        /// </summary>
        public int SpeedStatus { set; get; }

        /// <summary>
        /// 空转指示
        /// </summary>
        public int IdlingStatus { set; get; }

        /// <summary>
        /// 门控模式
        /// </summary>
        public int DoorOperationMode { set; get; }

        /// <summary>
        /// 子系统故障
        /// </summary>
        public int SubsystemFault { set; get; }

        /// <summary>
        /// 车站控车状态（跳停/扣车）
        /// </summary>
        public int StationControlTrainStatus { set; get; }

        /// <summary>
        /// 停站时间
        /// </summary>
        public float StationStopTime { set; get; }

        /// <summary>
        /// 发车状态
        /// </summary>
        public int DepartStatus { set; get; }

        /// <summary>
        /// 运行方向
        /// </summary>
        public int RunDirection { set; get; }

        /// <summary>
        /// 司机号字母
        /// </summary>
        public int DriverLetter { set; get; }

        /// <summary>
        /// 司机号
        /// </summary>
        public int DriverNum { set; get; }

        /// <summary>
        /// 车次号字母
        /// </summary>
        public int TrainLetter { set; get; }

        /// <summary>
        /// 车次号
        /// </summary>
        public int TrainNum { set; get; }

        /// <summary>
        /// 编组
        /// </summary>
        public int GroupNum { set; get; }

        /// <summary>
        /// 目的地号字母
        /// </summary>
        public int DestLetter { set; get; }

        /// <summary>
        /// 目的地号
        /// </summary>
        public int DestNum { set; get; }

        ///<summary>
        /// VOBC状态
        /// </summary>
        public int VOBCStatus { set; get; }

        /// <summary>
        /// ATO允许状态
        /// </summary>
        public int ATOPermitStatus { set; get; }

        /// <summary>
        /// ATP允许状态
        /// </summary>
        public int ATPPermitStatus { set; get; }

        /// <summary>
        /// IATO允许状态
        /// </summary>
        public int IATOPermitStatus { set; get; }

        /// <summary>
        /// IATP允许状态
        /// </summary>
        public int IATPPermitStatus { set; get; }

        /// <summary>
        /// 离站时间
        /// </summary>
        public int LeftTime { set; get; }

        /// <summary>
        /// 后备模式
        /// </summary>
        public int BMMode { set; get; }

        /// <summary>
        /// ATC请求状态
        /// </summary>
        public int ATCRequestStatus { set; get; }

        /// <summary>
        /// 列车停车点类型
        /// </summary>
        public int TrainLMAType { set; get; }

        /// <summary>
        /// 列车下一区段所在区域
        /// </summary>
        public int TrainNextAreaType { set; get; }

        /// <summary>
        /// 列车前一区段所在区域
        /// </summary>
        public int TrainPreAreaType { set; get; }

        /// <summary>
        /// 列车当前区段所在区域
        /// </summary>
        public int TrainCurAreaType { set; get; }

        /// <summary>
        /// 车载工作模式
        /// </summary>
        public int BrakeMode { set; get; }

        /// <summary>
        /// 机车信号码
        /// </summary>
        public int CabSignalCode { set; get; }

        /// <summary>
        /// 前方信号机状态
        /// </summary>
        public int FrontSignalStatus { set; get; }

        /// <summary>
        /// 列车长度
        /// </summary>
        public int TrainLength { set; get; }

        /// <summary>
        /// 服务号
        /// </summary>
        public int ServiceNum { set; get; }

        /// <summary>
        /// ATP当前限速
        /// </summary>
        public float ATPSpeedLimit { set; get; }

        /// <summary>
        /// 切除牵引速度
        /// </summary>
        public float CutOffPowerSpeed { set; get; }

        /// <summary>
        /// 当前站
        /// </summary>
        public int CurrentStation { set; get; }

        /// <summary>
        /// RM允许状态
        /// </summary>
        public int RMPermitStatus { set; get; }

        /// <summary>
        /// 移动授权
        /// </summary>
        public float MoveAllow { set; get; }

        ///<summary>
        /// 车辆实时运行工况
        /// </summary>
        public float RealTimeWorkStatus { set; get; }

        ///<summary>
        /// 模式开关
        /// </summary>
        public float ModeSwitch { set; get; }

        ///<summary>
        /// ATO开关
        /// </summary>
        public float ATORelationSwitch { set; get; }
        ///// <summary>
        ///// 紧急制动类型
        ///// </summary>
        //public int EBType { set; get; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime NowTime { set; get; }
      

        public SignalDataIn()
        {
            Fault = new bool[50];
            MessageInfo = new bool[40];
        }


        public virtual void ClearInfo()
        {
            
            Fault = new bool[50];
        }
    }
}