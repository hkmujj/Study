using System;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using CBTC.DataAdapter.Resource.Keys;
using CBTC.Infrasturcture.Events;
using CBTC.Infrasturcture.Extension;
using CBTC.DataAdapter.Util;
using CBTC.DataAdapter.Extension;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Train;
using CBTC.Infrasturcture.Model.Road;
namespace CBTC.DataAdapter.ConcreateAdapter.THALES
{
    public class THALESDataAdapter : DataAdapterBase
    {
        protected SignalDataInTHALES SignalDataInTHALES { get; set; }
        protected SignalDataInTHALES SignalDataInTHALESOld { get; set; }
        protected SignalDataOutTHALES SignalDataOutTHALES { get; set; }

        public THALESDataAdapter(Infrasturcture.Model.CBTC adaptTarget)
            : base(adaptTarget, new SendInterfaceTHALES(new SignalDataOutTHALES()))
        {
            SignalDataInTHALES = new SignalDataInTHALES();
            SignalDataInTHALESOld = new SignalDataInTHALES();
            SignalDataInTHALESOld = SignalDataInTHALES.Copy();
            SignalDataIn = SignalDataInTHALES;
            SignalDataInOld = SignalDataInTHALESOld;

            SignalDataOutTHALES = (SignalDataOutTHALES)SignalDataOut;
            SignalDataOutTHALES.SignalDataIn = SignalDataInTHALES;
            SignalDataOutTHALES.CBTC = CBTC;
            SignalDataOut = SignalDataOutTHALES;
        }

