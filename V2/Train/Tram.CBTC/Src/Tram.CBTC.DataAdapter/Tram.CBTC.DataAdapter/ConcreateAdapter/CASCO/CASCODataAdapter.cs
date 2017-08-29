using System;
using System.Diagnostics;
using CommonUtil.Util;
using Tram.CBTC.DataAdapter.Extension;
using Tram.CBTC.DataAdapter.Resource.Keys;
using Tram.CBTC.DataAdapter.Util;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Extension;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.DataAdapter.ConcreateAdapter.CASCO
{
    public class CASCODataAdapter : DataAdapterBase
    {
        protected SignalDataInCASCO SignalDataInCASCO { get; set; }
        protected SignalDataInCASCO SignalDataInCASCOOld { get; set; }
        protected SignalDataOutCASCO SignalDataOutCASCO { get; set; }

        public CASCODataAdapter(global::Tram.CBTC.Infrasturcture.Model.CBTC adaptTarget)
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
            ////故障更新
            //for (var i = InterfaceAdapterService.InBoolDictionary[InbKeys.故障1];
            //     i < SignalDataIn.Fault.Length + InterfaceAdapterService.InBoolDictionary[InbKeys.故障1];
            //     i++)
            //    dataChangedArgs.UpdateIfContains(i,
            //        b => SignalDataIn.Fault[i - InterfaceAdapterService.InBoolDictionary[InbKeys.故障1]] = b);

            //消息文本
            //for (var i = InterfaceAdapterService.InBoolDictionary[InbKeys.消息1];
            //     i < SignalDataIn.MessageInfo.Length + InterfaceAdapterService.InBoolDictionary[InbKeys.消息1];
            //     i++)
            //    dataChangedArgs.UpdateIfContains(i,
            //        b => SignalDataIn.MessageInfo[i - InterfaceAdapterService.InBoolDictionary[InbKeys.消息1]] = b);




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
            ////目标距离
            //dataChangedArgs.UpdateIfContains( (string)InfKeys.目标距离, f => SignalDataInCASCO.GoalDistance = f);
            //前方信号机距离
            dataChangedArgs.UpdateIfContains((string)InfKeys.前方信号机距离, f => SignalDataInCASCO.FrontSignalDistance = (int)f);
            //进路预选区终点距离
            dataChangedArgs.UpdateIfContains((string)InfKeys.进路预选区终点距离, f => SignalDataInCASCO.RouteSelectEndDistance = (int)f);
            ////前车距离
            //dataChangedArgs.UpdateIfContains((string)InfKeys.前车距离, f => SignalDataInCASCO.FrontSignalDistance = (int)f);
            ////后车距离
            //dataChangedArgs.UpdateIfContains((string)InfKeys.前方信号机距离, f => SignalDataInCASCO.FrontSignalDistance = (int)f);



            //距前车运行时分
            dataChangedArgs.UpdateIfContains((string)InfKeys.距前车运行时分, f => SignalDataInCASCO.MoveAllow = (int)f);
            //距后车运行时分
            dataChangedArgs.UpdateIfContains((string)InfKeys.距后车运行时分, f => SignalDataInCASCO.MoveAllow = (int)f);

            //制动状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.信号制动状态, f => SignalDataInCASCO.BrakeStatus = (int)f);

            //CBTC连接状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.CBTC连接状态, f => SignalDataInCASCO.CBTCStatus = (int)f);



            //折返信息
            dataChangedArgs.UpdateIfContains((string)InfKeys.折返信息, f => SignalDataInCASCO.TurnBackInfo = (int)f);
            //ATP保护控制
            dataChangedArgs.UpdateIfContains((string)InfKeys.ATP保护控制, f => SignalDataInCASCO.ATPProtectCtrl = (int)f);
            //ATP启动状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.ATP启动状态, f => SignalDataInCASCO.ATPStartStatus = (int)f);
            //信标状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.信标状态, f => SignalDataInCASCO.BaliseStatus = (int)f);
            //列车早晚点
            dataChangedArgs.UpdateIfContains((string)InfKeys.列车早晚点, f => SignalDataInCASCO.TrainSoonerOrLaters = (int)f);
            //车载运行模式
            dataChangedArgs.UpdateIfContains((string)InfKeys.车载运行模式, f => SignalDataInCASCO.TrainRunMode = (int)f);
            //发车时间
            dataChangedArgs.UpdateIfContains((string)InfKeys.发车时间, f => SignalDataInCASCO.DepartTime = (int)f);
            //到达时间
            dataChangedArgs.UpdateIfContains((string)InfKeys.到达时间, f => SignalDataInCASCO.ArrivedTime = (int)f);
            //列车号
            dataChangedArgs.UpdateIfContains((string)InfKeys.列车号, f => SignalDataInCASCO.TrainNumber = (int)f);
            //路径号
            dataChangedArgs.UpdateIfContains((string)InfKeys.路径号, f => SignalDataInCASCO.RouteNumber = (int)f);
            //线路号
            dataChangedArgs.UpdateIfContains((string)InfKeys.线路号, f => SignalDataInCASCO.RunLineNumber = (int)f);
            //单程号
            dataChangedArgs.UpdateIfContains((string)InfKeys.单程号, f => SignalDataInCASCO.OneWayNumber = (int)f);
            //ATP状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.ATP状态, f => SignalDataInCASCO.ATPStatus = (int)f);
            //ELS状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.ELS状态, f => SignalDataInCASCO.ELSStatus = (int)f);
            //RR状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.RR状态, f => SignalDataInCASCO.RRStatus = (int)f);
            //CP状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.CP状态, f => SignalDataInCASCO.CPStatus = (int)f);
            //VOBC车载主机状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.VOBC车载主机状态, f => SignalDataInCASCO.VOBCMasterStatus = (int)f);
            //车载定位状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.车载定位状态, f => SignalDataInCASCO.VOBCLocationStatus = (int)f);
            //车载系统与调度中心通信状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.车载系统与调度中心通信状态, f => SignalDataInCASCO.VOBCToCTCStatus = (int)f);
            //GPS工作状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.GPS工作状态, f => SignalDataInCASCO.GPSWorkStatus = (int)f);


            //GPS状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.GPS状态, f => SignalDataInCASCO.GPSStatus = (int)f);
            //NTC状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.NTC状态, f => SignalDataInCASCO.NTCStatus = (int)f);
            //雷达状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.雷达状态, f => SignalDataInCASCO.RadarStatus = (int)f);
            //STA状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.STA状态, f => SignalDataInCASCO.STAStatus = (int)f);
            //TOD状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.TOD状态, f => SignalDataInCASCO.TODStatus = (int)f);
            //无线连接状态
            dataChangedArgs.UpdateIfContains((string)InfKeys.无线连接状态, f => SignalDataInCASCO.WireLineStatus = (int)f);
            //到下一站时间
            dataChangedArgs.UpdateIfContains((string)InfKeys.到下一站时间, f => SignalDataInCASCO.ToNextStationTime = (int)f);
            //下一目标
            dataChangedArgs.UpdateIfContains((string)InfKeys.下一目标, f => SignalDataInCASCO.NextGoal = (int)f);
            //上站到下站偏移量
            dataChangedArgs.UpdateIfContains((string)InfKeys.上站到下站偏移量, f => SignalDataInCASCO.PreStationToNextStationOffSet = (int)f);

            //前车距离
            dataChangedArgs.UpdateIfContains((string)InfKeys.前车距离, f => SignalDataInCASCO.FrontTrainDistance = (int)f);
            //后车距离
            dataChangedArgs.UpdateIfContains((string)InfKeys.后车距离, f => SignalDataInCASCO.BackTrainDistance = (int)f);

            ////下一目标
            //dataChangedArgs.UpdateIfContains((string)InfKeys.下一目标, f => SignalDataInCASCO.NextGoal = (int)f);
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
            if ((!SignalDataInCASCOOld.PowerFlag && SignalDataInCASCO.PowerFlag)
                || (SignalDataInCASCOOld.PowerFlag && !SignalDataInCASCO.PowerFlag))
            {
                ClearInfos();
            }
            SignalDataInCASCOOld = SignalDataInCASCO.Copy();
            //初始化计划号、进路号
            CBTC.Recive.InitEndStationAndRoadID();
            CBTC.Recive.InitPlan();
        }




        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();
            return true;
        }



        public override bool BasicAndTrainInfo()
        {
            //base.BasicAndTrainInfo();
            //电源标志
            CBTC.TrainInfo.PowerState = SignalDataInCASCO.PowerFlag ? PowerState.Started : PowerState.Stopped;

            //系统时间
            CBTC.Other.NowInCBTC = SignalDataInCASCO.NowTime;
            //车载运行模式
            if (SignalDataInCASCO.TrainRunMode != SignalDataOutCASCO.TrainRunMode && SignalDataOutCASCO.TrainRunMode != 0)
            {
                CBTC.RunningInfo.VehicleRunningModel = (VehicleRunningModel)SignalDataOutCASCO.TrainRunMode;
            }
            else
            {
                CBTC.RunningInfo.VehicleRunningModel = (VehicleRunningModel)SignalDataInCASCO.TrainRunMode;
            }

           // CBTC.RunningInfo.VehicleRunningModel = (VehicleRunningModel)SignalDataInCASCO.TrainRunMode;
            // 等间隔时间
            CBTC.RunningInfo.VehicleOnlineEqualintervalTime = "7分11秒";
            //列车早晚点状态
            if (SignalDataInCASCO.TrainSoonerOrLaters<0)
            {
                CBTC.RunningInfo.TrainSoonerOrLaterStatus = TrainSoonerOrLaterStatus.TrainBreakfast;
                //SignalDataInCASCO.TrainSoonerOrLaters = -SignalDataInCASCO.TrainSoonerOrLaters;
                if (SignalDataInCASCO.TrainSoonerOrLaters>60)
                {
                    CBTC.RunningInfo.TrainSoonerOrLaterTime = (-SignalDataInCASCO.TrainSoonerOrLaters/60).ToString() + "分"
                        + (-SignalDataInCASCO.TrainSoonerOrLaters % 60) + "秒";
                }
                else
                {
                   CBTC.RunningInfo.TrainSoonerOrLaterTime = (-SignalDataInCASCO.TrainSoonerOrLaters).ToString() + "秒";
                }

            }
            else if (SignalDataInCASCO.TrainSoonerOrLaters > 0)
            {
                CBTC.RunningInfo.TrainSoonerOrLaterStatus = TrainSoonerOrLaterStatus.TrainLater;
                //CBTC.RunningInfo.TrainSoonerOrLaterTime = (-SignalDataInCASCO.TrainSoonerOrLaters).ToString() + "秒";
                if (SignalDataInCASCO.TrainSoonerOrLaters > 60)
                {
                    CBTC.RunningInfo.TrainSoonerOrLaterTime = (SignalDataInCASCO.TrainSoonerOrLaters / 60).ToString() + "分"
                        + SignalDataInCASCO.TrainSoonerOrLaters % 60 + "秒";
                }
                else
                {
                    CBTC.RunningInfo.TrainSoonerOrLaterTime = SignalDataInCASCO.TrainSoonerOrLaters.ToString() + "秒";
                }
            }
            else
            {
                CBTC.RunningInfo.TrainSoonerOrLaterStatus = TrainSoonerOrLaterStatus.TrainScheduled;
                CBTC.RunningInfo.TrainSoonerOrLaterTime = "";
            }
            
            //列车早晚点时间
           // CBTC.RunningInfo.TrainSoonerOrLaterTime = "52秒";

            //线路
            CBTC.RoadInfo.StationInfo.NextStation = SignalDataInCASCO.NextStationNo > 0
                ? GetStationName((ulong)SignalDataInCASCO.NextStationNo) : "";

            CBTC.RoadInfo.StationInfo.EndStation = SignalDataInCASCO.EndStationNo > 0
                ? GetStationName((ulong)SignalDataInCASCO.EndStationNo) : "";

            CBTC.RoadInfo.StationInfo.CurrentStation = SignalDataInCASCO.CurrentStationNo > 0
                 ? GetStationName((ulong)SignalDataInCASCO.CurrentStationNo) : "";


            //CBTC.RoadInfo.StationInfo.CurrentStation = "成都西站";
            //CBTC.RoadInfo.StationInfo.NextStation = "联工站";
            //CBTC.RoadInfo.StationInfo.EndStation = "郫县西站";

            // 到站时间
          //  CBTC.RoadInfo.StationInfo.PSD.ArriveTime = DateTime.Now;
            //发车时间
            // CBTC.RoadInfo.StationInfo.PSD.DepartTime = DateTime.Now;
            //计划号
            CBTC.RoadInfo.PlanID = "无计划";
            //前车距离
            CBTC.SignalInfo.ForwardDistance = SignalDataInCASCO.FrontTrainDistance;
            //距离前车时间
            //CBTC.SignalInfo.ForwardCarTime

            //后车距离
            CBTC.SignalInfo.AfterDistance = SignalDataInCASCO.BackTrainDistance;
            //距离后车时间
            //CBTC.SignalInfo.AfterCarTime

            ////前车距离和时间
            //string sa = ( SignalDataInCASCO.FrontTrainDistance ).ToString();

            //CBTC.SignalInfo.ForwardDistanceAndTime = "589 米     31秒";
            ////后车距离和时间
            //CBTC.SignalInfo.AfterDistanceAndTime = "941 米     54秒";

            //司机号
            if (SignalDataInCASCO.DriverNum==0 && SignalDataOutCASCO.DriverNum == 0)
            {
                CBTC.RoadInfo.DriverID = "666"; //默认司机号
                // CBTC.RoadInfo.DriverID = SignalDataInCASCO.DriverNum.ToString();
            }
            else if(SignalDataOutCASCO.DriverNum>0)
            {
                CBTC.RoadInfo.DriverID = SignalDataOutCASCO.DriverNum.ToString();
            }
            //CBTC.RoadInfo.DriverID = "666";
            //车次号
            CBTC.RoadInfo.TrainID =SignalDataInCASCO.TrainNum.ToString()/* "5"*/;
            //折返信息
            CBTC.RoadInfo.ReturnState = (ReturnInfo)SignalDataInCASCO.TurnBackInfo;
            //车站控车状态
            CBTC.RoadInfo.StationInfo.PSD.StationControlCarStatus = (StationControlCarStatus)SignalDataInCASCO.StationControlTrainStatus;
            //信号制动状态
            CBTC.TrainInfo.BrakeInfo.SignalBrakeStatus = (SignalBrakeStatus)SignalDataInCASCO.BrakeStatus/*SignalBrakeStatus.EmergentBrake*/;

            //ATP保护控制状态
            CBTC.SignalInfo.ATPProtectionControlStatus = (ATPProtectionControlStatus)SignalDataInCASCO.ATPProtectCtrl;
            //ATP禁止状态
            CBTC.SignalInfo.ATPProhibitStatus = (ATPProhibitStatus)SignalDataInCASCO.ATPStartStatus;
            //信标状态
            CBTC.SignalInfo.BeaconStatus = (BeaconStatus)SignalDataInCASCO.BaliseStatus;

            //系统状态
            CBTC.TrainInfo.System.SystemStatus = (SystemStatus)SignalDataInCASCO.SystemStatus;
            //车载主机状态
            CBTC.TrainInfo.System.VOBCOnBoardHostStatus = (VOBCOnBoardHostStatus)SignalDataInCASCO.VOBCMasterStatus;
            //定位
            CBTC.TrainInfo.System.VehicleLocationStatus = (VehicleLocationStatus)SignalDataInCASCO.VOBCLocationStatus;
            //车载主机与调度中心
            CBTC.TrainInfo.System.VehicleCommunicationStatus = (VehicleCommunicationStatus)SignalDataInCASCO.VOBCToCTCStatus;
            //GPS状态
            CBTC.TrainInfo.System.GPSWorkStatus = (GPSWorkStatus)SignalDataInCASCO.GPSStatus;

            CBTC.TrainInfo.System.ATPStatus = (ATPStatus)SignalDataInCASCO.ATPStatus;
            CBTC.TrainInfo.System.ELSStatus = (ELSStatus) SignalDataInCASCO.ELSStatus;
            CBTC.TrainInfo.System.RRStatus = (RRStatus) SignalDataInCASCO.RRStatus;
            CBTC.TrainInfo.System.CPStatus = (CPStatus) SignalDataInCASCO.CPStatus;
            // CBTC.TrainInfo.System.CPStatus = CPStatus.Fault;
           // CBTC.SignalInfo.Alram.SemaphoreAlram.Value = (SemaphoreStaus) SignalDataInCASCO.FrontSignalStatus;




            return false;
        }

        public override bool ScreenSpeedInfo()
        {
            base.ScreenSpeedInfo();
            //**************B4区信息显示******************
            //当前限速
            CBTC.SignalInfo.Alram.CurrentLimitSpeedAlram.Text = "当前限速";
            CBTC.SignalInfo.Alram.CurrentLimitSpeedAlram.Value = SignalDataIn.TrainRecommendSpeed;
            CBTC.SignalInfo.Alram.CurrentLimitSpeedAlram.Distance = 0;
            CBTC.SignalInfo.Alram.CurrentLimitSpeedAlram.Visible = true;

            //前方限速
            CBTC.SignalInfo.Alram.ForwardLimitSppedAlram.Text = "前方限速";
            CBTC.SignalInfo.Alram.ForwardLimitSppedAlram.Value = SignalDataIn.TrainGoalSpeed;
            CBTC.SignalInfo.Alram.ForwardLimitSppedAlram.Distance = SignalDataIn.GoalDistance;
            CBTC.SignalInfo.Alram.ForwardLimitSppedAlram.Visible = true;

            //前方信号
            CBTC.SignalInfo.Alram.SemaphoreAlram.Text = "前方信号";
            CBTC.SignalInfo.Alram.SemaphoreAlram.Value = (SemaphoreStaus)SignalDataIn.FrontSignalStatus;
            CBTC.SignalInfo.Alram.SemaphoreAlram.Distance = SignalDataInCASCO.FrontSignalDistance /*SignalDataIn.GoalDistance + 15*/;
            CBTC.SignalInfo.Alram.SemaphoreAlram.Visible = true;

            //进路请求
            CBTC.SignalInfo.Alram.RoadRequestAlram.Text = "";
            CBTC.SignalInfo.Alram.RoadRequestAlram.Value = 42;
            CBTC.SignalInfo.Alram.RoadRequestAlram.Distance = SignalDataInCASCO.RouteSelectEndDistance;
            CBTC.SignalInfo.Alram.RoadRequestAlram.Visible = true;

            //*************B2区域信息********************
            //目标速度
            CBTC.SignalInfo.Speed.TargetSpeed.Value = SignalDataIn.TrainRecommendSpeed;
            CBTC.SignalInfo.Speed.TargetSpeed.Visible = true;
            CBTC.SignalInfo.Speed.TargetSpeed.SpeedColor = CBTCColor.Green;
            //报警速度
            CBTC.SignalInfo.Speed.AlarmSpeed.Value = SignalDataInCASCO.TrainWarningSpeed;
            CBTC.SignalInfo.Speed.AlarmSpeed.Visible = true;
            CBTC.SignalInfo.Speed.AlarmSpeed.SpeedColor = CBTCColor.Green;

            //运行速度
            CBTC.SignalInfo.Speed.RunSpeed.Value = SignalDataIn.TrainSpeed;
            CBTC.SignalInfo.Speed.RunSpeed.Visible = true;
            CBTC.SignalInfo.Speed.RunSpeed.SpeedColor = CBTCColor.Green;

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
            SignalDataInCASCO.ClearInfo();
            SignalDataOutCASCO.ClearInfo();
            return false;
        }
    }
}