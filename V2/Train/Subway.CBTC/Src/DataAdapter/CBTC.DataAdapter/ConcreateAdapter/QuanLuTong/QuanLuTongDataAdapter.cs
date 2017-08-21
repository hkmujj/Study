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
namespace CBTC.DataAdapter.ConcreateAdapter.QuanLuTong
{
    public class QuanLuTongDataAdapter : DataAdapterBase
    {
        protected SignalDataInQuanLuTong SignalDataInQuanLuTong { get; set; }
        protected SignalDataInQuanLuTong SignalDataInQuanLuTongOld { get; set; }
        protected SignalDataOutQuanLuTong SignalDataOutQuanLuTong { get; set; }

        public QuanLuTongDataAdapter(Infrasturcture.Model.CBTC adaptTarget)
            : base(adaptTarget, new SendInterfaceQuanLuTong(new SignalDataOutQuanLuTong()))
        {
            SignalDataInQuanLuTong = new SignalDataInQuanLuTong();
            SignalDataInQuanLuTongOld = new SignalDataInQuanLuTong();
            SignalDataInQuanLuTongOld = SignalDataInQuanLuTong.Copy();
            SignalDataIn = SignalDataInQuanLuTong;
            SignalDataInOld = SignalDataInQuanLuTongOld;

            SignalDataOutQuanLuTong = (SignalDataOutQuanLuTong)SignalDataOut;
            SignalDataOutQuanLuTong.SignalDataIn = SignalDataInQuanLuTong;
            SignalDataOutQuanLuTong.CBTC = CBTC;
            SignalDataOut = SignalDataOutQuanLuTong;
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
            if ((!SignalDataInQuanLuTongOld.PowerFlag && SignalDataInQuanLuTong.PowerFlag))
            {
                ClearInfos();
            }
            SignalDataInQuanLuTongOld = SignalDataInQuanLuTong.Copy();
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
            CBTC.Other.NowInCBTC = SignalDataInQuanLuTong.NowTime;
            //电源
            CBTC.TrainInfo.PowerState = SignalDataInQuanLuTong.PowerFlag ? PowerState.Started : PowerState.Stopped;
            CBTC.TrainInfo.WorkState = (WorkState)SignalDataInQuanLuTong.WorkStatus;

            //编组
             //CBTC.TrainInfo.TrainLength

            //制动
            CBTC.TrainInfo.BrakeInfo.SignalBrake = (BrakeType)SignalDataInQuanLuTong.BrakeStatus;
            CBTC.TrainInfo.BrakeInfo.BrakeState = (BrakeState)SignalDataInQuanLuTong.IdlingStatus;

            //司机室
            CBTC.TrainInfo.CarriageInfo.CurrentCab.State = (CabState)SignalDataInQuanLuTong.OBCUHeadStatus;
            CBTC.TrainInfo.CarriageInfo.RemoteCab.State = (CabState)SignalDataInQuanLuTong.OBCUTailStatus;
            CBTC.TrainInfo.CompleteState = SignalDataInQuanLuTong.TrainCompleteFlag ? CompleteState.Completed : CompleteState.Lose;

            //车门
            CBTC.TrainInfo.DoorInfo.Bypassed = ((DOORPERMITSTATUS)SignalDataInQuanLuTong.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS
                || (DOORPERMITSTATUS)SignalDataInQuanLuTong.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_BYPASS) ? true : false;
            CBTC.TrainInfo.DoorInfo.DoorControlMode = (DoorControlMode)SignalDataInQuanLuTong.DoorOperationMode;
            if ((DOORPERMITSTATUS)SignalDataInQuanLuTong.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInQuanLuTong.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.AllowBoth;
            }
            else if ((DOORPERMITSTATUS)SignalDataInQuanLuTong.DoorLeftPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
                && (DOORPERMITSTATUS)SignalDataInQuanLuTong.DoorRightPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.LeftAllow;
            }
            else if ((DOORPERMITSTATUS)SignalDataInQuanLuTong.DoorLeftPermit != DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT
               && (DOORPERMITSTATUS)SignalDataInQuanLuTong.DoorRightPermit == DOORPERMITSTATUS.DOORPERMITSTATUS_PERMIT)
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.RightAllow;
            }
            else
            {
                CBTC.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.NoAllow;
            }
            CBTC.TrainInfo.DoorInfo.LeftDoorState = (DoorState)SignalDataInQuanLuTong.DoorLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightDoorState = (DoorState)SignalDataInQuanLuTong.DoorRightStatus;
            CBTC.TrainInfo.DoorInfo.LeftPSDState = (DoorState)SignalDataInQuanLuTong.PSDLeftStatus;
            CBTC.TrainInfo.DoorInfo.RightPSDState = (DoorState)SignalDataInQuanLuTong.PSDLeftStatus;
            //OBCU重复待删除
             //CBTC.RunningInfo.DepartState
            CBTC.RunningInfo.OperatingGrade = (OperatingGrade)SignalDataInQuanLuTong.RunLevel;
            CBTC.RunningInfo.TrainOperatingMode = (TrainOperatingMode)SignalDataInQuanLuTong.ControlMode;
            CBTC.RunningInfo.TrainPosition = TrainPosition.Initalize;
            if (SignalDataInQuanLuTong.DepotEnterFlag)
            {
                CBTC.RunningInfo.TrainPosition = TrainPosition.CarDepot;
            }
            //CBTC.RunningInfo.ParkingState = (ParkingStationState)SignalDataInQuanLuTong.DockedStatus;
            CBTC.RunningInfo.ParkingAlignmentState = (ParkingAlignmentState)SignalDataInQuanLuTong.DockedStatus;

            CBTC.SignalInfo.ATPConnectState = (ATPConnectState)SignalDataInQuanLuTong.CBTCStatus;
            CBTC.SignalInfo.JumpStop = (JumpStopDetainCar)SignalDataInQuanLuTong.StationControlTrainStatus;
            //CBTC.SignalInfo.CabSignalCode ;
            CBTC.SignalInfo.HighDirveModel = (HighDirveModel)SignalDataInQuanLuTong.MaxPermitMode;
            CBTC.SignalInfo.Speed.IsZeroSpeed = ((SPEEDSTATUS)SignalDataInQuanLuTong.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_ZERO) ? true : false;
            CBTC.SignalInfo.Speed.IsSpeeding = ((SPEEDSTATUS)SignalDataInQuanLuTong.SpeedStatus == SPEEDSTATUS.SPEEDSTATUS_OVER) ? true : false;

            //线路
            CBTC.RoadInfo.StationInfo.NextStation= SignalDataInQuanLuTong.NextStationNo > 0
                ? GetStationName((ulong)SignalDataInQuanLuTong.NextStationNo)
                : "";
            CBTC.RoadInfo.StationInfo.EndStation = SignalDataInQuanLuTong.EndStationNo > 0
                ? GetStationName((ulong)SignalDataInQuanLuTong.EndStationNo)
                : "";

            CBTC.RoadInfo.ReturnState = ReturnState.None;
            CBTC.RoadInfo.DriverID = "";
            CBTC.RoadInfo.DesID = "";
            CBTC.RoadInfo.TrainID = "";

            CBTC.FaultInfo.FaultLocation = FaultLocation.None;
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
            SignalDataInQuanLuTong.ClearInfo();
            SignalDataOutQuanLuTong.ClearInfo();
            return false;
        }
    }
}