using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtil.Model;
using Excel.Interface;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Tram.CBTC.DataAdapter.Extension;
using Tram.CBTC.DataAdapter.Resource.Keys;
using Tram.CBTC.DataAdapter.Util;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Extension;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Fault;
using Tram.CBTC.Infrasturcture.Model.Road;

namespace Tram.CBTC.DataAdapter.ConcreateAdapter.NRIET
{
    public class NRIETDataAdapter : DataAdapterBase
    {
        protected SignalDataInNRIET SignalDataInNRIET { get; set; }
        protected SignalDataInNRIET SignalDataInNRIETOld { get; set; }
        protected SignalDataOutNRIET SignalDataOutNRIET { get; set; }
        public object InFloatKeys { get; private set; }


        public const string strFork = "道岔";
        public const string strCrossing = "道口";
        public const string strStation = "车站";
        public const string strDefault = "_ _";


        public NRIETDataAdapter(global::Tram.CBTC.Infrasturcture.Model.CBTC adaptTarget)
            : base(adaptTarget, new SendInterfaceNRIET(new SignalDataOutNRIET()))
        {
            SignalDataInNRIET = new SignalDataInNRIET();
            SignalDataInNRIETOld = new SignalDataInNRIET();
            SignalDataInNRIETOld = SignalDataInNRIET.Copy();
            SignalDataIn = SignalDataInNRIET;
            SignalDataInOld = SignalDataInNRIETOld;

            SignalDataOutNRIET = (SignalDataOutNRIET)SignalDataOut;
            SignalDataOutNRIET.SignalDataIn = SignalDataInNRIET;
            SignalDataOutNRIET.CBTC = CBTC;
            SignalDataOut = SignalDataOutNRIET;


            //车站
            var tmp = StationConfigDictionary.Values.OrderBy(o => o.StationKey).Select((s) => new StationPass { ID = s.StationKey, StationName = s.Staion, IsStation = true }).ToList();

            var list = new List<StationPass>();

            for (int i = 0; i < tmp.Count; ++i)
            {
                list.Add(tmp[i]);
                if (i < tmp.Count - 1)
                {
                    for (int j = 0; j < 4; ++j)
                    {
                        list.Add(new StationPass());
                    }
                }
            }
            CBTC.RoadInfo.StationInfo.StationPasses = list.AsReadOnly();


            //创建8个雷达目标，默认值
            for (int i = 0; i < 8; i++)
            {
                CBTC.FaultInfo.RadarInfo.AllRadarTargets.Add(new RadarTarget() { BackColor = CBTCColor.White, HorizontalDistance = 0, VerticalDistance = 0 });
            }


            //列车时刻表
            var timeTableService = App.Current.ServiceManager.GetService<ITimeTableProviderService>();

            if (timeTableService != null)
            {
                timeTableService.TimeTableChanged += TimeTableServiceOnTimeTableChanged;
            }


            //雷达测试代码
            //CBTC.FaultInfo.RadarInfo.AllRadarTargets.Add(new RadarTarget(){BackColor = CBTCColor.Red, HorizontalDistance = 20, VerticalDistance = 100});
            //CBTC.FaultInfo.RadarInfo.AllRadarTargets.Add(new RadarTarget() { BackColor = CBTCColor.Green, HorizontalDistance = 50, VerticalDistance = 150 });
            //CBTC.FaultInfo.RadarInfo.AllRadarTargets.Add(new RadarTarget() { BackColor = CBTCColor.White, HorizontalDistance = 80, VerticalDistance = 190 });


            //时刻表测试代码
            //Dictionary<int, CTimeTableItem> TempTimeTableItemsDictionary = new Dictionary<int, CTimeTableItem>();

            //DateTime nowTime = DateTime.Now;

            //int nStationID = 1;

            //for (int i = 0; i < 20; ++i)
            //{
            //    if (nStationID > 11)
            //    {
            //        nStationID = 1;
            //    }

            //    TempTimeTableItemsDictionary.Add(i, new CTimeTableItem() { ID = string.Format("{0}", nStationID), ArriveTime = string.Format("{0}", nowTime.Ticks + i), DepartTime = string.Format("{0}", nowTime.Ticks + i * 10) });

            //    ++nStationID;
            //}

            //var TimeList = new List<TimeTableItem>();

            //foreach (var TimeItem in TempTimeTableItemsDictionary)
            //{
            //    ulong nID = 0;
            //    ulong.TryParse(TimeItem.Value.ID, out nID);
            //    var StationConfig = StationConfigDictionary.Values.FirstOrDefault(s => s.StationKey == nID);

            //    long ArrieveTimeTicks = 0;
            //    long.TryParse(TimeItem.Value.ArriveTime, out ArrieveTimeTicks);

            //    long DepartureTimeTicks = 0;
            //    long.TryParse(TimeItem.Value.DepartTime, out DepartureTimeTicks);

            //    TimeList.Add(new TimeTableItem() { StationName = StationConfig.Staion, ArrieveTime = new DateTime(ArrieveTimeTicks), DepartureTime = new DateTime(DepartureTimeTicks) });
            //}

            //CBTC.TimeTableInfo.TimeTables = TimeList.AsReadOnly();
        }


