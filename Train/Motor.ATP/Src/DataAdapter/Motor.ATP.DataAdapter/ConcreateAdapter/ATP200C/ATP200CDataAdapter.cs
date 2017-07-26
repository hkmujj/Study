using System;
using System.Globalization;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using Motor.ATP.DataAdapter.Extension;
using Motor.ATP.DataAdapter.Resource.Keys;
using Motor.ATP.Infrasturcture.Control.Infomation;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Extension;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.DataAdapter.Util;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP200C
{
    public class ATP200CDataAdapter : DataAdapterBase
    {

        protected SignalDataIn200C SignalDataIn200C { get; set; }
        protected SignalDataIn200C SignalDataIn200COld { get; set; }
        protected SignalDataOut200C SignalDataOut200C { get; set; }

        //private bool StartCtcsSelected = false;

        public ATP200CDataAdapter(ATPDomain adaptTarget)
            : base(adaptTarget, new SendInterface200C(new SignalDataOut200C()))
        {
            SignalDataIn200C = new SignalDataIn200C();
            SignalDataIn200COld = new SignalDataIn200C();
            SignalDataIn200COld = (SignalDataIn200C) SignalDataIn200C.Clone();
            SignalDataIn = SignalDataIn200C;
            SignalDataInOld = SignalDataIn200COld;

            SignalDataOut200C = (SignalDataOut200C)SignalDataOut;
            SignalDataOut200C.SignalDataIn = SignalDataIn200C;
            SignalDataOut200C.ATP = ATP;
            SignalDataOut = SignalDataOut200C;
        }

        public override void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.OnBoolChangedImp(sender, dataChangedArgs);
            //黑屏
            //dataChangedArgs.UpdateIfContains(InBoolKeys.ATP电源, b => SignalDataIn300s.ScreenBlackFlag = b);

            //请缓解列车制动
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_请缓解列车制动, b => SignalDataIn200C.TrainBrakeReleaseAck = b);
            //目视警惕确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_目视警惕确认, b => SignalDataIn200C.OSAlarmAck = b);
            //引导警惕确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.请求_引导警惕确认, b => SignalDataIn200C.SRAlarmAck = b);
            //目视使能
            dataChangedArgs.UpdateIfContains(InBoolKeys.目视使能, b => SignalDataIn200C.OSButtonStatus = b);
           

            FeedBackInfo(sender, dataChangedArgs);
        }

        public override void OnFloatChangedImp(object sender, CommunicationDataChangedWrapperArgs<float> dataChangedArgs)
        {
            base.OnFloatChangedImp(sender, dataChangedArgs);
            //紧急制动测试状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.紧急制动测试状态, f => SignalDataIn200C.nBreakTestStatus_Emerg = (int)f);
            //最大常用制动测试状态
            dataChangedArgs.UpdateIfContains(InFloatKeys.最大常用制动测试状态, f => SignalDataIn200C.nBreakTestStatus_Break7 = (int)f);
            //LKJ车次号头
            dataChangedArgs.UpdateIfContains(InFloatKeys.LKJ车次号头, f => SignalDataIn200C.LKJTrainNumHead = f);
            //LKJ车次号
            dataChangedArgs.UpdateIfContains(InFloatKeys.LKJ车次号, f => SignalDataIn200C.LKJTrainNum = f);
            //LKJ司机号
            dataChangedArgs.UpdateIfContains(InFloatKeys.LKJ司机号, f => SignalDataIn200C.LKJDriverNum = f);
        }

        public override void OnDataChangedImp(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            //1、信号-屏适配
            BasicAndTrainInfo();
            ScreenDistanceInfo();
            ScreenSpeedInfo();
            ScreenPlanInfo();

            ScreenBtnEnable(); //按钮使能

            ScreenTextInfo(); //E区文本
            ScreenControl(); //F区流程控制

            //2、屏-信号适配
            ScreenToSignalInfo();

            //3、清除信息
            if ((SignalDataIn200COld.ATPPowerFlag && !SignalDataIn200C.ATPPowerFlag))//|| (!SignalDataIn300hOld.ATPBypassFlag && SignalDataIn300h.ATPBypassFlag)
            {
                ClearInfos();
            }


            //4、保存上周期状态 
            //if (SignalDataIn300hOld.ControlMode != SignalDataIn.ControlMode)
            //{
            //    Trace.WriteLine("old data：" + SignalDataIn300hOld.ControlMode.ToString());
            //    Trace.WriteLine("new data：" + SignalDataIn.ControlMode.ToString());
            //}
            SignalDataIn200COld = (SignalDataIn200C) SignalDataIn200C.Clone();

            //GC.Collect();

        }

        public override bool FeedBackInfo(object sender, CommunicationDataChangedWrapperArgs<bool> dataChangedArgs)
        {
            base.FeedBackInfo(sender, dataChangedArgs);
            //反馈_请缓解列车制动
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_请缓解列车制动, b =>
            {
                if (b)
                    SignalDataOut200C.TrainBrakeReleaseSign = false;
            });
            //反馈_目视警惕确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_目视警惕确认, b =>
            {
                if (b)
                    SignalDataOut200C.OSAlarmSign = false;
            });
            //反馈_引导警惕确认
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_引导警惕确认, b =>
            {
                if (b)
                    SignalDataOut200C.SRAlarmSign = false;
            });
            //反馈_执行紧急制动测试
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_执行紧急制动测试, b =>
            {
                if (b)
                    SignalDataOut200C.BrakeTestEmerg = false;
            }); 
            //反馈_执行最大常用制动测试
            dataChangedArgs.UpdateIfContains(InBoolKeys.反馈_执行最大常用制动测试, b =>
            {
                if (b)
                    SignalDataOut200C.BrakeTestBreak7 = false;
            }); 
            return true;
        }

        public override bool ScreenToSignalInfo()
        {
            base.ScreenToSignalInfo();
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认请缓解列车制动],
               SignalDataOut200C.TrainBrakeReleaseSign);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认目视警惕确认],
               SignalDataOut200C.OSAlarmSign);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.确认引导警惕确认],
               SignalDataOut200C.SRAlarmSign);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.执行紧急制动测试],
               SignalDataOut200C.BrakeTestEmerg);
            DataService.WriteService.ChangeBool(InterfaceAdapterService.OutBoolDictionary[OutBoolKeys.执行最大常用制动测试],
               SignalDataOut200C.BrakeTestBreak7);

            return true;
        }
        public override bool ScreenPlanInfo()
        {
            base.ScreenPlanInfo();
            if (SignalDataIn.ControlMode != (int)ControlType.FullSupervision)
            {
                ATP.SpeedMonitoringSection.Visible = false;
            }
            else
            {
                ATP.SpeedMonitoringSection.Visible = true;
            }
            return true;
        }
        public override bool ScreenSpeedInfo()
        {
            base.ScreenSpeedInfo();
            //C2LKJ控车无限速处理
            if (SignalDataIn.ControlMode == (int)ControlType.LKJ )
            {
                ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Grey;
                ATP.TrainInfo.Speed.TargetSpeed.SpeedColor = ATPColor.None;
                ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.None;
                ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.SpeedColor = ATPColor.None;
                ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.SpeedColor = ATPColor.None;
               
             }else if( SignalDataIn.ControlMode == (int)ControlType.StandBy || SignalDataIn.ControlMode == (int)ControlType.Trip || SignalDataIn.ControlMode == (int)ControlType.PostTrip)//有限速要求的模式
            {
                //如果有制动指针变成红色
                if (SignalDataIn200C.BrakeStatus > (int)BrakeStatus.PowerLoseSign)
                {
                    ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Red;
                }
            }else//有限速的模式
            {
                //如果紧急且限速为0允许速度变红；紧急时指针变红
                if (SignalDataIn200C.BrakeStatus == (int)BrakeStatus.CommonBreak_E && SignalDataIn.TrainLimitSpeed <0.5)
                {
                    ATP.TrainInfo.Speed.PermittedLimitSpeed.SpeedColor = ATPColor.Red;
                    ATP.TrainInfo.Speed.CurrentSpeed.SpeedColor = ATPColor.Red;
                }
            }
             return true;
        }
        public override bool BasicAndTrainInfo()
        {

            ATP.Other.NowInATP = SignalDataIn200C.NowTime;

            //速度及限速信息
            ATP.TrainInfo.Speed.Visible = !SignalDataIn200C.ChangeHostAuto;

            ATP.TrainInfo.Speed.CurrentSpeed.Value = Math.Abs(SignalDataIn200C.TrainSpeed);

            ATP.TrainInfo.Speed.ServiceBrakeInterventionSpeed.Value = SignalDataIn200C.SBISpeed;
            ATP.TrainInfo.Speed.EmergencyBrakeInterventionSpeed.Value = SignalDataIn200C.EBISpeed;
            ATP.TrainInfo.Speed.PermittedLimitSpeed.Value = SignalDataIn200C.TrainLimitSpeed;
            ATP.TrainInfo.Speed.IntervertionSpeed.Value = SignalDataIn200C.IntervertionSpeed;
            ATP.TrainInfo.Speed.WarningLimitSpeed.Value = SignalDataIn200C.WarningSpeed;
            ATP.TrainInfo.Speed.TargetSpeed.Value = SignalDataIn200C.ControlMode != (int)ControlType.FullSupervision &&
                                                    SignalDataIn200C.ControlMode != (int)ControlType.CallingOn
                ? SignalDataIn200C.TrainLimitSpeed - 5
                : (SignalDataIn200C.TrainGoalSpeed > SignalDataIn200C.TrainLimitSpeed
                    ? SignalDataIn200C.TrainLimitSpeed
                    : SignalDataIn200C.TrainGoalSpeed);
            //制动信息
            ATP.TrainInfo.Brake.Visible = SignalDataIn200C.BrakeStatus > (int)BrakeStatus.PowerLoseSign && SignalDataIn200C.BrakeStatus <= (int)BrakeStatus.CommonBreak_R &&
                                          SignalDataIn200C.BrakeTestStatus != (int)BrakeTestStatus.BrakeTesting;
            ATP.TrainInfo.Brake.BrakeType = (BrakeType)SignalDataIn200C.BrakeStatus;
            //公里标
            ATP.TrainInfo.KilometerPost.Visible = SignalDataIn200C.IsGLBVisible;
            if (ATP.TrainInfo.KilometerPost.Visible)
            {
                ATP.TrainInfo.KilometerPost.Kilometer = Math.Truncate(SignalDataIn200C.TrainGLB / 1000);
                ATP.TrainInfo.KilometerPost.Meter = SignalDataIn200C.TrainGLB % 1000;
            }
            //GSM && RBC
            //ATP.TrainInfo.ConnectState.GSMRState = GSMRState.Invalidate;
            //ATP.TrainInfo.ConnectState.RBCConnectState = RBCConnectState.Invalidate;

            ATP.TrainInfo.ConnectState.Visible = false;


            //车站
            ATP.TrainInfo.Station.Visible = true;
            ATP.TrainInfo.Station.CurrentStation = SignalDataIn200C.StationNo > 0
                ? GetStationName((ulong)SignalDataIn200C.StationNo)
                : "";
            //等级
            ATP.CTCS.CurrentCTCSType = (CTCSType)SignalDataIn200C.RunLevel;
            ATP.CTCS.Visible = ATP.CTCS.CurrentCTCSType == CTCSType.CTCS2 || ATP.CTCS.CurrentCTCSType == CTCSType.CTCS0;
            //模式
            ATP.ControlModel.InEffect = true;
            ATP.ControlModel.CurrentControlType = (ControlType)SignalDataIn200C.ControlMode;
            //TODO 增加其它模式
            ATP.SpeedMonitoringSection.ZoomVisible = true;
            ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState = (ControlType)SignalDataIn200C.ControlMode ==
                                                                             ControlType.Shunting
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;
            ATP.RegionFStateProvier.CabsignalStateProvider.EnterOrQuitState = (ControlType)SignalDataIn200C.ControlMode ==
                                                                              ControlType.LKJ
                ? EnterOrQuit.Quit
                : EnterOrQuit.Enter;

            //机车信号
            ATP.CabSignal.Code = (CabSignalCode)SignalDataIn200C.SignalCode;
            ATP.CabSignal.Visible = ATP.CabSignal.Code > 0;
            //机控人控
            ATP.TrainControlType = (TrainControlType)SignalDataIn200C.BrakeMode;
            ATP.TrainControlTypeVisible = ATP.TrainControlType != TrainControlType.Unkown;
            //电源
            ATP.ATPPower.ATPPowerState = SignalDataIn200C.ATPPowerFlag ? ATPPowerState.Started : ATPPowerState.Stopped;


            //司机号、车次号
            ATP.TrainInfo.Driver.TrainIdVisible = true;
            //同步LKJ司机号和车次号

            
            ATP.TrainInfo.Driver.TrainId = SignalDataOut200C.TrainCode;
            if (SignalDataIn200C.LKJTrainNum > 0)
            {
                ATP.TrainInfo.Driver.TrainId = DecodeTrainId(SignalDataIn200C.LKJTrainNumHead, SignalDataIn200C.LKJTrainNum);
            }
            ATP.TrainInfo.Driver.DriverId = SignalDataOut200C.DriverCode;
            if (SignalDataIn200C.LKJDriverNum > 0)
            {
                ATP.TrainInfo.Driver.DriverId = DecodeDriverId(SignalDataIn200C.LKJDriverNum);
            }

            //时间日期
            ATP.Other.TimeVisible = true;  
            ATP.Other.DateVisible = true;
            ATP.Other.DateTimeTitleVisible = true;
            //文本消息
            ATP.Message.Visible = true;
            ////隔离模式不显示
            //if (SignalDataIn200C.ControlMode == (int)ControlType.Isolated)
            //{

            //    ATP.Other.TimeVisible = false;
            //    ATP.Other.DateVisible = false;
            //    ATP.Other.DateTimeTitleVisible = false;

            //    ATP.SpeedMonitoringSection.ZoomVisible = false;

            //    ATP.CTCS.Visible = false;

            //    ATP.TrainInfo.Driver.TrainIdVisible = false;
            //    ATP.TrainInfo.Station.Visible = false;
            //    ATP.TrainInfo.ConnectState.Visible = false;
            //    ATP.TrainInfo.KilometerPost.Visible = false;
            //    ATP.TrainInfo.Brake.Visible = false;
            //    ATP.TrainInfo.Speed.Visible = false;
            //    ATP.Message.Visible = false;
            //    ATP.CabSignal.Visible = false;

            //    ATP.UpdateDriverInterface(DriverInterfaceKeys.Root_隔离状态);
            //}
            //列车车长
            if (SignalDataOut200C.TrainLength <= 0)
            {
                ATP.TrainInfo.TrainLegth[0] = 16;
            }
            else
            {
                ATP.TrainInfo.TrainLegth[0] = SignalDataOut200C.TrainLength;
            }
           
            return true;

        }

        public override bool ScreenBtnEnable()
        {
            //按钮触发状态

            for (int i = 0; i < SignalDataIn200C.ScreenBotton.Length; i++)
            {
                HardwareButton HBInfo = ATP.ATPHardwareButton.HardwareButtonCollection[i];
                HBInfo.Type = (UserActionType)(i + 1);
                HBInfo.MouseState = SignalDataIn200C.ScreenBotton[i] ? MouseState.MouseDown : MouseState.MouseUp;
            }
            //按钮使能状态
            //0-数据，1-模式，2-载频，3-等级，4-其他，5-启动，6-缓解,7-警惕,8-司机号,9-车次号,10-列车数据,11-RBC数据，12-调车，13-目视，14-机信，300h(15-制动测试执行，16-制动测试略过
            //17-RBCID，18-RBC电话号码，19-RBC网络号码)，20-制动测试，21-DMI测试,22-C0，23-C2,24-C3,25-列车数据，26-常用制动测试，27-紧急制动测试
            /*********300S按钮使能***********
            1、启动完成前按钮状态
            （1）屏自检完成以前，所有按钮灰色
            （2）未进入任何等级时，等级、启动、缓解、警惕按钮灰色，其他亮
            （3）C2等级，启动条件不满足时，启动按钮为灰色，其他亮
            （4）C3等级，等级、启动、缓解均为灰色,调车及机信灰色
            ********************************/
            bool[] BtnEnable = new bool[50];
            for (int i = 0; i < BtnEnable.Length; i++)
            {
                BtnEnable[i] = true;
            }
            //紧急制动测试前所有都是灰
            if (SignalDataIn200C.ControlMode == (int)ControlType.Unknown)//|| SignalDataIn200C.ControlMode == (int)ControlType.Isolated
            {
                for (int i = 0; i < 8; i++)
                {
                    BtnEnable[i] = false;
                }
            }
            //模式：LKJ灰，下面的目视也是灰
            if (SignalDataIn200C.ControlMode == (int)ControlType.LKJ || SignalDataIn200C.ControlMode == (int)ControlType.Trip 
                || SignalDataIn200C.ControlMode == (int)ControlType.Isolated)
            {
                BtnEnable[1] = false;
                BtnEnable[12] = false;
                BtnEnable[13] = false;
            }
            //载频：调车时灰
            if (SignalDataIn200C.ControlMode == (int)ControlType.Shunting || SignalDataIn200C.ControlMode == (int)ControlType.Trip
                || SignalDataIn200C.ControlMode == (int)ControlType.PostTrip || SignalDataIn200C.ControlMode == (int)ControlType.Isolated)
            {
                BtnEnable[2] = false;
            }

            //等级：调车灰
            if (SignalDataIn200C.ControlMode == (int)ControlType.Shunting || SignalDataIn200C.TrainSpeed > 0.5
                || SignalDataIn200C.ControlMode == (int)ControlType.Trip || SignalDataIn200C.ControlMode == (int)ControlType.PostTrip
                || SignalDataIn200C.ControlMode == (int)ControlType.Isolated)
            {
                BtnEnable[3] = false;
            }
            //启动按钮使能：待机下符合启动条件下才使能
            BtnEnable[5] = false;
            if ((SignalDataIn200C.ControlMode == (int)ControlType.StandBy || SignalDataIn200C.ControlMode == (int)ControlType.PostTrip) && SignalDataIn200C.StartConditionFlag)
            {
                BtnEnable[5] = true;
            }
            //缓解按钮使能：允许缓解时使能
            BtnEnable[6] = false;
            if (SignalDataIn200C.BrakeStatus == (int)BrakeStatus.CommonBreak_R)
            {
                BtnEnable[6] = true;
            }
            //警惕按钮使能：除了允许缓解所有闪烁文本都要警惕使能
            BtnEnable[7] = false;
            if (SignalDataIn200C.OSAlarmAck || SignalDataIn200C.SRAlarmAck||SignalDataIn200C.SRAck || SignalDataIn200C.TrainBrakeReleaseAck || SignalDataIn200C.MJAck
                || SignalDataIn200C.EnterC0Ack || SignalDataIn200C.EnterC2Ack)
            {
                BtnEnable[7] = true;
            }
            if (SignalDataIn200C.BrakeStatus == (int)BrakeStatus.CommonBreak_E && SignalDataIn200C.EBType == (int)EBType.EBType_ALERT && SignalDataIn200C.TrainSpeed <= 0.5)
            {
               BtnEnable[7] = true;
            } 
                    
            
            ////调车：1、C0，2、冒进
            // if (SignalDataIn200C.ControlMode == (int)ControlType.LKJ ||  SignalDataIn200C.ControlMode == (int)ControlType.Trip)
            //{
            //    BtnEnable[12] = false;
            //}
            //目视：1、调车灰，2、停车2分钟后运行目视才使能
             if (SignalDataIn200C.ControlMode == (int)ControlType.Shunting || !SignalDataIn200C.OSButtonStatus)
            {
                BtnEnable[13] = false;
            }
            //制动测试、DMI测试：只有在待机使能
            if (SignalDataIn200C.ControlMode != (int)ControlType.StandBy)
            {
                BtnEnable[20] = false;
                BtnEnable[21] = false;
            }
            //C2级使能：在C2等级且不在待机变灰,其他C2时C0使能，C0时C2使能
            if (SignalDataIn200C.ControlMode != (int)ControlType.StandBy)
            {
                if (SignalDataIn200C.RunLevel == (int)CTCSType.CTCS2)
                {
                    BtnEnable[23] = false;
                }else if (SignalDataIn200C.RunLevel == (int)CTCSType.CTCS0)
                {
                    BtnEnable[22] = false;
                }
                
            }
            //列车数据使能：只有待机模式使能
            if (SignalDataIn200C.ControlMode != (int)ControlType.StandBy)
            {
                BtnEnable[25] = false;
            }
            //制动测试过程中：常用和紧急变灰：从请求制动测试到制动测试成功或失败的时间
            if (SignalDataIn200C.ControlMode == (int)ControlType.StandBy)
            {
                if (SignalDataIn200C.nBreakTestStatus_Emerg <= (int)BrakeTestStatus.BrakeTesting && SignalDataIn200C.nBreakTestStatus_Emerg != 0)
                {
                    BtnEnable[26] = false;
                    BtnEnable[27] = false;
                }
                else if (SignalDataIn200C.nBreakTestStatus_Break7 <= (int)BrakeTestStatus.BrakeTesting && SignalDataIn200C.nBreakTestStatus_Break7 != 0)
                {
                    BtnEnable[26] = false;
                    BtnEnable[27] = false;
                }
               
            }
            //按钮使能转换
            for (int i = 0; i < BtnEnable.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        ATP.RegionFStateProvier.DataStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 1:
                        ATP.RegionFStateProvier.ControlModelStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 2:
                        ATP.RegionFStateProvier.FreqStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 3:
                        ATP.RegionFStateProvier.CTCSStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 4:
                        ATP.RegionFStateProvier.OtherStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 5:
                        ATP.RegionFStateProvier.StartStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 6:
                        ATP.RegionFStateProvier.RelaseStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 7:
                        ATP.RegionFStateProvier.VigilantStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 8:
                        ATP.RegionFStateProvier.DriverIdStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 9:
                        ATP.RegionFStateProvier.TrainIdStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 10:
                        ATP.RegionFStateProvier.TrainDataStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 11:
                        ATP.RegionFStateProvier.RBCDataStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 12:
                        ATP.RegionFStateProvier.ShuntingStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 13:
                        ATP.RegionFStateProvier.OnSightStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 14:
                        ATP.RegionFStateProvier.CabsignalStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 20:
                        ATP.RegionFStateProvier.RunBrakeTestStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 21:
                        ATP.RegionFStateProvier.DMITestStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 22:
                        ATP.RegionFStateProvier.CTCS0StateProvider.Enabled = BtnEnable[i];
                        break;
                    case 23:
                        ATP.RegionFStateProvier.CTCS2StateProvider.Enabled = BtnEnable[i];
                        break;
                    case 25:
                        ATP.RegionFStateProvier.TrainDataStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 26:
                        ATP.RegionFStateProvier.RunNormalBrakeTestStateProvider.Enabled = BtnEnable[i];
                        break;
                    case 27:
                        ATP.RegionFStateProvier.RunEmergencyBrakeTestStateProvider.Enabled = BtnEnable[i];
                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        public override bool ScreenTextInfo()
        {
            InfomationCreater InfoCreater =
                (InfomationCreater)ATP.InfomationService.GetInformationCreater();
            InfoCreater.ClearContents();



            //启动过程文本—start—————————————————————————————————————————————
            //请缓解列车制动
            if (!SignalDataIn200COld.TrainBrakeReleaseAck && SignalDataIn200C.TrainBrakeReleaseAck)
            {
                InfoCreater.UpdateInfomation(61);
            }
            else if (SignalDataIn200COld.TrainBrakeReleaseAck && !SignalDataIn200C.TrainBrakeReleaseAck)
            {
                InfoCreater.UpdateInfomation(61, InformationUpdateType.Ensure);
            }
            //紧急和常用制动测试
            if (SignalDataIn200COld.nBreakTestStatus_Emerg != SignalDataIn200C.nBreakTestStatus_Emerg)
            {   
                switch ((BrakeTestStatus)SignalDataIn200C.nBreakTestStatus_Emerg)
                {   
                    case BrakeTestStatus.BrakeTesting:
                        {
                            InfoCreater.UpdateInfomation(62);
                            if (SignalDataIn200C.ControlMode == (int)ControlType.Unknown)
                            {
                                SignalDataIn200C.BreakTestFlag = true;
                            }
                            else
                            {
                                SignalDataIn200C.BreakTestFlag = false;
                            }
                        }
                        break;
                    case BrakeTestStatus.BrakeTestSucceed:
                        {
                            InfoCreater.UpdateInfomation(63);
                            if (SignalDataIn200C.BreakTestFlag)
                            {
                                InfoCreater.UpdateInfomation(52);
                            }
                        }
                        break;
                    case BrakeTestStatus.BrakeTestFail:
                        {
                            InfoCreater.UpdateInfomation(64);
                            if (SignalDataIn200C.BreakTestFlag)
                            {
                                InfoCreater.UpdateInfomation(53);
                            }
                        }
                        break;
                }
            }
            
            if (SignalDataIn200COld.nBreakTestStatus_Break7 != SignalDataIn200C.nBreakTestStatus_Break7)
            {
                switch ((BrakeTestStatus)SignalDataIn200C.nBreakTestStatus_Break7)
                {
                    case BrakeTestStatus.BrakeTesting:
                        InfoCreater.UpdateInfomation(65);
                        break;
                    case BrakeTestStatus.BrakeTestSucceed:
                        InfoCreater.UpdateInfomation(66);
                        break;
                    case BrakeTestStatus.BrakeTestFail:
                        InfoCreater.UpdateInfomation(67);
                        break;
                }
            }
            
            //启动过程文本—start—————————————————————————————————————————————

            //闪烁文本—start—————————————————————————————————————————————
            //警惕、冒进、越行、引导确认
            //目视警惕确认
            if (!SignalDataIn200COld.OSAlarmAck && SignalDataIn200C.OSAlarmAck) 
            {
                InfoCreater.UpdateInfomation(38);
            }
            else if (SignalDataIn200COld.OSAlarmAck && !SignalDataIn200C.OSAlarmAck)
            {
                InfoCreater.UpdateInfomation(38, InformationUpdateType.Remove);
            }
            //引导警惕确认
            if (!SignalDataIn200COld.SRAlarmAck && SignalDataIn200C.SRAlarmAck)
            {
                InfoCreater.UpdateInfomation(69);
            }
            else if (SignalDataIn200COld.SRAlarmAck && !SignalDataIn200C.SRAlarmAck)
            {
                InfoCreater.UpdateInfomation(69, InformationUpdateType.Remove);
            }
            //冒进确认
            if (!SignalDataIn200COld.MJAck && SignalDataIn200C.MJAck)
            {
                InfoCreater.UpdateInfomation(39);
            }
            else if (SignalDataIn200COld.MJAck && !SignalDataIn200C.MJAck)
            {
                InfoCreater.UpdateInfomation(39, InformationUpdateType.Remove);
            }
            //允许缓解
            if (SignalDataIn200COld.BrakeStatus != (int)BrakeStatus.CommonBreak_R && SignalDataIn200C.BrakeStatus == (int)BrakeStatus.CommonBreak_R)
            {
                InfoCreater.UpdateInfomation(36);
            }
            else if (SignalDataIn200COld.BrakeStatus == (int)BrakeStatus.CommonBreak_R && SignalDataIn200C.BrakeStatus != (int)BrakeStatus.CommonBreak_R)
            {
                InfoCreater.UpdateInfomation(36, InformationUpdateType.Remove);
            }
        
            if (!SignalDataIn200COld.SRAck && SignalDataIn200C.SRAck)
            {
                InfoCreater.UpdateInfomation(41);
            }
            else if (SignalDataIn200COld.SRAck && !SignalDataIn200C.SRAck)
            {
                InfoCreater.UpdateInfomation(41, InformationUpdateType.Ensure);
            }
            //闪烁文本—end—————————————————————————————————————————————

            //等级转换确认
            if (!SignalDataIn200COld.EnterC2Ack && SignalDataIn200C.EnterC2Ack)
            {
                InfoCreater.UpdateInfomation(42);
            }
            if (!SignalDataIn200COld.EnterC0Ack && SignalDataIn200C.EnterC0Ack)
            {
                InfoCreater.UpdateInfomation(75);
            }
            //退行或溜逸防护
            if (SignalDataIn200COld.BrakeStatus != (int)BrakeStatus.CommonBreak_E && SignalDataIn200C.BrakeStatus == (int)BrakeStatus.CommonBreak_E
                && SignalDataIn200C.EBType == (int)EBType.EBTYPE_SlideProtect)
            {
                if (SignalDataIn200C.ControlMode == (int)ControlType.StandBy)
                {
                    InfoCreater.UpdateInfomation(72);
                }
                else
                {
                    InfoCreater.UpdateInfomation(47);
                }
            }
          
            //if (!SignalDataIn200COld.BackProtectAck && SignalDataIn200C.BackProtectAck)
            //{
            //    InfoCreater.UpdateInfomation(47, true);
            //}

       
            //等级转换文本
            if (SignalDataIn200COld.RunLevel != SignalDataIn200C.RunLevel && SignalDataIn200C.StartConditionFlag)
            {
                if (SignalDataIn200C.RunLevel == (int)CTCSType.CTCS0)
                {
                    InfoCreater.UpdateInfomation(68);
                }
                else if (SignalDataIn200C.RunLevel == (int)CTCSType.CTCS2)
                {
                    InfoCreater.UpdateInfomation(10);
                }
            }
            //等级转换预告文本
            if (SignalDataIn200COld.YGLevelInfo != SignalDataIn200C.YGLevelInfo)
            {
                if (SignalDataIn200C.YGLevelInfo == (int)LevelChangeType.YG_CTCS0TOCTCS2)
                {
                    InfoCreater.UpdateInfomation(12);
                }
                if (SignalDataIn200C.YGLevelInfo == (int)LevelChangeType.YG_CTCS2TOCTCS0)
                {
                    InfoCreater.UpdateInfomation(71);
                }
            }

            //模式转换文本
            if (SignalDataIn200COld.ControlMode != SignalDataIn200C.ControlMode)
            {
                switch ((ControlType)SignalDataIn200C.ControlMode)
                {
                    case ControlType.FullSupervision:
                        InfoCreater.UpdateInfomation(7);
                        break;
                    case ControlType.StandBy:
                        InfoCreater.UpdateInfomation(1);
                        break;
                    case ControlType.PartialSupervision:
                        InfoCreater.UpdateInfomation(2);
                        break;
                    case ControlType.Shunting:
                        InfoCreater.UpdateInfomation(4);
                        break;
                    case ControlType.OnSight:
                        InfoCreater.UpdateInfomation(3);
                        break;
                    case ControlType.CallingOn:
                        InfoCreater.UpdateInfomation(5);
                        break;
                    case ControlType.Isolated:
                        InfoCreater.UpdateInfomation(8);
                        break;
                    case ControlType.Sleep:
                        InfoCreater.UpdateInfomation(9);
                        break;
                    case ControlType.Trip:
                        InfoCreater.UpdateInfomation(73);
                        break;
                    case ControlType.PostTrip:
                        InfoCreater.UpdateInfomation(74);
                        break;
                    default:
                        break;
                }
            }
       //故障文本—start—————————————————————————————————————————————
            //“系统故障”
            if (!SignalDataIn200COld.Fault[0] && SignalDataIn200C.Fault[0])
            {
                InfoCreater.UpdateInfomation(76);
            }
            //“应答器信息接收模块故障”和“系统故障”
            if (!SignalDataIn200COld.Fault[1] && SignalDataIn200C.Fault[1])
            {
                InfoCreater.UpdateInfomation(76);
                InfoCreater.UpdateInfomation(77);
            }
            //“测速系统故障”和“系统故障”
            if (!SignalDataIn200COld.Fault[2] && SignalDataIn200C.Fault[2])
            {
                InfoCreater.UpdateInfomation(76);
                InfoCreater.UpdateInfomation(78);
            }
            //“列车接口故障”和“系统故障”
            if (!SignalDataIn200COld.Fault[3] && SignalDataIn200C.Fault[3])
            {
                InfoCreater.UpdateInfomation(76);
                InfoCreater.UpdateInfomation(79);
            }
            //“单链严重故障”
            if (!SignalDataIn200COld.Fault[4] && SignalDataIn200C.Fault[4])
            {
                InfoCreater.UpdateInfomation(80);
            }
           
    //故障文本—end—————————————————————————————————————————————


            InfoCreater.FlushInfomation();

            return true;
        }

        public override bool ClearInfos()
        {
            InfomationCreater InfoCreater =
                (InfomationCreater)ATP.InfomationService.GetInformationCreater();
            InfoCreater.ClearShowingItems();

            ATP.Other.ShowingTimeDifference = TimeSpan.Zero;
            ATP.GetInterfaceController().UpdateDriverInterface(DriverInterfaceKey.Root);

            //清除输入信息
            SignalDataIn200C.ClearInfo();
            SignalDataOut200C.ClearInfo();
            return true;
        }

        public override bool ScreenControl()
        {   
            //只要待机到目视才弹出载频：与选择了载频是否有关？
            if ( SignalDataIn200C.ControlMode == (int)ControlType.OnSight
                && SignalDataIn200COld.ControlMode == (int)ControlType.StandBy)
            {
                ATP.DriverInterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_载频));
            }
        
            return true;
        }

        private string DecodeDriverId(params float[] values)
        {
            if (values == null || !values.Any())
            {
                return "";
            }

            return Math.Truncate(values[0]).ToString("#######");
        }

        /// <summary>
        /// // 车次号
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private string DecodeTrainId(params float[] values)
        {
            if (values == null || values.Length < 2)
            {
                return "";
            }
            
            var sb = new StringBuilder();

            var head = Convert.ToChar(BitConverter.GetBytes(values[0])[0]);

            var c = Convert.ToChar(head);
            if (char.IsLower(c) || char.IsUpper(c))
            {
                sb.Append(c.ToString(CultureInfo.InvariantCulture).ToUpper());
            }

            return sb.Append(Math.Truncate(values[1]).ToString("####")).ToString();
        }
    }
}