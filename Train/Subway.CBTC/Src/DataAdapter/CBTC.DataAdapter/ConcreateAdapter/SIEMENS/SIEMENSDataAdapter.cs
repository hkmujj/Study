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
namespace CBTC.DataAdapter.ConcreateAdapter.SIEMENS
{
    public class SIEMENSDataAdapter : DataAdapterBase
    {
        protected SignalDataInSIEMENS SignalDataInSIEMENS { get; set; }
        protected SignalDataInSIEMENS SignalDataInSIEMENSOld { get; set; }
        protected SignalDataOutSIEMENS SignalDataOutSIEMENS { get; set; }

        public SIEMENSDataAdapter(Infrasturcture.Model.CBTC adaptTarget)
            : base(adaptTarget, new SendInterfaceSIEMENS(new SignalDataOutSIEMENS()))
        {
            SignalDataInSIEMENS = new SignalDataInSIEMENS();
            SignalDataInSIEMENSOld = new SignalDataInSIEMENS();
            SignalDataInSIEMENSOld = SignalDataInSIEMENS.Copy();
            SignalDataIn = SignalDataInSIEMENS;
            SignalDataInOld = SignalDataInSIEMENSOld;

            SignalDataOutSIEMENS = (SignalDataOutSIEMENS)SignalDataOut;
            SignalDataOutSIEMENS.SignalDataIn = SignalDataInSIEMENS;
            SignalDataOutSIEMENS.CBTC = CBTC;
            SignalDataOut = SignalDataOutSIEMENS;
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
            if ((!SignalDataInSIEMENSOld.PowerFlag && SignalDataInSIEMENS.PowerFlag))
            {
                ClearInfos();
            }
            SignalDataInSIEMENSOld = SignalDataInSIEMENS.Copy();
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
            CBTC.Other.NowInCBTC = SignalDataInSIEMENS.NowTime;
            //电源
            CBTC.TrainInfo.PowerState = SignalDataInSIEMENS.PowerFlag ? PowerState.Started : PowerState.Stopped;
            CBTC.TrainInfo.WorkState = (WorkState)SignalDataInSIEMENS.WorkStatus;

            //编组
             //CBTC.TrainInfo.TrainLength

            //制动
            CBTC.TrainInfo.BrakeInfo.SignalBrake = (BrakeType)SignalDataInSIEMENS.BrakeStatus;
            CBTC.TrainInfo.BrakeInfo.BrakeState = (BrakeState)SignalDataInSIEMENS.IdlingStatus;

            //司机室
            CBTC.TrainInfo.CarriageInfo.CurrentCab.State = (CabState)SignalDataInSIEMENS.OBCUHeadStatus;
            CBTC.TrainInfo.CarriageInfo.RemoteCab.State = (CabState)SignalDataInSIEMENS.OBCUTailStatus;
            CBTC.TrainInfo.CompleteState = SignalDataInSIEMENS.TrainCompleteFlag ? CompleteState.Completed : CompleteState.Lose;

            //车门
            CBTC.TrainInfo.DoorInfo.Bypassed = ((DOORPERMITSTATUS)SignalDataInSIEMENS.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS
                || (DOORPERMITSTATUS)SignalDataInSIEMENS.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS) ? true : false;
            CBTC.TrainInfo.DoorInfo.DoorControlMode = (DoorControlMode)SignalDataInSIEMENS.DoorOperationMode;
            if ((DOORPERMITSTATUS)SignalDataInSIEMENS.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInSIEMENS.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.AllowBoth;
            }
            else if ((DOORPERMITSTATUS)SignalDataInSIEMENS.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInSIEMENS.DoorRightPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.LeftAllow;
            }
            else if ((DOORPERMITSTATUS)SignalDataInSIEMENS.DoorLeftPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
               && (DOORPERMITSTATUS)SignalDataInSIEMENS.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.RightAllow;
            }
            else
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.NoAllow;
            }
            CBTC.TrainInfo.DoorInfo.LeftDoorState = (DoorState)SignalDataInSIEMENS.DoorLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightDoorState = (DoorState)SignalDataInSIEMENS.DoorRightStatus;
            CBTC.TrainInfo.DoorInfo.LeftPSDState = (DoorState)SignalDataInSIEMENS.PSDLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightPSDState = (DoorState)SignalDataInSIEMENS.PSDLeftStatus;
            //OBCU重复待删除
             //CBTC.RunningInfo.DepartState
            CBTC.RunningInfo.OperatingGrade = (OperatingGrade)SignalDataInSIEMENS.RunLevel;
            CBTC.RunningInfo.TrainOperatingMode = (TrainOperatingMode)SignalDataInSIEMENS.ControlMode;
            CBTC.RunningInfo.TrainPosition = TrainPosition.Initalize;
            if (SignalDataInSIEMENS.DepotEnterFlag)
            {
                CBTC.RunningInfo.TrainPosition = TrainPosition.CarDepot;
            }
            //CBTC.RunningInfo.ParkingState = (ParkingStationState)SignalDataInSIEMENS.DockedStatus;
            CBTC.RunningInfo.ParkingAlignmentState = (ParkingAlignmentState)SignalDataInSIEMENS.DockedStatus;

            CBTC.SignalInfo.ATPConnectState = (ATPConnectState)SignalDataInSIEMENS.CBTCStatus;
            CBTC.SignalInfo.JumpStop = (JumpStopDetainCar)SignalDataInSIEMENS.StationControlTrainStatus;
            //CBTC.SignalInfo.CabSignalCode ;
            CBTC.SignalInfo.HighDirveModel = (HighDirveModel)SignalDataInSIEMENS.MaxPermitMode;
            CBTC.SignalInfo.Speed.IsZeroSpeed = ((SPEEDSTATUS)SignalDataInSIEMENS.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_ZERO) ? true : false;
            CBTC.SignalInfo.Speed.IsSpeeding = ((SPEEDSTATUS)SignalDataInSIEMENS.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_OVER) ? true : false;

            //线路
            CBTC.RoadInfo.StationInfo.NextStation= SignalDataInSIEMENS.NextStationNo > 0
                ? GetStationName((ulong)SignalDataInSIEMENS.NextStationNo)
                : "";
            CBTC.RoadInfo.StationInfo.EndStation = SignalDataInSIEMENS.EndStationNo > 0
                ? GetStationName((ulong)SignalDataInSIEMENS.EndStationNo)
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
            SignalDataInSIEMENS.ClearInfo();
            SignalDataOutSIEMENS.ClearInfo();
            return false;
        }
    }
}