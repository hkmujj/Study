using System;
using System.Windows.Threading;
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
using MMI.Facility.Interface;
using MMI.Facility.Interface.Extension;

namespace CBTC.DataAdapter.ConcreateAdapter.CASCO
{
    public class CASCODataAdapter : DataAdapterBase
    {
        protected SignalDataInCASCO SignalDataInCASCO { get; set; }
        protected SignalDataInCASCO SignalDataInCASCOOld { get; set; }
        protected SignalDataOutCASCO SignalDataOutCASCO { get; set; }

        public CASCODataAdapter(Infrasturcture.Model.CBTC adaptTarget)
            : base(adaptTarget, new SendInterfaceCASCO(new SignalDataOutCASCO()))
        {
            SignalDataInCASCO = new SignalDataInCASCO();
            SignalDataInCASCOOld = new SignalDataInCASCO();
            SignalDataInCASCOOld = SignalDataInCASCO.Copy();
            SignalDataIn = SignalDataInCASCO;
            SignalDataInOld = SignalDataInCASCOOld;

            SignalDataOutCASCO = (SignalDataOutCASCO)SignalDataOut;
            SignalDataOutCASCO.SignalDataIn = SignalDataInCASCO;
            SignalDataOutCASCO.CBTC = CBTC;
            SignalDataOut = SignalDataOutCASCO;
        }

        public override void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.OnBoolChangedImp(sender, dataChangedArgs);
            //CBTC_BM按钮
            dataChangedArgs.UpdateIfContains(InbKeys.CBTC_BM按钮, b => SignalDataInCASCO.CBTC_BM_Button = b);

