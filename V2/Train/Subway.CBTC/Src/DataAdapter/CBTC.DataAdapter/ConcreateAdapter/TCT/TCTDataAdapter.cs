﻿using System;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using CBTC.DataAdapter.Resource.Keys;
using CBTC.Infrasturcture.Events;
using CBTC.Infrasturcture.Extension;
using CBTC.DataAdapter.Util;
using CBTC.DataAdapter.Extension;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Fault;
using CBTC.Infrasturcture.Model.Train;
using CBTC.Infrasturcture.Model.Road;
namespace CBTC.DataAdapter.ConcreateAdapter.TCT
{
    public class TCTDataAdapter : DataAdapterBase
    {
        protected SignalDataInTCT SignalDataInTCT { get; set; }
        protected SignalDataInTCT SignalDataInTCTOld { get; set; }
        protected SignalDataOutTCT SignalDataOutTCT { get; set; }

        public TCTDataAdapter(Infrasturcture.Model.CBTC adaptTarget)
            : base(adaptTarget, new SendInterfaceTCT(new SignalDataOutTCT()))
        {
            SignalDataInTCT = new SignalDataInTCT();
            SignalDataInTCTOld = new SignalDataInTCT();
            SignalDataInTCTOld = SignalDataInTCT.Copy();
            SignalDataIn = SignalDataInTCT;
            SignalDataInOld = SignalDataInTCTOld;

            SignalDataOutTCT = (SignalDataOutTCT)SignalDataOut;
            SignalDataOutTCT.SignalDataIn = SignalDataInTCT;
            SignalDataOutTCT.CBTC = CBTC;
            SignalDataOut = SignalDataOutTCT;
        }

        public override void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.OnBoolChangedImp(sender, dataChangedArgs);

            //模式升按钮
            dataChangedArgs.UpdateIfContains(InbKeys.模式升按钮, b => SignalDataIn.ModeUp = b);

            //模式降按钮
            dataChangedArgs.UpdateIfContains(InbKeys.模式降按钮, b => SignalDataIn.ModeDown = b);
            //低速释放
            dataChangedArgs.UpdateIfContains(InbKeys.低速释放, b => SignalDataIn.LowSpeedReleaseConfirm = b);

            //制动测试状态
            dataChangedArgs.UpdateIfContains(InbKeys.制动测试状态, b => SignalDataIn.EBBrakeTestSwitch = b);

            //MMI黑屏文字显示
            dataChangedArgs.UpdateIfContains(InbKeys.MMI黑屏文字显示, b => SignalDataIn.MMIBlackShowWord = b);
            //红网状态
            dataChangedArgs.UpdateIfContains(InbKeys.红网状态, b => SignalDataIn.RedWirelineStatus = b);

            //蓝网状态
            dataChangedArgs.UpdateIfContains(InbKeys.蓝网状态, b => SignalDataIn.BlueWirelineStatus = b);

            //反馈_制动测试试闸
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_制动测试试闸, b => SignalDataInTCT.FeedBackBrakeTestSwitch = b);