        public override void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.OnBoolChangedImp(sender, dataChangedArgs);
            //信号屏是否显示停站倒计时
            dataChangedArgs.UpdateIfContains(InbKeys.信号屏是否显示停站倒计时, b => SignalDataInTHALES.IsShowLeftTime = b);
            FeedBackInfo(sender, dataChangedArgs);
        }
        public override bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.FeedBackInfo(sender, dataChangedArgs);
            return true;
        }

        public override void OnFloatChangedImp(object sender, CommunicationDataChangedWrapperArgs<float> dataChangedArgs)
        {
            base.OnFloatChangedImp(sender, dataChangedArgs);

            //下一个车站打开车门的侧
            dataChangedArgs.UpdateIfContains(InfKeys.下一个车站打开车门的侧, f => SignalDataInTHALES.NextStationDoorSide = f);

            //方向手柄
            dataChangedArgs.UpdateIfContains(InfKeys.方向手柄, f => SignalDataInTHALES.MasterControlDirection = f);



        }


        public override void OnDataChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> changedBools,
            CommunicationDataChangedWrapperArgs<float> changedFloats)
        {
            //1、信号-屏适配
            BasicAndTrainInfo();
            ScreenDistanceInfo();
            ScreenSpeedInfo();
            //按钮使能
            ScreenBtnEnable();
            //E区文本
            ScreenTextInfo();
            //F区流程控制
            ScreenControl();

            //2、屏-信号适配
            ScreenToSignalInfo();

            UpdateEqpmtFault();

            //3、清除信息
            if ((!SignalDataInTHALESOld.PowerFlag && SignalDataInTHALES.PowerFlag))
            {
                ClearInfos();
            }
            SignalDataInTHALESOld = SignalDataInTHALES.Copy();
        }




        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();
            return true;
        }



        public override bool BasicAndTrainInfo()
        {
            //时间日期
            CBTC.Other.TimeVisible = true;
            CBTC.Other.DateVisible = true;
            CBTC.Other.DateTimeTitleVisible = true;
            CBTC.Other.NowInCBTC = SignalDataInTHALES.NowTime;
            //电源
            CBTC.TrainInfo.PowerState = SignalDataInTHALES.PowerFlag ? PowerState.Started : PowerState.Stopped;
            CBTC.TrainInfo.WorkState = (WorkState)SignalDataInTHALES.WorkStatus;

            //运行方向
            CBTC.RunningInfo.RunDirection =(RunDirection)SignalDataInTHALES.RunDirection /*RunDirection.Forward*/;
            //编组
            //CBTC.TrainInfo.TrainLength

            //制动
            CBTC.TrainInfo.BrakeInfo.SignalBrake = (BrakeType)SignalDataInTHALES.BrakeStatus;
            CBTC.TrainInfo.BrakeInfo.BrakeState = (BrakeState)SignalDataInTHALES.IdlingStatus;

            //司机室
            CBTC.TrainInfo.CarriageInfo.CurrentCab.State = (CabState)SignalDataInTHALES.OBCUHeadStatus;
            CBTC.TrainInfo.CarriageInfo.RemoteCab.State = (CabState)SignalDataInTHALES.OBCUTailStatus;
            CBTC.TrainInfo.CarriageInfo.VOBCState = (VOBCState)SignalDataInTHALES.VOBCStatus;
            CBTC.TrainInfo.CompleteState = SignalDataInTHALES.TrainCompleteFlag ? CompleteState.Completed : CompleteState.Lose;

            //车门
            CBTC.TrainInfo.DoorInfo.Bypassed = ((DOORPERMITSTATUS)SignalDataInTHALES.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS
                || (DOORPERMITSTATUS)SignalDataInTHALES.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS) ? true : false;

            CBTC.TrainInfo.DoorInfo.DoorControlMode = (DoorControlMode)SignalDataInTHALES.DoorOperationMode;
           
            //车门状态
            CBTC.TrainInfo.DoorInfo.LeftDoorState = (DoorState)SignalDataInTHALES.DoorLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightDoorState = (DoorState)SignalDataInTHALES.DoorRightStatus;
            
            //车门允许
            //当列车对准站台，列车门使能并打开
            if ((ParkingAlignmentState)SignalDataInTHALES.DockedStatus == ParkingAlignmentState.AlignmentInStation)
            {
                if ((DOORSTATUSFLAG)SignalDataInTHALES.DoorRightStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN ||
                    (DOORSTATUSFLAG)SignalDataInTHALES.DoorLeftStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN)
                {
                    if ((DOORSTATUSFLAG)SignalDataInTHALES.DoorRightStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN)
                    {
                        CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.RightAllow;
                    }
                    if ((DOORSTATUSFLAG)SignalDataInTHALES.DoorLeftStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN)
                    {
                        CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.LeftAllow;
                    }
                }
                else
                {
                    CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.NoAllow;
                }
            }//当列车离开站台或在车站没有停准，列车门使能并打开
            else if ((ParkingAlignmentState)SignalDataInTHALES.DockedStatus == ParkingAlignmentState.NotInStation
                || (ParkingAlignmentState)SignalDataInTHALES.DockedStatus == ParkingAlignmentState.NotAlignmentInStation)
            {
                if ((DOORSTATUSFLAG)SignalDataInTHALES.DoorRightStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN)
                {
                    CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.RightAllow;
                }
                if ((DOORSTATUSFLAG)SignalDataInTHALES.DoorLeftStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN)
                {
                    CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.LeftAllow;
                }

            }  //车门未打开时不送出车门使能
            else
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.NoAllow;
            }


            //站台门状态：在车站之间或车地通信故障（无线通信故障），站台门状态不可用
            if ((ParkingAlignmentState)SignalDataInTHALES.DockedStatus == ParkingAlignmentState.NotInStation
                || SignalDataInTHALES.Fault[0] )
            {
                CBTC.TrainInfo.DoorInfo.LeftPSDState = DoorState.Unkown;
                CBTC.TrainInfo.DoorInfo.RightPSDState = DoorState.Unkown;
            }
            else 
            {
                CBTC.TrainInfo.DoorInfo.LeftPSDState = (DoorState)SignalDataInTHALES.PSDLeftStatus;
                CBTC.TrainInfo.DoorInfo.RightPSDState = (DoorState)SignalDataInTHALES.PSDRightStatus;
            }

            //站台门使能：
            if ((ParkingAlignmentState)SignalDataInTHALES.DockedStatus == ParkingAlignmentState.AlignmentInStation)
            {
                if ((DOORSTATUSFLAG)SignalDataInTHALES.PSDRightStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN
                    || (DOORSTATUSFLAG)SignalDataInTHALES.PSDLeftStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN)
                {
                    if ((DOORSTATUSFLAG)SignalDataInTHALES.PSDRightStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN)
                    {
                        CBTC.TrainInfo.DoorInfo.StationAllowState = DoorAllowState.RightAllow;
                    }
                    if ((DOORSTATUSFLAG)SignalDataInTHALES.PSDLeftStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN)
                    {
                        CBTC.TrainInfo.DoorInfo.StationAllowState = DoorAllowState.LeftAllow;
                    }
                }
                else
                {
                    CBTC.TrainInfo.DoorInfo.StationAllowState = DoorAllowState.NoAllow;
                }
            }
            else 
            {
                CBTC.TrainInfo.DoorInfo.StationAllowState = DoorAllowState.NoAllow;
            }


            //OBCU重复待删除
            //CBTC.RunningInfo.DepartState
            CBTC.RunningInfo.OperatingGrade = (OperatingGrade)SignalDataInTHALES.RunLevel;

            CBTC.RunningInfo.TrainOperatingMode = (TrainOperatingMode)SignalDataInTHALES.ControlMode;
            CBTC.RunningInfo.TrainPosition = TrainPosition.Initalize;
            if (SignalDataInTHALES.DepotEnterFlag)
            {
                CBTC.RunningInfo.TrainPosition = TrainPosition.CarDepot;
            }
            //列车停站
            CBTC.RunningInfo.ParkingState = (ParkingStationState)SignalDataInTHALES.DockedStatus; //停车状态
            //CBTC.RunningInfo.ParkingState = ParkingStationState.Invalid;
            // CBTC.RunningInfo.ParkingAlignmentState = (ParkingAlignmentState)SignalDataInTHALES.DockedStatus; //停车状态

            CBTC.SignalInfo.ATPConnectState = (ATPConnectState)SignalDataInTHALES.CBTCStatus;
            CBTC.SignalInfo.JumpStop = (JumpStopDetainCar)SignalDataInTHALES.StationControlTrainStatus;

            CBTC.SignalInfo.Speed.IsZeroSpeed = ((SPEEDSTATUS)SignalDataInTHALES.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_ZERO) ? true : false;
            CBTC.SignalInfo.Speed.IsSpeeding = ((SPEEDSTATUS)SignalDataInTHALES.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_OVER) ? true : false;

            //线路
            CBTC.RoadInfo.StationInfo.NextStation = SignalDataInTHALES.NextStationNo > 0  //下一站
                ? GetStationName((ulong)SignalDataInTHALES.NextStationNo) : "";
            CBTC.RoadInfo.StationInfo.EndStation = SignalDataInTHALES.EndStationNo > 0   //终点站
                ? GetStationName((ulong)SignalDataInTHALES.EndStationNo) : "";
            CBTC.TrainInfo.DoorInfo.NextStationDoorOpenDirection = (NextStationDoorOpenDirection)SignalDataInTHALES.NextStationDoorSide; //下一个车站打开车门的侧

            //人工折返站台图标
            if (((DOCKEDSTATUS)SignalDataInTHALES.DockedStatus == DOCKEDSTATUS.DOCKEDSTATUS_ALIGNED && (SEGFLAG)SignalDataInTHALES.TrainCurAreaType == SEGFLAG.MTBPLATFORM)
                || (SEGFLAG)SignalDataInTHALES.TrainCurAreaType == SEGFLAG.TRANSITTRACK)
            {
                if (!SignalDataInTHALES.TurnBackConfirmFlag)
                {
                    CBTC.RoadInfo.ReturnState = ReturnState.ManuanlReturn;
                }
                else
                {
                    CBTC.RoadInfo.ReturnState = ReturnState.ManuanlReturnActived;
                }
            }

            //自动折返站台图标
            if (SignalDataInTHALES.DockedStatus == 1 && (SEGFLAG)SignalDataInTHALES.TrainCurAreaType == SEGFLAG.ATBPLATFORM
                && (CONTROL_LEVEL)SignalDataInTHALES.RunLevel == CONTROL_LEVEL.CONTROL_LEVEL_CTC)
            {
                if (!SignalDataInTHALES.TurnBackConfirmFlag)
                {
                    CBTC.RoadInfo.ReturnState = ReturnState.AutoReturn;
                }
                else
                {
                    CBTC.RoadInfo.ReturnState = ReturnState.AutoReturnActived;
                }
            }
            else
            {
                CBTC.RoadInfo.ReturnState = ReturnState.None;
            }
            // CBTC.RoadInfo.ReturnState = ReturnState.WillPullback;
            CBTC.RoadInfo.DriverID = "---";  //司机号
            CBTC.RoadInfo.DesID = "---"; //目的地号
            CBTC.RoadInfo.TrainID = "---"; //车次号
            CBTC.RoadInfo.TrainID = "10";
            CBTC.TrainInfo.VOBCNumber = 2;
            CBTC.TrainInfo.TrainLength = "2";

            CBTC.RunningInfo.ATCRequestState = ATCRequestState.None;
            //CBTC.RunningInfo.ATCRequestState = ATCRequestState.ATCRequestLeave;
            if ((SEGFLAG)SignalDataInTHALES.TrainCurAreaType == SEGFLAG.TRANSITTRACK
                && (SEGFLAG)SignalDataInTHALES.TrainNextAreaType == SEGFLAG.DEPOT) //车辆进入车辆段
            {
                CBTC.TrainInfo.DepotEntry = true;
            }
            else
            {
                CBTC.TrainInfo.DepotEntry = false;
            }
            
            CBTC.SignalInfo.WarningIntervention.ParkingPointDistance = SignalDataInTHALES.MoveAllow;  //停车点距离
            CBTC.SignalInfo.WarningIntervention.TargetDistance = SignalDataInTHALES.GoalDistance;
            //CBTC.SignalInfo.WarningIntervention.ParkingPointDistance = 20;
            CBTC.SignalInfo.WarningIntervention.TagetSpeed = SignalDataInTHALES.TrainGoalSpeed;
            CBTC.SignalInfo.Speed.TargetSpeed.Value = SignalDataInTHALES.TrainGoalSpeed;  //信号屏表盘黄标
            CBTC.SignalInfo.Speed.PermittedSpeed.Value = SignalDataInTHALES.TrainEmergSpeed; //信号屏表盘红标
            CBTC.SignalInfo.Speed.TargetSpeed.Visible = true;
            
            //IATP允许状态
            if (SignalDataInTHALES.IATPPermitStatus == 2)
            {
                if (SignalDataInTHALES.TrainSpeed >0.01)
                {
                    CBTC.RunningInfo.IATPAvailable = TrainOperatingModeStatus.AvailableForMoveTrain;
                }
                else
                {
                    CBTC.RunningInfo.IATPAvailable = TrainOperatingModeStatus.AvailableForStationaryTrain;
                }
            }
            else
            {
                CBTC.RunningInfo.IATPAvailable = TrainOperatingModeStatus.NotAvailable;
            }

            //ATO允许状态
            if (SignalDataInTHALES.ATOPermitStatus == 2)
            {
                if (SignalDataInTHALES.TrainSpeed > 0.01)
                {
                    CBTC.RunningInfo.ATOAvailable = TrainOperatingModeStatus.AvailableForMoveTrain;
                }
                else
                {
                    CBTC.RunningInfo.ATOAvailable = TrainOperatingModeStatus.AvailableForStationaryTrain;
                }
            }
            else
            {
                CBTC.RunningInfo.ATOAvailable = TrainOperatingModeStatus.NotAvailable;
            }

            //ATP允许状态
            if (SignalDataInTHALES.ATPPermitStatus == 2)
            {
                if (SignalDataInTHALES.TrainSpeed > 0.01)
                {
                    CBTC.RunningInfo.ATPAvailable = TrainOperatingModeStatus.AvailableForMoveTrain;
                }
                else
                {
                    CBTC.RunningInfo.ATPAvailable = TrainOperatingModeStatus.AvailableForStationaryTrain;
                }
            }
            else
            {
                CBTC.RunningInfo.ATPAvailable = TrainOperatingModeStatus.NotAvailable;
            }

            //停站时间控制
            if (SignalDataInTHALES.TrainSpeed < 0.01  &&　((SEGFLAG)SignalDataInTHALES.TrainCurAreaType == SEGFLAG.STATION ||
                (SEGFLAG)SignalDataInTHALES.TrainCurAreaType == SEGFLAG.TURNBACKTRACK))
            {
                CBTC.RoadInfo.StationInfo.PSD.StopStationTimeVisibility = true;//控制是否显示时间
                //正计时是负值，需进行值的转换
                if (SignalDataInTHALES.LeftTime<0)
                {
                    CBTC.RoadInfo.StationInfo.PSD.StopStationTime = -SignalDataInTHALES.LeftTime; //停站时间数值
                }
                else
                {
                    CBTC.RoadInfo.StationInfo.PSD.StopStationTime = SignalDataInTHALES.LeftTime; //停站时间数值
                }
                //控制正计时星光闪烁图标
                if (SignalDataInTHALES.LeftTime<-1)
                {
                    CBTC.RoadInfo.StationInfo.PSD.IsTimeOut = true;//是否超时，倒计时
                }
                else
                {
                    CBTC.RoadInfo.StationInfo.PSD.IsTimeOut = false;//是否超时，倒计时
                }

            }
            else
            {
                CBTC.RoadInfo.StationInfo.PSD.StopStationTimeVisibility = false;//控制是否显示时间

            }


            CBTC.RunningInfo.DepartState = (DepartState)SignalDataInTHALES.DepartStatus;  //发车状态
            CBTC.TrainInfo.DepotDriver = !(SignalDataInTHALES.LocationBuildFlag);  //列车定位丢失(THALES:位置)

            //信号屏小红点闪烁标志用
            if ((BREAKSTATUS)SignalDataInTHALES.BrakeStatus == BREAKSTATUS.RequestBrake) //制动预警
            {
                CBTC.SignalInfo.Speed.IsSpeeding = true;
            }
            else
            {
                CBTC.SignalInfo.Speed.IsSpeeding = false;
            }

            if ((RealTimeWokeStatus) SignalDataInTHALES.RealTimeWorkStatus == RealTimeWokeStatus.EB)  //紧急制动
            {
                CBTC.TrainInfo.BrakeInfo.EmergencyBrake = true;
            }
            else
            {
                CBTC.TrainInfo.BrakeInfo.EmergencyBrake = false;
            }

            CBTC.SignalInfo.IsCalibration = SignalDataInTHALES.LocationBuildFlag; //列车校准
            


            return false;
        }

        public override bool ScreenSpeedInfo()
        {
             base.ScreenSpeedInfo();
            return true;
        }

        public override bool ScreenBtnEnable()
        {
            return false;
        }

        public override bool ScreenTextInfo()
        {
            //红灯信号机
            if ((LMATYPE)SignalDataInTHALES.TrainLMAType == LMATYPE.LMATYPE_REDSIGNAL)
            {
                CBTC.Message.MessageFactory.CreateMessage(1);
            }
            //停车站台
            if ((LMATYPE)SignalDataInTHALES.TrainLMAType == LMATYPE.LMATYPE_PLATFORM)
            {
                CBTC.Message.MessageFactory.CreateMessage(2);
            }
            //车站入口
            if ((LMATYPE)SignalDataInTHALES.TrainLMAType == LMATYPE.LMATYPE_STATIONENTER)
            {
                CBTC.Message.MessageFactory.CreateMessage(3);
            }

            //ATS通信正常
            if ((CBTCCONNECTSTATUS)SignalDataInTHALES.CBTCStatus == CBTCCONNECTSTATUS.CBTCCONNECTSTATUS_CONNECTED)
            {
                CBTC.Message.MessageFactory.CreateMessage(4);
            }
            //向后运行激活
            if ((CONTROL_RUNMODE)SignalDataInTHALES.ControlMode == CONTROL_RUNMODE.RUN_MODE_RMR)
            {
                CBTC.Message.MessageFactory.CreateMessage(5);
            }

            return false;
        }

        public override bool ScreenControl()
        {
            return false;
        }

        public override bool ClearInfos()
        {
            ////清理文本

            CBTC.Other.ShowingTimeDifference = TimeSpan.Zero;
            CBTC.RoadInfo.DriverID = string.Empty;
            CBTC.RoadInfo.TrainID = string.Empty;
            CBTC.RoadInfo.DesID = string.Empty;
            CBTC.Message.MessageFactory.ClearMessages();


            //清除输入信息
            SignalDataInTHALES.ClearInfo();
            SignalDataOutTHALES.ClearInfo();
            return false;
        }

        public override bool UpdateEqpmtFault()
        {
            if (SignalDataInTHALES.Fault[0]/* && !SignalDataInTCTOld.Fault[1]*/)  //无线通信故障
            {
                CBTC.FaultInfo.RADRedFault = true;
            }
            else
            {
                CBTC.FaultInfo.RADRedFault = false;
            }

            if (SignalDataInTHALES.Fault[1] /*&& !SignalDataInTCTOld.Fault[2]*/)  //ATP故障
            {
                CBTC.FaultInfo.ATPYellowFault = true;
            }
            else
            {
                CBTC.FaultInfo.ATPYellowFault = false;
            }

            if (SignalDataInTHALES.Fault[2] /*&& !SignalDataInTCTOld.Fault[3]*/)  //ATO故障
            {
                CBTC.FaultInfo.ATORedFault = true;
            }
            else
            {
                CBTC.FaultInfo.ATORedFault = false;
            }

            if (SignalDataInTHALES.Fault[3]/* && !SignalDataInTCTOld.Fault[4]*/)  //AM不可用（ATO输出反馈异常）
            {
                CBTC.FaultInfo.AMRedFault = true;
            }
            else
            {
                CBTC.FaultInfo.AMRedFault = false;
            }

            if (SignalDataInTHALES.Fault[4]) //MMI通信中断
            {
                CBTC.TrainInfo.BlackText = BlackText.MMICommunicationFault;
            }
            else
            {
                CBTC.TrainInfo.BlackText = BlackText.None;
            }

            //模式开关不在OFF位
            if ((MODESWITCH)SignalDataInTHALES.ModeSwitch != MODESWITCH.MODESWITCH_OFF)
            {
                CBTC.Message.MessageFactory.CreateMessage(51);
            }

            //门模式转为人工
            if ((DoorControlMode)SignalDataInTHALES.DoorOperationMode == DoorControlMode.MOMC)
            {
                CBTC.Message.MessageFactory.CreateMessage(52);
            }
            //行车时车门未关闭
            if (SignalDataInTHALES.TrainSpeed > 0.01 && ((DOORSTATUSFLAG)SignalDataInTHALES.DoorRightStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN ||
                    (DOORSTATUSFLAG)SignalDataInTHALES.DoorLeftStatus == DOORSTATUSFLAG.DOORSTATUS_FLAG_OPEN))
            {
                CBTC.Message.MessageFactory.CreateMessage(53);
            }
            //模式不可用
            if ((SEGFLAG)SignalDataInTHALES.TrainCurAreaType == SEGFLAG.DEPOT && (MODESWITCH)SignalDataInTHALES.ModeSwitch == MODESWITCH.MODESWITCH_ATP
                && (MASTERDIRECTION)SignalDataInTHALES.MasterControlDirection != MASTERDIRECTION.MASTERDIRECTION_ZERO)
            {
                CBTC.Message.MessageFactory.CreateMessage(54);
            }

            //方向手柄不在零位
            if ((MASTERDIRECTION)SignalDataInTHALES.MasterControlDirection != MASTERDIRECTION.MASTERDIRECTION_ZERO)
            {
                CBTC.Message.MessageFactory.CreateMessage(55);
            }


            return false;
        }

        public override bool OtherPageShowControlInfo()
        {
            //屏保、电钥匙未打开、ATP切除、列车自动折返等显示界面
            //屏保
            if (SignalDataInTHALES.MMIBlackShowWord)
            {
                CBTC.TrainInfo.ScreenSaverEnable = true;
            }
            else
            {
                CBTC.TrainInfo.ScreenSaverEnable = false;
            }
            //屏保时间显示
            CBTC.TrainInfo.BlackTimeEnable = true;

            if (SignalDataInTHALES.ATCPass)  //ATP已切除
            {
                CBTC.TrainInfo.BlackText = BlackText.ATPAbscission;
            }

            if (SignalDataInTHALES.BATBing)  // 无人折返
            {
                CBTC.TrainInfo.BlackText = BlackText.UNManuanlReturn;
            }


            //CBTC.TrainInfo.ScreenSaverEnable = true;


            //INFO子界面相关显示控制信息
            //红网状态
            CBTC.TestInfo.RedStatus = NetStatus.NormalStatus;
            //蓝网状态
            CBTC.TestInfo.BlueStatus = NetStatus.ErrorStatus;
            //广播测试
            CBTC.TestInfo.BroadcastTestStatus = BroadcastTestStatus.StatusUnKnown;


            if (SignalDataInTHALES.EBBrakeTestSwitch)  //试闸测试
            {
                CBTC.TestInfo.EmergencyBrakeStatus = EmergencyBrakeStatus.HasEB;
            }
            else
            {
                CBTC.TestInfo.EmergencyBrakeStatus = EmergencyBrakeStatus.None;
            }
            return false;
        }
    }
}