            //反馈_强制BM确认
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_强制BM确认, b => SignalDataInCASCO.FeedBackBMACK_Confirm = b);
            //反馈_非强制BM确认
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_非强制BM确认, b => SignalDataInCASCO.FeedBackBMUNACK_Confirm = b);

            //CBTC.Message.MessageFactory.CreateMessage(2, DateTime.Now);
            //CBTC.Message.MessageFactory.CreateMessage(3, DateTime.Now);

            FeedBackInfo(sender, dataChangedArgs);
        }
        public override bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.FeedBackInfo(sender, dataChangedArgs);
            //反馈_强制BM确认
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_强制BM确认, b =>
            {
                if (b)
                {
                    SignalDataOutCASCO.BMACK_Confirm = false;
                }
            });

            //反馈_非强制BM确认
            dataChangedArgs.UpdateIfContains(InbKeys.反馈_非强制BM确认, b =>
            {
                if (b)
                {
                    SignalDataOutCASCO.BMUNACK_Confirm = false;
                }
            });
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
            if ((!SignalDataInCASCOOld.PowerFlag && SignalDataInCASCO.PowerFlag))
            {
                ClearInfos();
            }
            SignalDataInCASCOOld = SignalDataInCASCO.Copy();

            //4、设备故障状态更新
            UpdateEqpmtFault();
        }




        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();
            //强制BM确认
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OuBKeys.强制BM确认],
             SignalDataOutCASCO.BMACK_Confirm, true);
            //非强制BM确认
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OuBKeys.非强制BM确认],
            SignalDataOutCASCO.BMUNACK_Confirm, true);
            return true;
        }



        public override bool BasicAndTrainInfo()
        {
            //时间日期
            CBTC.Other.TimeVisible = true;
            CBTC.Other.DateVisible = true;
            CBTC.Other.NowInCBTC = SignalDataInCASCO.NowTime;
            //电源
            CBTC.TrainInfo.PowerState = SignalDataInCASCO.PowerFlag ? PowerState.Started : PowerState.Stopped;
            CBTC.TrainInfo.WorkState = (WorkState)SignalDataInCASCO.WorkStatus;
            // CBTC.SignalInfo.Speed.BrakeDetailsType = (BrakeType)SignalDataInCASCO.BrakeStatus;
            //信号屏左上角制动状态
            if ((BREAKSTATUS)SignalDataInCASCO.BrakeStatus == BREAKSTATUS.RequestBrake) //制动预警
            {
                CBTC.SignalInfo.Speed.BrakeDetailsType = BrakeDetailsType.OverSpeed;
            }
            else if ((BREAKSTATUS)SignalDataInCASCO.BrakeStatus == BREAKSTATUS.EmergencyBrake) //紧急制动
            {
                CBTC.SignalInfo.Speed.BrakeDetailsType = BrakeDetailsType.EnmergencyBrake;
            }
            else
            {
                CBTC.SignalInfo.Speed.BrakeDetailsType = BrakeDetailsType.Normal;
            }

            //编组
            //CBTC.TrainInfo.TrainLength
            //强制与非强制框显示
            App.Current.GetMainDispatcher().BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                if (SignalDataInCASCO.CBTC_BM_Button)
                {
                    CBTC.Request.RequestView(ViewNames.BM);
                }
                else if (SignalDataInCASCO.FeedBackBMACK_Confirm || SignalDataInCASCO.FeedBackBMUNACK_Confirm)
                {
                    CBTC.Request.RequestView(ViewNames.Menu);
                }
            }));
            


            //发车时间、停站时间是否需要显示
            if (SignalDataInCASCO.TrainSpeed > 0.01)
            {
                CBTC.RoadInfo.StationInfo.PSD.DepartTimeVisible = false;
                CBTC.RoadInfo.StationInfo.PSD.DepartSecondVisble = false;
            }
            else
            {
                CBTC.RoadInfo.StationInfo.PSD.DepartTimeVisible = true;
                CBTC.RoadInfo.StationInfo.PSD.DepartSecondVisble = true;
            }

            //离站时间显示
            if (SignalDataInCASCO.LeftTime > 0.01 && SignalDataInCASCO.LeftTime < 999)
            {
                CBTC.RoadInfo.StationInfo.PSD.DepartSecond = SignalDataInCASCO.LeftTime;
            }
            else
            {
                CBTC.RoadInfo.StationInfo.PSD.DepartSecond = 0;
            }

            //制动
            CBTC.TrainInfo.BrakeInfo.SignalBrake = (BrakeType)SignalDataInCASCO.BrakeStatus;
            CBTC.TrainInfo.BrakeInfo.BrakeState = (BrakeState)SignalDataInCASCO.IdlingStatus;

            //司机室(车首、车尾)状态
            CBTC.TrainInfo.CarriageInfo.CurrentCab.State = (CabState)SignalDataInCASCO.OBCUHeadStatus;
            CBTC.TrainInfo.CarriageInfo.RemoteCab.State = (CabState)SignalDataInCASCO.OBCUTailStatus;
            //车载VOBC状态
            CBTC.TrainInfo.CarriageInfo.VOBCState = (VOBCState)SignalDataInCASCO.VOBCStatus;
            //列车完整性
            CBTC.TrainInfo.CompleteState = SignalDataInCASCO.TrainCompleteFlag ? CompleteState.Completed : CompleteState.Lose;

            //车门
            CBTC.TrainInfo.DoorInfo.Bypassed = ((DOORPERMITSTATUS)SignalDataInCASCO.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS
                || (DOORPERMITSTATUS)SignalDataInCASCO.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS) ? true : false;
            CBTC.TrainInfo.DoorInfo.DoorControlMode = (DoorControlMode)SignalDataInCASCO.DoorOperationMode;
            if ((DOORPERMITSTATUS)SignalDataInCASCO.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInCASCO.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.AllowBoth;
            }
            else if ((DOORPERMITSTATUS)SignalDataInCASCO.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInCASCO.DoorRightPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.LeftAllow;
            }
            else if ((DOORPERMITSTATUS)SignalDataInCASCO.DoorLeftPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
               && (DOORPERMITSTATUS)SignalDataInCASCO.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.RightAllow;
            }
            else
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.NoAllow;
            }

            //人工折返站台图标
            if (((DOCKEDSTATUS)SignalDataInCASCO.DockedStatus == DOCKEDSTATUS.DOCKEDSTATUS_ALIGNED && (SEGFLAG)SignalDataInCASCO.TrainCurAreaType == SEGFLAG.MTBPLATFORM)
                || (SEGFLAG)SignalDataInCASCO.TrainCurAreaType == SEGFLAG.TRANSITTRACK)
            {
                if (!SignalDataInCASCO.TurnBackConfirmFlag)
                {
                    CBTC.RoadInfo.ReturnState = ReturnState.ManuanlReturn;
                }
                else
                {
                    CBTC.RoadInfo.ReturnState = ReturnState.ManuanlReturnActived;
                }
            }

            //自动折返站台图标
            if (SignalDataInCASCO.DockedStatus == 1 && (SEGFLAG)SignalDataInCASCO.TrainCurAreaType == SEGFLAG.ATBPLATFORM
                && (CONTROL_LEVEL)SignalDataInCASCO.RunLevel == CONTROL_LEVEL.CONTROL_LEVEL_CTC)
            {
                if (!SignalDataInCASCO.TurnBackConfirmFlag)
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
            //CBTC.RoadInfo.ReturnState = ReturnState.AutoReturn;
            CBTC.TrainInfo.DoorInfo.LeftDoorState = (DoorState)SignalDataInCASCO.DoorLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightDoorState = (DoorState)SignalDataInCASCO.DoorRightStatus;
            CBTC.TrainInfo.DoorInfo.LeftPSDState = (DoorState)SignalDataInCASCO.PSDLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightPSDState = (DoorState)SignalDataInCASCO.PSDRightStatus;
            //OBCU重复待删除
            //CBTC.RunningInfo.DepartState
            //列车运行模式
            CBTC.RunningInfo.OperatingGrade = (OperatingGrade)SignalDataInCASCO.RunLevel;
            if ((TrainOperatingMode)SignalDataInCASCO.ControlMode == TrainOperatingMode.ATB)
            {
                CBTC.RunningInfo.TrainOperatingMode = TrainOperatingMode.AM;
            }
            else if ((TrainOperatingMode)SignalDataInCASCO.ControlMode == TrainOperatingMode.RMR)
            {
                CBTC.RunningInfo.TrainOperatingMode = TrainOperatingMode.RM;
            }
            else
            {
                CBTC.RunningInfo.TrainOperatingMode = (TrainOperatingMode)SignalDataInCASCO.ControlMode;
            }

            CBTC.RunningInfo.TrainPosition = TrainPosition.Initalize;
            if (SignalDataInCASCO.DepotEnterFlag)
            {
                CBTC.RunningInfo.TrainPosition = TrainPosition.CarDepot;
            }
            //CBTC.RunningInfo.ParkingState = (ParkingStationState)SignalDataInCASCO.DockedStatus;
            CBTC.RunningInfo.ParkingAlignmentState = (ParkingAlignmentState)SignalDataInCASCO.DockedStatus;

            CBTC.SignalInfo.ATPConnectState = (ATPConnectState)SignalDataInCASCO.CBTCStatus;
            CBTC.SignalInfo.JumpStop = (JumpStopDetainCar)SignalDataInCASCO.StationControlTrainStatus;
            //CBTC.SignalInfo.CabSignalCode ;
            CBTC.SignalInfo.HighDirveModel = (HighDirveModel)SignalDataInCASCO.MaxPermitMode;
            CBTC.SignalInfo.Speed.IsZeroSpeed = ((SPEEDSTATUS)SignalDataInCASCO.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_ZERO) ? true : false;
            CBTC.SignalInfo.Speed.IsSpeeding = ((SPEEDSTATUS)SignalDataInCASCO.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_OVER) ? true : false;

            //线路
            CBTC.RoadInfo.StationInfo.NextStation = SignalDataInCASCO.NextStationNo > 0
                ? GetStationName((ulong)SignalDataInCASCO.NextStationNo)
                : "";
            CBTC.RoadInfo.StationInfo.EndStation = SignalDataInCASCO.EndStationNo > 0
                ? GetStationName((ulong)SignalDataInCASCO.EndStationNo)
                : "";

            // CBTC.RoadInfo.ReturnState = ReturnState.None;
            CBTC.RoadInfo.DriverID = "";
            CBTC.RoadInfo.DesID = "";
            CBTC.RoadInfo.TrainID = "";

            //if ((RealTimeWokeStatus)SignalDataInCASCO.RealTimeWorkStatus == RealTimeWokeStatus.EB)  //紧急制动
            //{
            //    CBTC.TrainInfo.BrakeInfo.EmergencyBrake = true;
            //    //CBTC.TrainInfo.RealTimeWokeStatus = true;
            //}
            //else
            //{
            //    CBTC.TrainInfo.BrakeInfo.EmergencyBrake = false;
            //}
            // CBTC.TrainInfo.RealTimeWokeStatus = (RealTimeWORKSTATUS)SignalDataInCASCO.RealTimeWorkStatus;
            CBTC.TrainInfo.RealTimeWokeStatus = (RealTimeWokeStatus)SignalDataInCASCO.RealTimeWorkStatus;
            CBTC.FaultInfo.FaultLocation = FaultLocation.None;
            CBTC.TrainInfo.DepotDriver = !(SignalDataInCASCO.LocationBuildFlag);  //列车定位丢失(THALES:位置)
            return false;
        }

        public override bool ScreenSpeedInfo()
        {
            base.ScreenSpeedInfo();

            if ((TrainOperatingMode)SignalDataIn.ControlMode == TrainOperatingMode.AM
                || (TrainOperatingMode)SignalDataIn.ControlMode == TrainOperatingMode.ATB)
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

        public override bool UpdateEqpmtFault()
        {
            if ((CBTCCONNECTSTATUS)SignalDataInCASCO.CBTCStatus == CBTCCONNECTSTATUS.CBTCCONNECTSTATUS_NOCONNECT)  //无线通信故障
            {
                CBTC.FaultInfo.RADRedFault = true;
            }
            else
            {
                CBTC.FaultInfo.RADRedFault = false;
            }
            return true;
            // ReferenceEquals()
            //return base.UpdateEqpmtFault();
        }

        public override bool ScreenBtnEnable()
        {
            return false;
        }

        public override bool ScreenTextInfo()
        {
            //要求进入RM模式
            if ((SEGFLAG)SignalDataInCASCO.TrainNextAreaType == SEGFLAG.DEPOT && (SEGFLAG)SignalDataInCASCO.TrainCurAreaType == SEGFLAG.TRANSITTRACK
                && (CONTROL_RUNMODE)SignalDataInCASCO.ControlMode > CONTROL_RUNMODE.RUN_MODE_RM)
            {
                CBTC.Message.MessageFactory.CreateMessage(2, DateTime.Now);
            }
            else
            {
                CBTC.Message.MessageFactory.RemoveMessage(2);
            }
            //MCS/ATO模式可用
            if ((CBTCCONNECTSTATUS)SignalDataInCASCO.CBTCStatus == CBTCCONNECTSTATUS.CBTCCONNECTSTATUS_CONNECTED
                && SignalDataInCASCO.LocationBuildFlag)
            {
                CBTC.Message.MessageFactory.CreateMessage(13, DateTime.Now);
            }
            //else
            //{
            //    CBTC.Message.MessageFactory.RemoveMessage(13);
            //}


            //由于离开TGMT区域，切换到RM模式
            if ((SEGFLAG)SignalDataInCASCO.TrainNextAreaType == SEGFLAG.DEPOT && (SEGFLAG)SignalDataInCASCO.TrainCurAreaType == SEGFLAG.TRANSITTRACK
                && (CONTROL_RUNMODE)SignalDataInCASCO.ControlMode == CONTROL_RUNMODE.RUN_MODE_RM)
            {
                CBTC.Message.MessageFactory.CreateMessage(16, DateTime.Now);
            }
            //else
            //{
            //    CBTC.Message.MessageFactory.RemoveMessage(16);
            //}
            //激活确认按钮
            if (SignalDataInCASCO.TxtConfirmFlag)
            {
                CBTC.Message.MessageFactory.CreateMessage(19, DateTime.Now);
            }
            //else
            //{
            //    CBTC.Message.MessageFactory.RemoveMessage(19);
            //}

            //激活ATO启动按钮://0、无 	1、ATO模式开关		2、ATO模式选择		3、ATO启动按钮
            if (SignalDataInCASCO.ATORelationSwitch == 3)
            {
                CBTC.Message.MessageFactory.CreateMessage(20, DateTime.Now);
            }
            else
            {
                CBTC.Message.MessageFactory.RemoveMessage(20);
            }

            //激活自动折返按钮
            if (SignalDataInCASCO.AtbButton)
            {
                CBTC.Message.MessageFactory.CreateMessage(21, DateTime.Now);
            }
            else
            {
                CBTC.Message.MessageFactory.RemoveMessage(21);
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
            SignalDataInCASCO.ClearInfo();
            SignalDataOutCASCO.ClearInfo();
            return false;
        }
    }
}