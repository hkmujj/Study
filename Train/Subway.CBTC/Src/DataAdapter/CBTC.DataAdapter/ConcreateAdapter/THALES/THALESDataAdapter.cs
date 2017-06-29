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
            FeedBackInfo(sender, dataChangedArgs);
        }
        public override bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
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
            CBTC.Other.NowInCBTC = SignalDataInTHALES.NowTime;
            //电源
            CBTC.TrainInfo.PowerState = SignalDataInTHALES.PowerFlag ? PowerState.Started : PowerState.Stopped;
            CBTC.TrainInfo.WorkState = (WorkState)SignalDataInTHALES.WorkStatus;

            //编组
             //CBTC.TrainInfo.TrainLength

            //制动
            CBTC.TrainInfo.BrakeInfo.SignalBrake = (BrakeType)SignalDataInTHALES.BrakeStatus;
            CBTC.TrainInfo.BrakeInfo.BrakeState = (BrakeState)SignalDataInTHALES.IdlingStatus;

            //司机室
            CBTC.TrainInfo.CarriageInfo.CurrentCab.State = (CabState)SignalDataInTHALES.OBCUHeadStatus;
            CBTC.TrainInfo.CarriageInfo.RemoteCab.State = (CabState)SignalDataInTHALES.OBCUTailStatus;
            CBTC.TrainInfo.CompleteState = SignalDataInTHALES.TrainCompleteFlag ? CompleteState.Completed : CompleteState.Lose;

            //车门
            CBTC.TrainInfo.DoorInfo.Bypassed = ((DOORPERMITSTATUS)SignalDataInTHALES.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS
                || (DOORPERMITSTATUS)SignalDataInTHALES.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS) ? true : false;
            CBTC.TrainInfo.DoorInfo.DoorControlMode = (DoorControlMode)SignalDataInTHALES.DoorOperationMode;
            if ((DOORPERMITSTATUS)SignalDataInTHALES.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInTHALES.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.AllowBoth;
            }
            else if ((DOORPERMITSTATUS)SignalDataInTHALES.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInTHALES.DoorRightPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.LeftAllow;
            }
            else if ((DOORPERMITSTATUS)SignalDataInTHALES.DoorLeftPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
               && (DOORPERMITSTATUS)SignalDataInTHALES.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.RightAllow;
            }
            else
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.NoAllow;
            }
            CBTC.TrainInfo.DoorInfo.LeftDoorState = (DoorState)SignalDataInTHALES.DoorLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightDoorState = (DoorState)SignalDataInTHALES.DoorRightStatus;
            CBTC.TrainInfo.DoorInfo.LeftPSDState = (DoorState)SignalDataInTHALES.PSDLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightPSDState = (DoorState)SignalDataInTHALES.PSDLeftStatus;
            //OBCU重复待删除
             //CBTC.RunningInfo.DepartState
            CBTC.RunningInfo.OperatingGrade = (OperatingGrade)SignalDataInTHALES.RunLevel;
            CBTC.RunningInfo.TrainOperatingMode = (TrainOperatingMode)SignalDataInTHALES.ControlMode;
            CBTC.RunningInfo.TrainPosition = TrainPosition.Initalize;
            if (SignalDataInTHALES.DepotEnterFlag)
            {
                CBTC.RunningInfo.TrainPosition = TrainPosition.CarDepot;
            }
            //CBTC.RunningInfo.ParkingState = (ParkingStationState)SignalDataInTHALES.DockedStatus;
            CBTC.RunningInfo.ParkingAlignmentState = (ParkingAlignmentState)SignalDataInTHALES.DockedStatus;

            CBTC.SignalInfo.ATPConnectState = (ATPConnectState)SignalDataInTHALES.CBTCStatus;
            CBTC.SignalInfo.JumpStop = (JumpStopDetainCar)SignalDataInTHALES.StationControlTrainStatus;
            //CBTC.SignalInfo.CabSignalCode ;
            CBTC.SignalInfo.HighDirveModel = (HighDirveModel)SignalDataInTHALES.MaxPermitMode;
            CBTC.SignalInfo.Speed.IsZeroSpeed = ((SPEEDSTATUS)SignalDataInTHALES.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_ZERO) ? true : false;
            CBTC.SignalInfo.Speed.IsSpeeding = ((SPEEDSTATUS)SignalDataInTHALES.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_OVER) ? true : false;

            //线路
            CBTC.RoadInfo.StationInfo.NextStation= SignalDataInTHALES.NextStationNo > 0
                ? GetStationName((ulong)SignalDataInTHALES.NextStationNo)
                : "";
            CBTC.RoadInfo.StationInfo.EndStation = SignalDataInTHALES.EndStationNo > 0
                ? GetStationName((ulong)SignalDataInTHALES.EndStationNo)
                : "";

            CBTC.RoadInfo.ReturnState = ReturnState.None;
            CBTC.RoadInfo.DriverID = "";
            CBTC.RoadInfo.DesID = "";
            CBTC.RoadInfo.TrainID = "";

            CBTC.FaultInfo.FaultLocation = FaultLocation.None;
            return false;
        } 

        public override bool ScreenBtnEnable()
        {
            return false;
        }

        public override bool ScreenTextInfo()
        {
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
    }
}