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
namespace CBTC.DataAdapter.ConcreateAdapter.BOMBARDIER
{
    public class BOMBARDIERDataAdapter : DataAdapterBase
    {
        protected SignalDataInBOMBARDIER SignalDataInBOMBARDIER { get; set; }
        protected SignalDataInBOMBARDIER SignalDataInBOMBARDIEROld { get; set; }
        protected SignalDataOutBOMBARDIER SignalDataOutBOMBARDIER { get; set; }

        public BOMBARDIERDataAdapter(Infrasturcture.Model.CBTC adaptTarget)
            : base(adaptTarget, new SendInterfaceBOMBARDIER(new SignalDataOutBOMBARDIER()))
        {
            SignalDataInBOMBARDIER = new SignalDataInBOMBARDIER();
            SignalDataInBOMBARDIEROld = new SignalDataInBOMBARDIER();
            SignalDataInBOMBARDIEROld = SignalDataInBOMBARDIER.Copy();
            SignalDataIn = SignalDataInBOMBARDIER;
            SignalDataInOld = SignalDataInBOMBARDIEROld;

            SignalDataOutBOMBARDIER = (SignalDataOutBOMBARDIER)SignalDataOut;
            SignalDataOutBOMBARDIER.SignalDataIn = SignalDataInBOMBARDIER;
            SignalDataOutBOMBARDIER.CBTC = CBTC;
            SignalDataOut = SignalDataOutBOMBARDIER;
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
            if ((!SignalDataInBOMBARDIEROld.PowerFlag && SignalDataInBOMBARDIER.PowerFlag))
            {
                ClearInfos();
            }
            SignalDataInBOMBARDIEROld = SignalDataInBOMBARDIER.Copy();
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
            CBTC.Other.NowInCBTC = SignalDataInBOMBARDIER.NowTime;
            //电源
            CBTC.TrainInfo.PowerState = SignalDataInBOMBARDIER.PowerFlag ? PowerState.Started : PowerState.Stopped;
            CBTC.TrainInfo.WorkState = (WorkState)SignalDataInBOMBARDIER.WorkStatus;

            //编组
             //CBTC.TrainInfo.TrainLength

            //制动
            CBTC.TrainInfo.BrakeInfo.SignalBrake = (BrakeType)SignalDataInBOMBARDIER.BrakeStatus;
            CBTC.TrainInfo.BrakeInfo.BrakeState = (BrakeState)SignalDataInBOMBARDIER.IdlingStatus;

            //司机室
            CBTC.TrainInfo.CarriageInfo.CurrentCab.State = (CabState)SignalDataInBOMBARDIER.OBCUHeadStatus;
            CBTC.TrainInfo.CarriageInfo.RemoteCab.State = (CabState)SignalDataInBOMBARDIER.OBCUTailStatus;
            CBTC.TrainInfo.CompleteState = SignalDataInBOMBARDIER.TrainCompleteFlag ? CompleteState.Completed : CompleteState.Lose;

            //车门
            CBTC.TrainInfo.DoorInfo.Bypassed = ((DOORPERMITSTATUS)SignalDataInBOMBARDIER.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS
                || (DOORPERMITSTATUS)SignalDataInBOMBARDIER.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS) ? true : false;
            CBTC.TrainInfo.DoorInfo.DoorControlMode = (DoorControlMode)SignalDataInBOMBARDIER.DoorOperationMode;
            if ((DOORPERMITSTATUS)SignalDataInBOMBARDIER.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInBOMBARDIER.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.AllowBoth;
            }
            else if ((DOORPERMITSTATUS)SignalDataInBOMBARDIER.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInBOMBARDIER.DoorRightPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.LeftAllow;
            }
            else if ((DOORPERMITSTATUS)SignalDataInBOMBARDIER.DoorLeftPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
               && (DOORPERMITSTATUS)SignalDataInBOMBARDIER.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.RightAllow;
            }
            else
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.NoAllow;
            }
            CBTC.TrainInfo.DoorInfo.LeftDoorState = (DoorState)SignalDataInBOMBARDIER.DoorLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightDoorState = (DoorState)SignalDataInBOMBARDIER.DoorRightStatus;
            CBTC.TrainInfo.DoorInfo.LeftPSDState = (DoorState)SignalDataInBOMBARDIER.PSDLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightPSDState = (DoorState)SignalDataInBOMBARDIER.PSDLeftStatus;
            //OBCU重复待删除
             //CBTC.RunningInfo.DepartState
            CBTC.RunningInfo.OperatingGrade = (OperatingGrade)SignalDataInBOMBARDIER.RunLevel;
            CBTC.RunningInfo.TrainOperatingMode = (TrainOperatingMode)SignalDataInBOMBARDIER.ControlMode;
            CBTC.RunningInfo.TrainPosition = TrainPosition.Initalize;
            if (SignalDataInBOMBARDIER.DepotEnterFlag)
            {
                CBTC.RunningInfo.TrainPosition = TrainPosition.CarDepot;
            }
            //CBTC.RunningInfo.ParkingState = (ParkingStationState)SignalDataInBOMBARDIER.DockedStatus;
            CBTC.RunningInfo.ParkingAlignmentState = (ParkingAlignmentState)SignalDataInBOMBARDIER.DockedStatus;

            CBTC.SignalInfo.ATPConnectState = (ATPConnectState)SignalDataInBOMBARDIER.CBTCStatus;
            CBTC.SignalInfo.JumpStop = (JumpStopDetainCar)SignalDataInBOMBARDIER.StationControlTrainStatus;
            //CBTC.SignalInfo.CabSignalCode ;
            CBTC.SignalInfo.HighDirveModel = (HighDirveModel)SignalDataInBOMBARDIER.MaxPermitMode;
            CBTC.SignalInfo.Speed.IsZeroSpeed = ((SPEEDSTATUS)SignalDataInBOMBARDIER.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_ZERO) ? true : false;
            CBTC.SignalInfo.Speed.IsSpeeding = ((SPEEDSTATUS)SignalDataInBOMBARDIER.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_OVER) ? true : false;

            //线路
            CBTC.RoadInfo.StationInfo.NextStation= SignalDataInBOMBARDIER.NextStationNo > 0
                ? GetStationName((ulong)SignalDataInBOMBARDIER.NextStationNo)
                : "";
            CBTC.RoadInfo.StationInfo.EndStation = SignalDataInBOMBARDIER.EndStationNo > 0
                ? GetStationName((ulong)SignalDataInBOMBARDIER.EndStationNo)
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
            SignalDataInBOMBARDIER.ClearInfo();
            SignalDataOutBOMBARDIER.ClearInfo();
            return false;
        }
    }
}