            //反馈_制动测试缓解
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_制动测试缓解, b => SignalDataInTCT.FeedBackBrakeTestRelease = b);

            //反馈_广播测试
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_广播测试, b => SignalDataInTCT.FeedBackBroadcastTest = b);

            //反馈_灯测试
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_灯测试, b => SignalDataInTCT.FeedBackLampTest = b);

            FeedBackInfo(sender, dataChangedArgs);
        }
        public override bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            //反馈_制动测试试闸
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_制动测试试闸, b =>
            {
                if (b)
                {
                    SignalDataOutTCT.BrakeTestSwitch = false;
                }
            });

            //反馈_制动测试缓解
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_制动测试缓解, b =>
            {
                if (b)
                {
                    SignalDataOutTCT.BrakeTestRelease = false;
                }
            });

            //反馈_广播测试
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_广播测试, b =>
            {
                if (b)
                {
                    SignalDataOutTCT.BroadcastTest = false;
                }
            });

            //反馈_灯测试
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_灯测试, b =>
            {
                if (b)
                {
                    SignalDataOutTCT.LampTest = false;
                }
            });
            base.FeedBackInfo(sender, dataChangedArgs);
            return true;
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

            //3、清除信息
            if ((!SignalDataInTCTOld.PowerFlag && SignalDataInTCT.PowerFlag))
            {
                ClearInfos();
            }
            SignalDataInTCTOld = SignalDataInTCT.Copy();

            //设备故障状态更新
            UpdateEqpmtFault();

            //其他页面显示与控制信息
            OtherPageShowControlInfo();
        }




        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OuBKeys.制动测试试闸],
               SignalDataOutTCT.BrakeTestSwitch,true);

            if (SignalDataOutTCT.BrakeTestSwitch)
            {
                int b = 1;
            }

            DataService.WriteService.ChangeFloat(20, 123);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OuBKeys.制动测试缓解],
                SignalDataOutTCT.BrakeTestRelease,true);
            if (SignalDataOutTCT.BrakeTestRelease)
            {
                int a = 1;
            }

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OuBKeys.灯测试],
                SignalDataOutTCT.LampTest);

            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OuBKeys.广播测试],
                SignalDataOutTCT.BroadcastTest);

            return true;
        }



        public override bool BasicAndTrainInfo()
        {
            //时间日期
            CBTC.Other.TimeVisible = true;
            CBTC.Other.DateVisible = true;
            CBTC.Other.DateTimeTitleVisible = true;
            CBTC.Other.NowInCBTC = SignalDataInTCT.NowTime;
            //电源
            CBTC.TrainInfo.PowerState = SignalDataInTCT.PowerFlag ? PowerState.Started : PowerState.Stopped;
            CBTC.TrainInfo.WorkState = (WorkState)SignalDataInTCT.WorkStatus;
            if ((RealTimeWORKSTATUS)SignalDataInTCT.RealTimeWorkStatus == RealTimeWORKSTATUS.WORKSTATUS_EB)
            {

            }

            //编组
            //CBTC.TrainInfo.TrainLength

            //制动
            CBTC.TrainInfo.BrakeInfo.SignalBrake = (BrakeType)SignalDataInTCT.BrakeStatus;
            CBTC.TrainInfo.BrakeInfo.BrakeState = (BrakeState)SignalDataInTCT.IdlingStatus;

            //司机室
            CBTC.TrainInfo.CarriageInfo.CurrentCab.State = (CabState)SignalDataInTCT.OBCUHeadStatus;
            CBTC.TrainInfo.CarriageInfo.RemoteCab.State = (CabState)SignalDataInTCT.OBCUTailStatus;
            CBTC.TrainInfo.CarriageInfo.VOBCState = (VOBCState) SignalDataInTCT.VOBCStatus;
            CBTC.TrainInfo.CompleteState = SignalDataInTCT.TrainCompleteFlag ? CompleteState.Completed : CompleteState.Lose;

            //车门允许
            CBTC.TrainInfo.DoorInfo.Bypassed = ((DOORPERMITSTATUS)SignalDataInTCT.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS
                || (DOORPERMITSTATUS)SignalDataInTCT.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS) ? true : false;

            CBTC.TrainInfo.DoorInfo.DoorControlMode = (DoorControlMode)SignalDataInTCT.DoorOperationMode;

            if ((DOORPERMITSTATUS)SignalDataInTCT.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInTCT.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.AllowBoth;
            }
            else if ((DOORPERMITSTATUS)SignalDataInTCT.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInTCT.DoorRightPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.LeftAllow;
            }
            else if ((DOORPERMITSTATUS)SignalDataInTCT.DoorLeftPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
               && (DOORPERMITSTATUS)SignalDataInTCT.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.RightAllow;
            }
            else if ((DOORPERMITSTATUS)SignalDataInTCT.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMITFIRST
                && (DOORPERMITSTATUS)SignalDataInTCT.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMITSECOND)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.FirstLeft;
            }
            else if ((DOORPERMITSTATUS)SignalDataInTCT.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMITSECOND
               && (DOORPERMITSTATUS)SignalDataInTCT.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMITFIRST)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.FirstRight;
            }
            else
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.NoAllow;
            }
            //左侧车门状态
            CBTC.TrainInfo.DoorInfo.LeftDoorState = (DoorState)SignalDataInTCT.DoorLeftStatus;
            //右侧车门状态
            CBTC.TrainInfo.DoorInfo.RightDoorState = (DoorState)SignalDataInTCT.DoorRightStatus;
            //左侧屏蔽门状态
            CBTC.TrainInfo.DoorInfo.LeftPSDState = (DoorState)SignalDataInTCT.PSDLeftStatus;
            //右侧屏蔽门状态
            CBTC.TrainInfo.DoorInfo.RightPSDState = (DoorState)SignalDataInTCT.PSDRightStatus; 
            //OBCU重复待删除
            //CBTC.RunningInfo.DepartState
            //运行等级
            CBTC.RunningInfo.OperatingGrade = (OperatingGrade)SignalDataInTCT.RunLevel;  
            //控制模式
            if ((TrainOperatingMode)SignalDataInTCT.ControlMode == TrainOperatingMode.RMR)
            {
                CBTC.RunningInfo.TrainOperatingMode = TrainOperatingMode.RM;
            }
            else
            {
                CBTC.RunningInfo.TrainOperatingMode = (TrainOperatingMode)SignalDataInTCT.ControlMode;
            }
             
            CBTC.RunningInfo.TrainPosition = TrainPosition.Initalize;
            //进入车辆段
            if (SignalDataInTCT.DepotEnterFlag)  
            {
                CBTC.RunningInfo.TrainPosition = TrainPosition.CarDepot;
            }
            //CBTC.RunningInfo.ParkingState = (ParkingStationState)SignalDataInTCT.DockedStatus;
            //停车状态
            CBTC.RunningInfo.ParkingAlignmentState = (ParkingAlignmentState)SignalDataInTCT.DockedStatus; 
            //CBTC连接状态
            CBTC.SignalInfo.ATPConnectState = (ATPConnectState)SignalDataInTCT.CBTCStatus; 
            //车站控车状态（跳停/扣车）
            CBTC.SignalInfo.JumpStop = (JumpStopDetainCar)SignalDataInTCT.StationControlTrainStatus; 
            //CBTC.SignalInfo.CabSignalCode ;
            if (SignalDataInTCT.MaxPermitMode >5) //最高运行模式转换
            {
                CBTC.SignalInfo.HighDirveModelFlick = true;
                CBTC.SignalInfo.HighDirveModel = (HighDirveModel)(SignalDataInTCT.MaxPermitMode-5);

            }
            else
            {
                CBTC.SignalInfo.HighDirveModelFlick = false;
                CBTC.SignalInfo.HighDirveModel = (HighDirveModel)SignalDataInTCT.MaxPermitMode;
            }
            

            CBTC.SignalInfo.Speed.IsZeroSpeed = ((SPEEDSTATUS)SignalDataInTCT.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_ZERO) ? true : false;
            CBTC.SignalInfo.Speed.IsSpeeding = ((SPEEDSTATUS)SignalDataInTCT.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_OVER) ? true : false;

            //线路
            CBTC.RoadInfo.StationInfo.NextStation = SignalDataInTCT.NextStationNo > 0
                ? GetStationName((ulong)SignalDataInTCT.NextStationNo) : "";
            CBTC.RoadInfo.StationInfo.EndStation = SignalDataInTCT.EndStationNo > 0
                ? GetStationName((ulong)SignalDataInTCT.EndStationNo) : "";

            //人工折返站台图标
            if (((DOCKEDSTATUS)SignalDataInTCT.DockedStatus == DOCKEDSTATUS.DOCKEDSTATUS_ALIGNED && (SEGFLAG)SignalDataInTCT.TrainCurAreaType == SEGFLAG.MTBPLATFORM)
                || (SEGFLAG)SignalDataInTCT.TrainCurAreaType == SEGFLAG.TRANSITTRACK)
            {
                if (!SignalDataInTCT.TurnBackConfirmFlag)
                {
                    CBTC.RoadInfo.ReturnState = ReturnState.ManuanlReturn;
                }
                else
                {
                    CBTC.RoadInfo.ReturnState = ReturnState.ManuanlReturnActived;
                }
            }

            //自动折返站台图标
            if (SignalDataInTCT.DockedStatus == 1 && (SEGFLAG)SignalDataInTCT.TrainCurAreaType == SEGFLAG.ATBPLATFORM
                && (CONTROL_LEVEL)SignalDataInTCT.RunLevel == CONTROL_LEVEL.CONTROL_LEVEL_CTC)
            {
                if (!SignalDataInTCT.TurnBackConfirmFlag)
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
            CBTC.RoadInfo.DriverID = "T000011";  //司机号
            CBTC.RoadInfo.DesID = "T51230"; //目的地号
            CBTC.RoadInfo.TrainID = "D1122"; //车次号

            //CBTC.FaultInfo.FaultLocation = FaultLocation.None;
            //CBTC.FaultInfo.RADRedFault = true;
            //发车状态(ATC请求状态)
            if (SignalDataInTCT.LeftTime < 0.01 &&SignalDataInTCT.AllDoorCloseSign && SignalDataInTCT.TrainSpeed <0.01)  //ATC请求列车离站 
            {
                CBTC.RunningInfo.ATCRequestState = ATCRequestState.ATCRequestLeave;;
            }
            else if((ATCREQUESTSTATUS)SignalDataInTCT.ATCRequestStatus == ATCREQUESTSTATUS.ATCREQUESTSTATUS_CLOSEDOOR)  //请求车门关闭
            {
                CBTC.RunningInfo.ATCRequestState = ATCRequestState.CloseDoorOrder;
            }
            else if ((ATCREQUESTSTATUS) SignalDataInTCT.ATCRequestStatus == ATCREQUESTSTATUS.ATCREQUESTSTATUS_CLOSEDOORORDER)
            {

            }
            else
            {
                CBTC.RunningInfo.ATCRequestState = ATCRequestState.None;
            }
            // CBTC.RunningInfo.ATCRequestState = ATCRequestState.None;
            //CBTC.RunningInfo.ATCRequestState = ATCRequestState.ATCRequestLeave;
            //车辆进入车辆段
            if ((SEGFLAG)SignalDataInTCT.TrainCurAreaType == SEGFLAG.TRANSITTRACK 
                && (SEGFLAG)SignalDataInTCT.TrainNextAreaType == SEGFLAG.DEPOT) 
            {
                CBTC.TrainInfo.DepotEntry = true;
            }
            else
            {
                CBTC.TrainInfo.DepotEntry = false;
            }
            //列车定位丢失
            CBTC.TrainInfo.DepotDriver = !(SignalDataInTCT.LocationBuildFlag);
            //低速释放
            CBTC.TrainInfo.RelesaseSpeed = SignalDataInTCT.LowSpeedReleaseConfirm;
            //车辆紧急工况
            CBTC.TrainInfo.RealTimeWokeStatus = (RealTimeWokeStatus)SignalDataInTCT.RealTimeWorkStatus; 

            //信号屏左上角制动状态
            if ((BREAKSTATUS)SignalDataInTCT.BrakeStatus == BREAKSTATUS.RequestBrake) //制动预警
            {
                CBTC.SignalInfo.Speed.BrakeDetailsType = BrakeDetailsType.OverSpeed;  
            }
            else if ((BREAKSTATUS)SignalDataInTCT.BrakeStatus == BREAKSTATUS.EmergencyBrake) //紧急制动
            {
                CBTC.SignalInfo.Speed.BrakeDetailsType = BrakeDetailsType.EnmergencyBrake;      
            }
            else
            {
                CBTC.SignalInfo.Speed.BrakeDetailsType = BrakeDetailsType.Normal;
            }
            

            // CBTC.TrainInfo.
            return false;
        }

        public override bool ScreenSpeedInfo()
        {
            base.ScreenSpeedInfo();

            if ((TrainOperatingMode)SignalDataIn.ControlMode == TrainOperatingMode.AM)
            {
                if (SignalDataIn.TrainSpeed <= SignalDataIn.TrainEmergSpeed)
                {
                    CBTC.SignalInfo.Speed.CurrentSpeed.SpeedColor = CBTCColor.Gray;
                }
            }
            else
            {
                if (SignalDataIn.TrainSpeed <= SignalDataIn.TrainEmergSpeed && SignalDataIn.TrainSpeed > SignalDataIn.TrainRecommendSpeed)
                {
                    CBTC.SignalInfo.Speed.CurrentSpeed.SpeedColor = CBTCColor.Yellow;
                }
                else if (SignalDataIn.TrainSpeed < SignalDataIn.TrainRecommendSpeed)
                {
                    CBTC.SignalInfo.Speed.CurrentSpeed.SpeedColor = CBTCColor.Gray;
                }
            }
            //AM模式下不显示推荐速度
            if ((TrainOperatingMode)SignalDataIn.ControlMode == TrainOperatingMode.AM)
            {
                CBTC.SignalInfo.Speed.PermittedSpeed.Visible = false;
            }
            else
            {
                CBTC.SignalInfo.Speed.PermittedSpeed.Visible = true;
                CBTC.SignalInfo.Speed.PermittedSpeed.Value = SignalDataIn.TrainRecommendSpeed;
                CBTC.SignalInfo.Speed.PermittedSpeed.SpeedColor = CBTCColor.Yellow;
            }
            return true;
        }

        public override bool ScreenBtnEnable()
        {
            return false;
        }

        public override bool ScreenTextInfo()
        {
            if (SignalDataInTCT.MaxPermitMode == 6) //驾驶模式选择确认：RM
            {
                CBTC.Message.MessageFactory.CreateMessage(7);
            }
            else
            {
                CBTC.Message.MessageFactory.RemoveMessage(7);
            }

            if (SignalDataInTCT.MaxPermitMode == 7) //驾驶模式选择确认：ITC_CM
            {
                CBTC.Message.MessageFactory.CreateMessage(8);
            }
            else
            {
                CBTC.Message.MessageFactory.RemoveMessage(8);
            }

            if (SignalDataInTCT.MaxPermitMode == 9) //驾驶模式选择确认：ITC_AM
            {
                CBTC.Message.MessageFactory.CreateMessage(9);
            }
            else
            {
                CBTC.Message.MessageFactory.RemoveMessage(9);
            }

            if (SignalDataInTCT.MaxPermitMode == 8) //驾驶模式选择确认：CBTC-CM
            {
                CBTC.Message.MessageFactory.CreateMessage(10);
            }
            else
            {
                CBTC.Message.MessageFactory.RemoveMessage(10);
            }

            if (SignalDataInTCT.MaxPermitMode == 10) //驾驶模式选择确认：CBTC-AM
            {
                CBTC.Message.MessageFactory.CreateMessage(11);
            }
            else
            {
                CBTC.Message.MessageFactory.RemoveMessage(11);
            }

            //要求进入RM模式:列车当前区段在转换轨，下一区段入库，当列车运行模式不在RM模式时提示“要求进入RM模式”
            if ((SEGFLAG)SignalDataInTCT.TrainCurAreaType == SEGFLAG.TRANSITTRACK && (SEGFLAG)SignalDataInTCT.TrainNextAreaType == SEGFLAG.DEPOT
                && (CONTROL_RUNMODE)SignalDataInTCT.ControlMode > CONTROL_RUNMODE.RUN_MODE_RM && (CONTROL_RUNMODE)SignalDataInTCT.ControlMode != CONTROL_RUNMODE.RUN_MODE_NRM
                && (CONTROL_RUNMODE)SignalDataInTCT.ControlMode != CONTROL_RUNMODE.RUN_MODE_OFF)
            {
                CBTC.Message.MessageFactory.CreateMessage(2);
            }
            else
            {
                CBTC.Message.MessageFactory.RemoveMessage(2);
            }

            //RATO正常
            CBTC.TrainInfo.RATOState = RATOState.RATONormal;

           // CBTC.stationinfo.PSD.EM
           // CBTC.Message.MessageFactory.CreateMessage(54);

            //CBTC.Message.MessageFactory.CreateMessage(1);
            //CBTC.Message.MessageFactory.RemoveMessage(1);

            //CBTC.Message.MessageFactory.CreateMessage(13);
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
            SignalDataInTCT.ClearInfo();
            SignalDataOutTCT.ClearInfo();
            return false;
        }

        public override bool UpdateEqpmtFault()
        {
            if (SignalDataInTCT.Fault[0]/* && !SignalDataInTCTOld.Fault[1]*/)  //无线通信故障
            {
                CBTC.FaultInfo.RADRedFault = true;
            }
            else
            {
                CBTC.FaultInfo.RADRedFault = false;
            }

            if (SignalDataInTCT.Fault[1] /*&& !SignalDataInTCTOld.Fault[2]*/)  //ATP故障
            {
                CBTC.FaultInfo.ATPYellowFault = true;
            }
            else
            {
                CBTC.FaultInfo.ATPYellowFault = false;
            }

            if (SignalDataInTCT.Fault[2] /*&& !SignalDataInTCTOld.Fault[3]*/)  //ATO故障
            {
                CBTC.FaultInfo.ATORedFault = true;
            }
            else
            {
                CBTC.FaultInfo.ATORedFault = false;
            }

            if (SignalDataInTCT.Fault[3]/* && !SignalDataInTCTOld.Fault[4]*/)  //AM不可用（ATO输出反馈异常）
            {
                CBTC.FaultInfo.AMRedFault = true;
            }
            else
            {
                CBTC.FaultInfo.AMRedFault = false;
            }

            if (SignalDataInTCT.Fault[5] || !SignalDataInTCT.KeySwitch) //MMI通信中断
            {
                // CBTC.TrainInfo.BlackText = BlackText.MMICommunicationFault;
                CBTC.TrainInfo.BlackText = BlackText.None;
            }
            else
            {
                CBTC.TrainInfo.BlackText = BlackText.None;
            }



            return false;
        }

        public override bool OtherPageShowControlInfo()
        {
            //屏保、电钥匙未打开、ATP切除、列车自动折返等显示界面
            //屏保
            if (SignalDataInTCT.MMIBlackShowWord)
            {
                CBTC.TrainInfo.ScreenSaverEnable = true;
            }
            else
            {
                CBTC.TrainInfo.ScreenSaverEnable = false;
            }
            //屏保时间显示
            CBTC.TrainInfo.BlackTimeEnable = true;

            if (SignalDataInTCT.ATCPass)  //ATP已切除
            {
                //CBTC.TrainInfo.BlackText = BlackText.ATPAbscission;
                CBTC.TrainInfo.BlackText = BlackText.MMICommunicationFault;
            }

            if (SignalDataInTCT.BATBing)  // 无人折返
            {
                CBTC.TrainInfo.BlackText = BlackText.UNManuanlReturn;
            }


            //CBTC.TrainInfo.ScreenSaverEnable = true;


            //INFO子界面相关显示控制信息
            //红网状态:暂时做无线状态
            if (SignalDataInTCT.CBTCStatus == 1)
            {
                CBTC.TestInfo.RedStatus = NetStatus.NormalStatus;
            }
            //CBTC.TestInfo.RedStatus = NetStatus.NormalStatus;
            //蓝网状态：暂时做列车定位状态
            if (SignalDataInTCT.LocationBuildFlag)
            {
                CBTC.TestInfo.BlueStatus = NetStatus.NormalStatus;
            }
            //CBTC.TestInfo.BlueStatus = NetStatus.ErrorStatus;
            //广播测试
            CBTC.TestInfo.BroadcastTestStatus = BroadcastTestStatus.StatusUnKnown;


            if (SignalDataInTCT.EBBrakeTestSwitch)  //试闸测试
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