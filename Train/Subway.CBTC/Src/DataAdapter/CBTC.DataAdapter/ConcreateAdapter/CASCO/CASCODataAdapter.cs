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
            if ((!SignalDataInCASCOOld.PowerFlag && SignalDataInCASCO.PowerFlag))
            {
                ClearInfos();
            }
            SignalDataInCASCOOld = SignalDataInCASCO.Copy();
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
            CBTC.Other.NowInCBTC = SignalDataInCASCO.NowTime;
            //电源
            CBTC.TrainInfo.PowerState = SignalDataInCASCO.PowerFlag ? PowerState.Started : PowerState.Stopped;
            CBTC.TrainInfo.WorkState = (WorkState)SignalDataInCASCO.WorkStatus;

            //编组
             //CBTC.TrainInfo.TrainLength

            //制动
            CBTC.TrainInfo.BrakeInfo.SignalBrake = (BrakeType)SignalDataInCASCO.BrakeStatus;
            CBTC.TrainInfo.BrakeInfo.BrakeState = (BrakeState)SignalDataInCASCO.IdlingStatus;

            //司机室
            CBTC.TrainInfo.CarriageInfo.CurrentCab.State = (CabState)SignalDataInCASCO.OBCUHeadStatus;
            CBTC.TrainInfo.CarriageInfo.RemoteCab.State = (CabState)SignalDataInCASCO.OBCUTailStatus;
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
            CBTC.TrainInfo.DoorInfo.LeftDoorState = (DoorState)SignalDataInCASCO.DoorLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightDoorState = (DoorState)SignalDataInCASCO.DoorRightStatus;
            CBTC.TrainInfo.DoorInfo.LeftPSDState = (DoorState)SignalDataInCASCO.PSDLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightPSDState = (DoorState)SignalDataInCASCO.PSDLeftStatus;
            //OBCU重复待删除
             //CBTC.RunningInfo.DepartState
            CBTC.RunningInfo.OperatingGrade = (OperatingGrade)SignalDataInCASCO.RunLevel;
            CBTC.RunningInfo.TrainOperatingMode = (TrainOperatingMode)SignalDataInCASCO.ControlMode;
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
            CBTC.RoadInfo.StationInfo.NextStation= SignalDataInCASCO.NextStationNo > 0
                ? GetStationName((ulong)SignalDataInCASCO.NextStationNo)
                : "";
            CBTC.RoadInfo.StationInfo.EndStation = SignalDataInCASCO.EndStationNo > 0
                ? GetStationName((ulong)SignalDataInCASCO.EndStationNo)
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
            SignalDataInCASCO.ClearInfo();
            SignalDataOutCASCO.ClearInfo();
            return false;
        }
    }
}