        private void TimeTableServiceOnTimeTableChanged(object sender1, EventArgs eventArgs)
        {
            var items = sender1 as ITimeTableProviderService;
            CBTC.TimeTableInfo.TimeTables = items.TimeTableItemsDictionary.Select(
                t =>
                {
                    var time = new TimeTableItem();
                    time.ID = Convert.ToUInt64(t.ID);
                    time.ID = time.ID % 101000000;

                    if (!string.IsNullOrEmpty(t.ArriveTime) && t.ArriveTime != "0")
                    {
                        time.ArrieveTime = string.Format("{0:HH:mm}", NewDate(Convert.ToInt32(t.ArriveTime)));
                    }
                    else
                    {
                        time.ArrieveTime = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(t.DepartTime) && t.DepartTime != "0")
                    {
                        time.DepartureTime = string.Format("{0:HH:mm}", NewDate(Convert.ToInt32(t.DepartTime)));
                    }
                    else
                    {
                        time.DepartureTime = string.Empty;
                    }

                    var stat = StationConfigDictionary.Values.FirstOrDefault(f => f.StationKey == time.ID);
                    if (stat != null)
                    {
                        time.StationName = string.Format("{0}站",stat.Staion);
                    }

                    return time;
                }).ToList().AsReadOnly();


        }

        public override void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.OnBoolChangedImp(sender, dataChangedArgs);



            FeedBackInfo(sender, dataChangedArgs);
        }
        public override void OnFloatChangedImp(object sender, CommunicationDataChangedWrapperArgs<float> dataChangedArgs)
        {
            base.OnFloatChangedImp(sender, dataChangedArgs);
            //应答器故障状态
            dataChangedArgs.UpdateIfContains(InfKeys.信标状态, f => SignalDataInNRIET.BaliseState = (int)f);
            //GPS故障状态
            dataChangedArgs.UpdateIfContains(InfKeys.GPS状态, f => SignalDataInNRIET.GPSState = (int)f);
            //NTC状态故障状态
            dataChangedArgs.UpdateIfContains(InfKeys.NTC状态, f => SignalDataInNRIET.NTCState = (int)f);
            //雷达状态故障状态
            dataChangedArgs.UpdateIfContains(InfKeys.雷达状态, f => SignalDataInNRIET.RadarState = (int)f);
            //STA状态故障状态
            dataChangedArgs.UpdateIfContains(InfKeys.STA状态, f => SignalDataInNRIET.STAState = (int)f);
            //TOD状态故障状态
            dataChangedArgs.UpdateIfContains(InfKeys.TOD状态, f => SignalDataInNRIET.TODState = (int)f);
            //无线连接状态故障状态
            dataChangedArgs.UpdateIfContains(InfKeys.无线连接状态, f => SignalDataInNRIET.WirelessState = (int)f);
            //到下一站时间
            dataChangedArgs.UpdateIfContains(InfKeys.到下一站时间, f => SignalDataInNRIET.ArriveNextStationTimeSpan = f);
            //下一目标
            dataChangedArgs.UpdateIfContains(InfKeys.下一目标, f => SignalDataInNRIET.NextTarget = (int)f);
            //上站到下站偏移量
            dataChangedArgs.UpdateIfContains(InfKeys.上站到下站偏移量, f => SignalDataInNRIET.LastToNextStationOffSet = (int)f);
            //出入库预告
            dataChangedArgs.UpdateIfContains(InfKeys.出入库预告, f => SignalDataInNRIET.DepotNotice = (int)f);
            //进出折返预告
            dataChangedArgs.UpdateIfContains(InfKeys.进出折返预告, f => SignalDataInNRIET.TurnBackNotice = (int)f);

            int BeginNum = base.InterfaceAdapterService.InFloatDictionary[InfKeys.障碍物距离1];
            for (int i = base.InterfaceAdapterService.InFloatDictionary[InfKeys.障碍物距离1];
                i < base.InterfaceAdapterService.InFloatDictionary[InfKeys.障碍物左右偏移3] + 1;
                i++)
            {
               dataChangedArgs.UpdateIfContains(i, f => SignalDataInNRIET.Obstacle[i - BeginNum] = f);
            }
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

            //3、清除信息,亮屏的时候刷数据，数组数据会为空，必须黑屏刷新
              if (SignalDataInNRIETOld.PowerFlag && !SignalDataInNRIET.PowerFlag)
            {
                ClearInfos();
                SetPassed(0, 0, 0); 
            }
            SignalDataInNRIETOld = SignalDataInNRIET.Copy();
        }


        


        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OuBKeys.启动完成], SignalDataOutNRIET.StartFinish);
            return true;
        }

        public override bool ScreenSpeedInfo()
        {
            CBTC.SignalInfo.Speed.RunSpeed.Visible = true;
            CBTC.SignalInfo.Speed.RunSpeed.Value = SignalDataIn.TrainSpeed;
            CBTC.SignalInfo.Speed.RunSpeed.SpeedColor = CBTCColor.Red;
            CBTC.SignalInfo.Speed.RecommendedSpeed.Visible = true;
            CBTC.SignalInfo.Speed.RecommendedSpeed.Value = SignalDataIn.TrainRecommendSpeed;
            CBTC.SignalInfo.Speed.RecommendedSpeed.SpeedColor = CBTCColor.White;
            CBTC.SignalInfo.Speed.AlarmSpeed.Visible = true;
            CBTC.SignalInfo.Speed.AlarmSpeed.Value = 80;
            CBTC.SignalInfo.Speed.AlarmSpeed.SpeedColor = CBTCColor.Red;
            CBTC.SignalInfo.Speed.BrakeDetailsType = BrakeDetailsType.Normal;
            if (SignalDataIn.TrainSpeed > SignalDataIn.TrainRecommendSpeed)
            {
                CBTC.SignalInfo.Speed.RecommendedSpeed.SpeedColor = CBTCColor.Red;
                if (SignalDataInNRIET.LocationBuildFlag)
                {
                    CBTC.SignalInfo.Speed.BrakeDetailsType = BrakeDetailsType.OverSpeed;
                }
                else
                {
                    CBTC.SignalInfo.Speed.BrakeDetailsType = BrakeDetailsType.Normal;
                }
               
            }
            if (SignalDataIn.TrainRecommendSpeed == 0)
            {
                CBTC.SignalInfo.Speed.RecommendedSpeed.SpeedColor = CBTCColor.White;
                CBTC.SignalInfo.Speed.AlarmSpeed.SpeedColor = CBTCColor.White;
            }
            return true;
        }

        public override bool BasicAndTrainInfo()
        {   
            //启动控制
            if(SignalDataInNRIET.PowerFlag)
            {
                CBTC.TrainInfo.PowerState = PowerState.Started;
            }else 
            {
                CBTC.TrainInfo.PowerState = PowerState.Stopped;
            }

            //设备状态
            CBTC.TrainInfo.Device.BaliseState = (DeviceState)SignalDataInNRIET.BaliseState;
            CBTC.TrainInfo.Device.GPSState = (DeviceState)SignalDataInNRIET.GPSState;
            CBTC.TrainInfo.Device.NTCState = (DeviceState)SignalDataInNRIET.NTCState;
            CBTC.TrainInfo.Device.RadarState = (DeviceState)SignalDataInNRIET.RadarState;
            CBTC.TrainInfo.Device.STAState = (DeviceState)SignalDataInNRIET.STAState;
            CBTC.TrainInfo.Device.TODState = (DeviceState)SignalDataInNRIET.TODState;
            CBTC.TrainInfo.Device.DSRCState = (DeviceState)SignalDataInNRIET.WirelessState;
            //司机号、车次号
            CBTC.RoadInfo.DriverID = SignalDataOutNRIET.DriverCode;
            if(CBTC.RoadInfo.DriverID == "")
            {
                CBTC.RoadInfo.DriverID = strDefault;
            }
            CBTC.RoadInfo.TrainID = SignalDataIn.TrainNum.ToString();
            if (SignalDataIn.TrainNum == 0)
            {
                CBTC.RoadInfo.TrainID = strDefault;
            }
            //运行信息
            CBTC.RoadInfo.StationInfo.EndStation = GetStationName((ulong)SignalDataInNRIET.EndStationNo);
            if (CBTC.RoadInfo.StationInfo.EndStation=="")
            {
                CBTC.RoadInfo.StationInfo.EndStation = strDefault;
            }
           
            switch (SignalDataInNRIET.NextTarget)
            {
                case (int)NEXTTARGET.NEXTTARGET_FORK:
                    CBTC.RoadInfo.ForwardTagrt = strFork;
                    break;
                case (int)NEXTTARGET.NEXTTARGET_CROSSING:
                    CBTC.RoadInfo.ForwardTagrt = strCrossing;
                    break;
                case (int)NEXTTARGET.NEXTTARGET_STATION:
                    if(SignalDataInNRIET.LastToNextStationOffSet ==0)//在车站时前方目标还是车站
                    {
                        CBTC.RoadInfo.ForwardTagrt = GetStationName((ulong)SignalDataInNRIET.CurrentStationNo);
                    }
                    else
                    {
                        CBTC.RoadInfo.ForwardTagrt = GetStationName((ulong)SignalDataInNRIET.NextStationNo);
                    }
                    break;
                default:
                    CBTC.RoadInfo.ForwardTagrt = strDefault;
                    break;
            }
           
            if(SignalDataInNRIET.FrontTrainDis>200)
            {
                CBTC.SignalInfo.ForwardDistanceAndTime = "> 200m";
                
            }else if(SignalDataInNRIET.FrontTrainDis >0)
            {
                CBTC.SignalInfo.ForwardDistanceAndTime = SignalDataInNRIET.FrontTrainDis.ToString() + "m";
            }else
            {
                CBTC.SignalInfo.ForwardDistanceAndTime = strDefault;
            }
            //到下一站时间
            if (SignalDataInNRIET.ArriveNextStationTimeSpan < 0)
            {
                CBTC.SignalInfo.NextStationTime = strDefault;
            }else
            {
                DateTime t = new DateTime();
                t = t.AddSeconds((double)SignalDataInNRIET.ArriveNextStationTimeSpan);
                CBTC.SignalInfo.NextStationTime = t.ToString("mm：ss");
            }
           
           
            //停站时间,库内停站时间没有
            SignalDataInNRIET.StopStationTimeSec = (int)SignalDataInNRIET.StationStopTime;
           
            if (SignalDataInNRIET.StationStopTime<0)
            {
                SignalDataInNRIET.StopStationTimeSec = (int)(-1*SignalDataInNRIET.StationStopTime);
                if(SignalDataInNRIET.StopStationTimeSec>999)
                {
                    SignalDataInNRIET.StopStationTimeSec = 999;
                }
            }
           
            CBTC.RoadInfo.StationInfo.PSD.StopStationTimeSec = SignalDataInNRIET.StopStationTimeSec;
            if(SignalDataInNRIET.StationStopTime > 10)
            {
                CBTC.RoadInfo.StationInfo.PSD.StopStationFlicker = false;
                CBTC.RoadInfo.StationInfo.PSD.StopStationTimeColor = CBTCColor.White;
            }else if(SignalDataInNRIET.StationStopTime > 0)
            {
                CBTC.RoadInfo.StationInfo.PSD.StopStationTimeColor = CBTCColor.Red;
                CBTC.RoadInfo.StationInfo.PSD.StopStationFlicker = true;
            }
            else
            {
                CBTC.RoadInfo.StationInfo.PSD.StopStationFlicker = false;
                CBTC.RoadInfo.StationInfo.PSD.StopStationTimeColor = CBTCColor.Yellow;
            }
            
           
            if (SignalDataInNRIET.StationStopTime<999)
            {
                CBTC.RoadInfo.StationInfo.PSD.StopStationVisible = true;
            }else
            {
                CBTC.RoadInfo.StationInfo.PSD.StopStationVisible = false;
            }


            //车站控制
           base.SetPassed((ulong)SignalDataInNRIET.CurrentStationNo, (ulong)SignalDataInNRIET.NextStationNo, SignalDataInNRIET.LastToNextStationOffSet);

            //雷达控制
            CBTC.FaultInfo.OpenRadar = SignalDataOutNRIET.OpenRadar;
           for(int i=0;i<3;i++)
            {
                CBTC.FaultInfo.RadarInfo.AllRadarTargets[i].Visible = false;
                if(SignalDataInNRIET.Obstacle[i * 2 + 1] > 0)
                {
                    CBTC.FaultInfo.RadarInfo.AllRadarTargets[i].Visible = true;
                }
                CBTC.FaultInfo.RadarInfo.AllRadarTargets[i].HorizontalDistance = SignalDataInNRIET.Obstacle[i * 2 + 1];
                CBTC.FaultInfo.RadarInfo.AllRadarTargets[i].VerticalDistance = SignalDataInNRIET.Obstacle[i * 2];
                if (!CBTC.FaultInfo.OpenRadar || SignalDataIn.TrainSpeed < 0.5)
                {
                    CBTC.FaultInfo.RadarInfo.AllRadarTargets[i].BackColor = CBTCColor. White;
                }else
                {
                    if (SignalDataInNRIET.Obstacle[i * 2] > 100)
                    {
                        CBTC.FaultInfo.RadarInfo.AllRadarTargets[i].BackColor = CBTCColor.Green;
                    }
                    else if (SignalDataInNRIET.Obstacle[i * 2] > 50)
                    {
                        CBTC.FaultInfo.RadarInfo.AllRadarTargets[i].BackColor = CBTCColor.Yellow;
                    }
                    else if(SignalDataInNRIET.Obstacle[i * 2] > 0)
                    {
                        CBTC.FaultInfo.RadarInfo.AllRadarTargets[i].BackColor = CBTCColor.Red;
                    }
                }
               
            }
            //定位丢失
            CBTC.FaultInfo.LostGPS = false;
            if (!SignalDataInNRIET.LocationBuildFlag)
           {
                CBTC.FaultInfo.LostGPS = true;
           }
            //折返控制:赋值一次
            
            if((ReturnInfo)SignalDataInNRIET.TurnBackStatus != ReturnInfo.None && (ReturnInfo)SignalDataInNRIETOld.TurnBackStatus == ReturnInfo.None)
            {
                CBTC.RoadInfo.StationInfo.CurrentStation = GetStationName((ulong)SignalDataInNRIET.CurrentStationNo);
                CBTC.RoadInfo.ReturnState = (ReturnInfo)SignalDataInNRIET.TurnBackStatus;
            }else if((ReturnInfo)SignalDataInNRIET.TurnBackStatus == ReturnInfo.None)//换端完成后恢复
            {
                CBTC.RoadInfo.ReturnState = ReturnInfo.None;
            }

            CBTC.RoadInfo.ReturnIndicateInfo.ReturnIndicate = (ReturnIndicate)SignalDataInNRIET.TurnBackNotice;
            if((ReturnIndicate)SignalDataInNRIET.TurnBackNotice == ReturnIndicate.InReturn)
            {
                CBTC.RoadInfo.ReturnIndicateInfo.StationID = (ulong)SignalDataInNRIET.CurrentStationNo;
            }
            else if ((ReturnIndicate)SignalDataInNRIET.TurnBackNotice == ReturnIndicate.OutReurn)
            {
                CBTC.RoadInfo.ReturnIndicateInfo.StationID = (ulong)SignalDataInNRIET.NextStationNo;
            }

            //出入库控制

            CBTC.RoadInfo.GarageIndicateInfo.GarageIndicate = (GarageIndicate)SignalDataInNRIET.DepotNotice;
            if ((GarageIndicate)SignalDataInNRIET.DepotNotice == GarageIndicate.InGarage)
            {
                CBTC.RoadInfo.GarageIndicateInfo.StationID = (ulong)SignalDataInNRIET.CurrentStationNo;
            }
            else if ((GarageIndicate)SignalDataInNRIET.DepotNotice == GarageIndicate.OutGarage)
            {
                CBTC.RoadInfo.GarageIndicateInfo.StationID = (ulong)SignalDataInNRIET.NextStationNo;
            }

            //司机室未激活
            CBTC.TrainInfo.CarriageInfo.CurCabStatus = 0;
            CBTC.TrainInfo.CarriageInfo.CurrentCab.State = (OBCUStatus)SignalDataIn.OBCUHeadStatus;
            //司机室未激活清理站点
            if (CBTC.TrainInfo.CarriageInfo.CurrentCab.State== OBCUStatus.Standby)
            {
                SetPassed(0, 0, 0);
            }



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
            SignalDataInNRIET.ClearInfo();
            SignalDataOutNRIET.ClearInfo();
            return false;
        }
    }
}